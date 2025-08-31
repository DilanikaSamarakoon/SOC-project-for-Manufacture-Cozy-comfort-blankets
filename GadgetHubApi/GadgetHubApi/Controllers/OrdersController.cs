using GadgetHubApi.Models;
using GadgetHubApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GadgetHubApi.Controllers
{
    [ApiController]
    [Route("[controller]")] // Base route: /Orders
    public class OrdersController : ControllerBase
    {
        private readonly IQuotationService _quotationService;

        public OrdersController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        /// <summary>
        /// Receives a customer order, requests quotations from distributors, compares them,
        /// and returns a summary of the best fulfillment options.
        /// </summary>
        /// <param name="request">The customer's order request.</param>
        /// <returns>A summary of the order fulfillment plan.</returns>
        [HttpPost("place-order")] // Route: /Orders/place-order
        public async Task<ActionResult<GadgetHubOrderSummary>> PlaceOrder([FromBody] CustomerOrderRequest request)
        {
            if (request == null || !request.Items.Any())
            {
                return BadRequest(new GadgetHubOrderSummary
                {
                    OrderId = request?.OrderId ?? "N/A", // Ensure OrderId is set even if request is null
                    TotalEstimatedCost = 0,
                    OverallStatus = "Failed",
                    OverallMessage = "Invalid order request. Please provide items to order."
                });
            }

            // Process the order through the service layer
            var orderSummary = await _quotationService.ProcessCustomerOrder(request);

            if (orderSummary.OverallStatus == "Failed")
            {
                return StatusCode(500, orderSummary); // Internal server error if overall processing failed
            }

            return Ok(orderSummary);
        }
    }
}
