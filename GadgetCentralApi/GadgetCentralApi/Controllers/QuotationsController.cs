using Microsoft.AspNetCore.Mvc;
using GadgetCentralApi.Models; // Changed for Gadget Central
using GadgetCentralApi.Data; // Changed for Gadget Central
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetCentralApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly GadgetCentralDbContext _context; // Changed for Gadget Central

        public QuotationsController(GadgetCentralDbContext context) // Changed for Gadget Central
        {
            _context = context;
        }

        /// <summary>
        /// Handles requests for product quotations from The GadgetHub for Gadget Central.
        /// This endpoint simulates checking inventory, calculating prices, and estimating delivery.
        /// Data is fetched from the SQL database.
        /// </summary>
        /// <param name="request">A JSON object containing the OrderId and a list of ProductRequests.</param>
        /// <returns>A JSON object with quotation details for each product, including price, availability, and estimated delivery days.</returns>
        [HttpPost("request")]
        public async Task<ActionResult<QuotationResponse>> RequestQuotation([FromBody] QuotationRequest request)
        {
            // --- Input Validation ---
            if (request == null)
            {
                return BadRequest(new QuotationResponse
                {
                    OrderId = "", // Initialize OrderId for error response
                    Success = false,
                    Message = "Quotation request body is null. Please provide a valid request."
                });
            }

            if (string.IsNullOrEmpty(request.OrderId))
            {
                return BadRequest(new QuotationResponse
                {
                    OrderId = request.OrderId ?? "", // Ensure OrderId is not null, use empty string if it was null
                    Success = false,
                    Message = "OrderId is required in the quotation request."
                });
            }

            if (request.Products == null || !request.Products.Any())
            {
                return BadRequest(new QuotationResponse
                {
                    OrderId = request.OrderId, // OrderId is guaranteed to be non-null here
                    Success = false,
                    Message = "Products list is empty or null. Please provide products to quote."
                });
            }

            var response = new QuotationResponse
            {
                OrderId = request.OrderId,
                Success = true,
                Message = "Quotation generated successfully."
            };

            // --- Fetch all requested products from the database in one go for efficiency ---
            var requestedProductIds = request.Products.Select(p => p.ProductId).ToList();
            var productsInDb = await _context.Products
                                            .Where(p => requestedProductIds.Contains(p.ProductId))
                                            .ToDictionaryAsync(p => p.ProductId);

            // --- Process Each Product Request ---
            foreach (var productRequest in request.Products)
            {
                if (productsInDb.TryGetValue(productRequest.ProductId, out var productInDb))
                {
                    // --- Simulate Pricing and Availability Logic for Gadget Central ---
                    // Calculate price after discount
                    decimal pricePerUnit = productInDb.Price * (1 - productInDb.DiscountPercentage); // Now using 'Price'
                    pricePerUnit *= 1.07m; // Gadget Central's additional 7% markup

                    int availableUnits = productInDb.Stock;
                    int estimatedDeliveryDays = productInDb.DeliveryDays; // Now using 'DeliveryDays'

                    if (productRequest.Quantity > availableUnits)
                    {
                        estimatedDeliveryDays += 4; // Longer delivery for backorder/partial fulfillment
                    }

                    response.ProductQuotes.Add(new ProductQuote
                    {
                        ProductId = productRequest.ProductId,
                        PricePerUnit = pricePerUnit,
                        AvailableUnits = availableUnits,
                        EstimatedDeliveryDays = estimatedDeliveryDays
                    });
                }
                else
                {
                    // Product not found in Gadget Central's inventory
                    response.ProductQuotes.Add(new ProductQuote
                    {
                        ProductId = productRequest.ProductId,
                        PricePerUnit = 0m,
                        AvailableUnits = 0,
                        EstimatedDeliveryDays = -1
                    });
                    response.Success = false;
                    response.Message = "Some products could not be quoted due to unavailability or invalid IDs.";
                }
            }

            await Task.Delay(100); // Simulate network delay

            return Ok(response);
        }
    }
}
