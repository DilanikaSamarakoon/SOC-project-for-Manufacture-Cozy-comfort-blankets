using Microsoft.AspNetCore.Mvc;
using GadgetCentralApi.Models; // Ensure this is GadgetCentralApi.Models
using GadgetCentralApi.Data;   // Ensure this is GadgetCentralApi.Data
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GadgetCentralApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // Base route: /Orders
    public class OrdersController : ControllerBase
    {
        private readonly GadgetCentralDbContext _context; // Use GadgetCentralDbContext

        public OrdersController(GadgetCentralDbContext context) // Inject GadgetCentralDbContext
        {
            _context = context;
        }

        /// <summary>
        /// Receives an order placement request from The GadgetHub, processes it,
        /// and provides an order confirmation. This simulates inventory deduction.
        /// </summary>
        /// <param name="request">The order request from GadgetHub for a specific product.</param>
        /// <returns>An order confirmation with details including a distributor order ID.</returns>
        [HttpPost("place-order")] // Route: /Orders/place-order
        public async Task<ActionResult<OrderConfirmation>> PlaceOrder([FromBody] OrderRequest request)
        {
            // --- Input Validation ---
            if (request == null)
            {
                return BadRequest(new OrderConfirmation
                {
                    GadgetHubOrderId = "",
                    ProductId = "",
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = "Order request body is null."
                });
            }

            if (string.IsNullOrEmpty(request.ProductId) || request.Quantity <= 0)
            {
                return BadRequest(new OrderConfirmation
                {
                    GadgetHubOrderId = request.GadgetHubOrderId,
                    ProductId = request.ProductId ?? "",
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = "Invalid product ID or quantity in order request."
                });
            }

            var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (productInDb == null)
            {
                return NotFound(new OrderConfirmation
                {
                    GadgetHubOrderId = request.GadgetHubOrderId,
                    ProductId = request.ProductId,
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0,
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = $"Product {request.ProductId} not found in Gadget Central inventory."
                });
            }

            if (productInDb.Stock < request.Quantity)
            {
                // Cannot fulfill the entire requested quantity
                return Conflict(new OrderConfirmation
                {
                    GadgetHubOrderId = request.GadgetHubOrderId,
                    ProductId = request.ProductId,
                    DistributorOrderId = "N/A",
                    ConfirmedQuantity = 0, // Or productInDb.Stock if partial fulfillment is allowed
                    ConfirmedPricePerUnit = 0,
                    EstimatedDeliveryDays = -1,
                    Success = false,
                    Message = $"Insufficient stock for {request.ProductId}. Requested: {request.Quantity}, Available: {productInDb.Stock}."
                });
            }

            // --- Simulate Inventory Deduction ---
            productInDb.Stock -= request.Quantity;
            await _context.SaveChangesAsync(); // Persist the stock change

            // --- Calculate Final Price and Delivery (re-confirming logic for Gadget Central) ---
            decimal finalPricePerUnit = productInDb.Price * (1 - productInDb.DiscountPercentage);
            finalPricePerUnit *= 1.07m; // Gadget Central's additional 7% markup

            int estimatedDeliveryDays = productInDb.DeliveryDays;

            // Simulate a short delay for processing
            await Task.Delay(50);

            var distributorOrderId = $"GC-ORDER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"; // Generate a unique order ID for Gadget Central

            return Ok(new OrderConfirmation
            {
                GadgetHubOrderId = request.GadgetHubOrderId,
                ProductId = request.ProductId,
                DistributorOrderId = distributorOrderId,
                ConfirmedQuantity = request.Quantity,
                ConfirmedPricePerUnit = finalPricePerUnit,
                EstimatedDeliveryDays = estimatedDeliveryDays,
                Success = true,
                Message = $"Order for {request.Quantity} units of {request.ProductId} confirmed by Gadget Central."
            });
        }
    }
}
