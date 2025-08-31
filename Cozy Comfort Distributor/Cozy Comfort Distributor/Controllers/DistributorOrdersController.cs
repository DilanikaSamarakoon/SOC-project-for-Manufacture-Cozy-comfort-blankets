using Microsoft.AspNetCore.Mvc;
using Cozy_Comfort_Distributor.Dtos;    // Correct DTOs namespace
using Cozy_Comfort_Distributor.Services; // Correct Services namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cozy_Comfort_Distributor.Controllers // Correct Controllers namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorOrdersController : ControllerBase
    {
        private readonly IDistributorOrderService _distributorOrderService;
        private readonly IDistributorService _distributorService;

        public DistributorOrdersController(
            IDistributorOrderService distributorOrderService,
            IDistributorService distributorService)
        {
            _distributorOrderService = distributorOrderService;
            _distributorService = distributorService;
        }

        // GET: api/DistributorOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributorOrderDto>>> GetDistributorOrders()
        {
            var orders = await _distributorOrderService.GetAllDistributorOrdersAsync();
            return Ok(orders);
        }

        // GET: api/DistributorOrders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DistributorOrderDto>> GetDistributorOrder(int id)
        {
            var order = await _distributorOrderService.GetDistributorOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        
        [HttpPost]
        public async Task<ActionResult<DistributorOrderDto>> PostDistributorOrder([FromBody] CreateDistributorOrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var distributorExists = await _distributorService.GetDistributorByIdAsync(request.DistributorId);
            if (distributorExists == null)
            {
                return BadRequest($"Distributor with ID {request.DistributorId} does not exist.");
            }

          

            var createdOrder = await _distributorOrderService.CreateDistributorOrderAsync(request);
            
            return CreatedAtAction(nameof(GetDistributorOrder), new { id = createdOrder.Id }, createdOrder);
        }
        
    }
}