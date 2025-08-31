using Microsoft.AspNetCore.Mvc;
using TechWorldApi.Models;
using TechWorldApi.Data; // Import the Data namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // For async database operations

namespace TechWorldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly TechWorldDbContext _context; // Declare a private field for the DbContext

        // Constructor for dependency injection: EF Core will provide an instance of TechWorldDbContext
        public QuotationsController(TechWorldDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles requests for product quotations from The GadgetHub.
        /// This endpoint simulates checking inventory, calculating prices, and estimating delivery.
        /// Data is fetched from the SQL database.
        /// </summary>
        /// <param name="request">A JSON object containing the OrderId and a list of ProductRequests.</param>
        /// <returns>A JSON object with quotation details for each product, including price, availability, and estimated delivery days.</returns>
        [HttpPost("request")]
        public async Task<ActionResult<QuotationResponse>> RequestQuotation([FromBody] QuotationRequest request)
        {
            // --- Input Validation ---
            // If request is null, create a response with a default OrderId
            if (request == null)
            {
                return BadRequest(new QuotationResponse
                {
                    OrderId = "", // Initialize OrderId to an empty string
                    Success = false,
                    Message = "Quotation request body is null. Please provide a valid request."
                });
            }

            // If OrderId is null or empty, create a response with a default OrderId
            if (string.IsNullOrEmpty(request.OrderId))
            {
                return BadRequest(new QuotationResponse
                {
                    OrderId = request.OrderId ?? "", // Ensure OrderId is not null, use empty string if it was null
                    Success = false,
                    Message = "OrderId is required in the quotation request."
                });
            }

            // If Products list is null or empty, create a response using the provided OrderId
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
            // Get a list of all requested ProductIds
            var requestedProductIds = request.Products.Select(p => p.ProductId).ToList();

            // Query the database for these products
            var productsInDb = await _context.Products
                                            .Where(p => requestedProductIds.Contains(p.ProductId))
                                            .ToDictionaryAsync(p => p.ProductId);

            // --- Process Each Product Request ---
            foreach (var productRequest in request.Products)
            {
                if (productsInDb.TryGetValue(productRequest.ProductId, out var productInDb))
                {
                    // --- Simulate Pricing and Availability Logic based on DB data ---
                    // Calculate price after discount
                    decimal pricePerUnit = productInDb.Price * (1 - productInDb.DiscountPercentage); // Now using 'Price'
                    pricePerUnit *= 1.05m; // TechWorld's additional 5% markup

                    int availableUnits = productInDb.Stock;
                    int estimatedDeliveryDays = productInDb.DeliveryDays; // Now using 'DeliveryDays'

                    // If requested quantity exceeds available stock, adjust delivery time
                    if (productRequest.Quantity > availableUnits)
                    {
                        estimatedDeliveryDays += 4; // Longer delivery for backorder/partial fulfillment
                        // Note: For simplicity, we'll still quote the available units.
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
                    // Product not found in TechWorld's inventory (database)
                    response.ProductQuotes.Add(new ProductQuote
                    {
                        ProductId = productRequest.ProductId,
                        PricePerUnit = 0m, // Indicate not available
                        AvailableUnits = 0,
                        EstimatedDeliveryDays = -1 // Indicate not available
                    });
                    response.Success = false; // Mark overall response as partially successful or failed
                    response.Message = "Some products could not be quoted due to unavailability or invalid IDs.";
                }
            }

            // Simulate a small network delay for realism
            await Task.Delay(100);

            // Return an HTTP 200 OK status with the quotation response
            return Ok(response);
        }
    }
}
