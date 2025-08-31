using Microsoft.AspNetCore.Mvc;
using Cozy_Comfort_Distributor.Dtos;    // Correct DTOs namespace
using Cozy_Comfort_Distributor.Services; // Correct Services namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cozy_Comfort_Distributor.Controllers // Correct Controllers namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorService _distributorService;

        public DistributorsController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        // GET: api/Distributors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributorDto>>> GetDistributors()
        {
            var distributors = await _distributorService.GetAllDistributorsAsync();
            return Ok(distributors);
        }

        // GET: api/Distributors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DistributorDto>> GetDistributor(int id)
        {
            var distributor = await _distributorService.GetDistributorByIdAsync(id);
            if (distributor == null)
            {
                return NotFound();
            }
            return Ok(distributor);
        }

        // POST: api/Distributors
        [HttpPost]
        public async Task<ActionResult<DistributorDto>> PostDistributor([FromBody] CreateDistributorDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDistributor = await _distributorService.CreateDistributorAsync(request);
            // Return 201 Created status and the new resource location
            return CreatedAtAction(nameof(GetDistributor), new { id = createdDistributor.Id }, createdDistributor);
        }

        // PUT: api/Distributors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistributor(int id, [FromBody] CreateDistributorDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _distributorService.UpdateDistributorAsync(id, request);
            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content for successful update
        }

        // DELETE: api/Distributors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistributor(int id)
        {
            var success = await _distributorService.DeleteDistributorAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content for successful deletion
        }
    }
}