using GadgetHubApi.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration; // For accessing configuration
using System; // Required for Exception

namespace GadgetHubApi.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration; // To get distributor API URLs

        public QuotationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<GadgetHubOrderSummary> ProcessCustomerOrder(CustomerOrderRequest customerOrder)
        {
            var overallStatus = "Order processed. See product details for fulfillment status.";
            var overallMessage = "Quotation comparison complete.";
            var totalEstimatedCost = 0m;
            var productComparisonResults = new List<ProductComparisonResult>();

            // 1. Request Quotations from all Distributors concurrently
            var techWorldTask = GetQuotationFromDistributor("TechWorldApi", customerOrder);
            var electroComTask = GetQuotationFromDistributor("ElectroComApi", customerOrder);
            var gadgetCentralTask = GetQuotationFromDistributor("GadgetCentralApi", customerOrder);

            // Wait for all tasks to complete
            await Task.WhenAll(techWorldTask, electroComTask, gadgetCentralTask);

            var distributorResponses = new List<QuotationResponse>();
            if (techWorldTask.Result != null) distributorResponses.Add(techWorldTask.Result);
            if (electroComTask.Result != null) distributorResponses.Add(electroComTask.Result);
            if (gadgetCentralTask.Result != null) distributorResponses.Add(gadgetCentralTask.Result);

            if (!distributorResponses.Any())
            {
                overallStatus = "Failed";
                overallMessage = "Could not get quotations from any distributors.";
                return new GadgetHubOrderSummary
                {
                    OrderId = customerOrder.OrderId,
                    TotalEstimatedCost = 0,
                    ProductResults = productComparisonResults,
                    OverallStatus = overallStatus,
                    OverallMessage = overallMessage
                };
            }

            // List to hold tasks for placing orders
            var orderPlacementTasks = new List<Task<OrderConfirmation?>>();
            var tempProductResults = new List<ProductComparisonResult>(); // Use a temp list to build results

            // 2. Compare Quotations for each product and prepare order placements
            foreach (var customerItem in customerOrder.Items)
            {
                ProductQuote? bestQuote = null;
                string bestDistributorName = "N/A";
                decimal bestPricePerUnit = decimal.MaxValue;
                var otherDistributorOptions = new List<string>();

                foreach (var distributorResponse in distributorResponses)
                {
                    var productQuote = distributorResponse.ProductQuotes
                                        .FirstOrDefault(pq => pq.ProductId == customerItem.ProductId);

                    if (productQuote != null && productQuote.AvailableUnits >= customerItem.Quantity && productQuote.PricePerUnit > 0)
                    {
                        // This distributor can fully fulfill the order for this product
                        if (productQuote.PricePerUnit < bestPricePerUnit)
                        {
                            bestPricePerUnit = productQuote.PricePerUnit;
                            bestQuote = productQuote;
                            bestDistributorName = distributorResponse.DistributorName;
                            otherDistributorOptions.Clear(); // Reset if a new best is found
                        }
                        else if (productQuote.PricePerUnit == bestPricePerUnit)
                        {
                            // If same price, add as another option
                            otherDistributorOptions.Add(distributorResponse.DistributorName);
                        }
                    }
                    else if (productQuote != null && productQuote.AvailableUnits > 0 && productQuote.PricePerUnit > 0)
                    {
                        // This distributor has some stock but not enough to fully fulfill.
                        // Add them to other options with partial stock info.
                        if (!otherDistributorOptions.Contains($"{distributorResponse.DistributorName} (Partial Stock: {productQuote.AvailableUnits})"))
                        {
                            otherDistributorOptions.Add($"{distributorResponse.DistributorName} (Partial Stock: {productQuote.AvailableUnits})");
                        }
                    }
                }

                // Initialize ProductComparisonResult with default values for all required members
                var productResult = new ProductComparisonResult
                {
                    ProductId = customerItem.ProductId,
                    RequestedQuantity = customerItem.Quantity,
                    OtherDistributorOptions = otherDistributorOptions.Distinct().ToList(),
                    BestPricePerUnit = bestPricePerUnit == decimal.MaxValue ? 0m : bestPricePerUnit, // Default to 0 if no best price found
                    BestDistributor = bestDistributorName, // This will be "N/A" if no best found
                    AvailableUnitsFromBest = bestQuote?.AvailableUnits ?? 0, // Default to 0
                    EstimatedDeliveryDaysFromBest = bestQuote?.EstimatedDeliveryDays ?? -1, // Default to -1
                    FullyFulfilled = bestQuote != null, // Set to true only if a best quote was found
                    StatusMessage = "Quoted successfully." // Default status message
                };


                if (bestQuote != null)
                {
                    productResult.StatusMessage = $"Best option: {bestDistributorName}. Preparing order.";

                    // Prepare order placement task for the best option
                    orderPlacementTasks.Add(PlaceOrderWithDistributor(
                        $"{bestDistributorName}Api", // e.g., "TechWorldApi"
                        customerOrder.OrderId,
                        customerItem.ProductId,
                        customerItem.Quantity,
                        bestPricePerUnit
                    ));
                }
                else
                {
                    // Product not fully available from any distributor
                    productResult.StatusMessage = "Not fully available from any distributor.";

                    // Collect all distributors that had some stock or even just listed the product
                    var availableFromAny = distributorResponses
                        .SelectMany(dr => dr.ProductQuotes.Where(pq => pq.ProductId == customerItem.ProductId && pq.AvailableUnits > 0))
                        .Select(pq => $"{distributorResponses.First(dr => dr.ProductQuotes.Contains(pq)).DistributorName} (Stock: {pq.AvailableUnits})")
                        .Distinct()
                        .ToList();

                    if (availableFromAny.Any())
                    {
                        productResult.OtherDistributorOptions.AddRange(availableFromAny);
                        productResult.StatusMessage = "Partial fulfillment possible from: " + string.Join(", ", availableFromAny);
                    }
                    else
                    {
                        productResult.StatusMessage = "Not available from any distributor.";
                    }
                    overallStatus = "Partially Fulfilled or Unfulfilled"; // Update overall status if any product isn't fully fulfilled
                }
                tempProductResults.Add(productResult); // Add to temp list for now
            }

            // 3. Execute Order Placement concurrently for all selected products
            var orderConfirmations = await Task.WhenAll(orderPlacementTasks);

            // 4. Process Order Confirmations and finalize results
            foreach (var result in tempProductResults)
            {
                if (result.FullyFulfilled) // Only process if we attempted to place an order
                {
                    var confirmation = orderConfirmations.FirstOrDefault(oc => oc?.ProductId == result.ProductId && oc?.GadgetHubOrderId == customerOrder.OrderId);

                    if (confirmation != null && confirmation.Success)
                    {
                        result.StatusMessage = $"Order confirmed by {result.BestDistributor}. Distributor Order ID: {confirmation.DistributorOrderId}.";
                        result.EstimatedDeliveryDaysFromBest = confirmation.EstimatedDeliveryDays;
                        result.BestPricePerUnit = confirmation.ConfirmedPricePerUnit; // Use confirmed price
                        totalEstimatedCost += (confirmation.ConfirmedPricePerUnit * confirmation.ConfirmedQuantity);
                    }
                    else
                    {
                        result.FullyFulfilled = false;
                        result.StatusMessage = $"Order placement failed with {result.BestDistributor}. Message: {confirmation?.Message ?? "Unknown error."}";
                        overallStatus = "Partially Fulfilled or Unfulfilled"; // Update overall status
                    }
                }
                productComparisonResults.Add(result); // Add final result to the main list
            }


            // 5. Customer Notification (Simulated)
            Console.WriteLine($"--- Order Processing Summary for Order: {customerOrder.OrderId} ---");
            Console.WriteLine($"Overall Status: {overallStatus}");
            Console.WriteLine($"Overall Message: {overallMessage}");
            Console.WriteLine($"Total Estimated Cost: ${totalEstimatedCost:F2}");
            foreach (var result in productComparisonResults)
            {
                Console.WriteLine($"  - Product {result.ProductId} (Requested: {result.RequestedQuantity}):");
                Console.WriteLine($"    Status: {result.StatusMessage}");
                if (result.FullyFulfilled)
                {
                    Console.WriteLine($"    Best Distributor: {result.BestDistributor}");
                    Console.WriteLine($"    Price/Unit: ${result.BestPricePerUnit:F2}");
                    Console.WriteLine($"    Delivery: {result.EstimatedDeliveryDaysFromBest} days");
                }
                if (result.OtherDistributorOptions.Any())
                {
                    Console.WriteLine($"    Other Options: {string.Join(", ", result.OtherDistributorOptions)}");
                }
            }
            Console.WriteLine("-------------------------------------------------");


            return new GadgetHubOrderSummary
            {
                OrderId = customerOrder.OrderId,
                TotalEstimatedCost = totalEstimatedCost,
                ProductResults = productComparisonResults,
                OverallStatus = overallStatus,
                OverallMessage = overallMessage
            };
        }

        private async Task<QuotationResponse?> GetQuotationFromDistributor(string clientName, CustomerOrderRequest customerOrder)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                // Explicitly use Models.QuotationRequest and Models.ProductRequest
                var quotationRequest = new Models.QuotationRequest // Explicitly use Models.QuotationRequest
                {
                    OrderId = customerOrder.OrderId,
                    Products = customerOrder.Items.Select(item => new Models.ProductRequest // Explicitly use Models.ProductRequest
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(quotationRequest, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("Quotations/request", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var quotationResponse = await JsonSerializer.DeserializeAsync<QuotationResponse>(
                        responseStream,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    );
                    return quotationResponse;
                }
                else
                {
                    Console.WriteLine($"Error getting quotation from {clientName}: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error getting quotation from {clientName}: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error from {clientName}: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred with {clientName}: {ex.Message}");
                return null;
            }
        }

        private async Task<OrderConfirmation?> PlaceOrderWithDistributor(
            string clientName,
            string gadgetHubOrderId,
            string productId,
            int quantity,
            decimal agreedPricePerUnit)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(clientName);
                // Explicitly use Models.OrderRequest
                var orderRequest = new Models.OrderRequest // Explicitly use Models.OrderRequest
                {
                    GadgetHubOrderId = gadgetHubOrderId,
                    ProductId = productId,
                    Quantity = quantity,
                    AgreedPricePerUnit = agreedPricePerUnit
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(orderRequest, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("Orders/place-order", jsonContent); // Note the "Orders/place-order" endpoint

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var orderConfirmation = await JsonSerializer.DeserializeAsync<OrderConfirmation>(
                        responseStream,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    );
                    return orderConfirmation;
                }
                else
                {
                    Console.WriteLine($"Error placing order with {clientName} for Product {productId}: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return new OrderConfirmation // Return a failed confirmation
                    {
                        GadgetHubOrderId = gadgetHubOrderId,
                        ProductId = productId,
                        DistributorOrderId = "N/A",
                        ConfirmedQuantity = 0,
                        ConfirmedPricePerUnit = 0,
                        EstimatedDeliveryDays = -1,
                        Success = false,
                        Message = $"Failed to place order: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}"
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error placing order with {clientName} for Product {productId}: {ex.Message}");
                return new OrderConfirmation
                {
                    GadgetHubOrderId = gadgetHubOrderId,
                    ProductId = productId,
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = $"Network error: {ex.Message}"
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error from {clientName} order confirmation for Product {productId}: {ex.Message}");
                return new OrderConfirmation
                {
                    GadgetHubOrderId = gadgetHubOrderId,
                    ProductId = productId,
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = $"JSON error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred placing order with {clientName} for Product {productId}: {ex.Message}");
                return new OrderConfirmation
                {
                    GadgetHubOrderId = gadgetHubOrderId,
                    ProductId = productId,
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}
