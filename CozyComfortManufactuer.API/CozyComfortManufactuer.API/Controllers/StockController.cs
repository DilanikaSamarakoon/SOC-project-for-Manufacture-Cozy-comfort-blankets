using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.Manufacturer.API.Data;
using CozyComfort.Manufacturer.API.Models;
using CozyComfort.Manufacturer.API.DTO;
using System.Threading.Tasks;

namespace CozyComfort.Manufacturer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // The constructor injects the database context so the controller can use it.
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the stock information for a specific blanket.
        /// </summary>
        /// <param name="blanketId">The ID of the blanket.</param>
        /// <returns>Stock information for the requested blanket.</returns>
        // GET: api/stock/5
        [HttpGet("{blanketId}")]
        public async Task<ActionResult<Stock>> GetStock(int blanketId)
        {
            // Find the stock item in the database corresponding to the blanketId.
            var stockItem = await _context.Stocks.FirstOrDefaultAsync(s => s.BlanketId == blanketId);

            // If no stock item is found, return a 404 Not Found error.
            if (stockItem == null)
            {
                return NotFound("Stock information not found for the specified blanket ID.");
            }

            // If found, return the stock item with a 200 OK status.
            return Ok(stockItem);
        }

        /// <summary>
        /// Updates the stock quantity and production capacity for a specific blanket.
        /// </summary>
        /// <param name="blanketId">The ID of the blanket to update.</param>
        /// <param name="stockUpdateDto">The new stock information.</param>
        /// <returns>A 204 No Content response if successful.</returns>
        // PUT: api/stock/5
        [HttpPut("{blanketId}")]
        public async Task<IActionResult> UpdateStock(int blanketId, [FromBody] StockUpdateDto stockUpdateDto)
        {
            // Find the existing stock item to update.
            var stockItem = await _context.Stocks.FirstOrDefaultAsync(s => s.BlanketId == blanketId);

            // If no stock item is found, return a 404 Not Found error.
            if (stockItem == null)
            {
                return NotFound("Stock information not found for the specified blanket ID.");
            }

            // Update the properties of the found stock item with the new values.
            stockItem.Quantity = stockUpdateDto.Quantity;
            stockItem.ProductionCapacityPerWeek = stockUpdateDto.ProductionCapacityPerWeek;

            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Return a 204 No Content response to indicate success.
            return NoContent();
        }

        /// <summary>
        /// Creates a new stock entry for a blanket.
        /// </summary>
        /// <param name="stockCreateDto">The stock information for the new entry.</param>
        /// <returns>The newly created stock item.</returns>
        // POST: api/stock
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Stock>> CreateStock([FromBody] StockUpdateDto stockCreateDto) // CORRECTED: Use the specific Create DTO
        {
            // FIX: Add a null check for the incoming data.
            if (stockCreateDto == null)
            {
                return BadRequest("Stock creation data cannot be null.");
            }

            var stockToCreate = new Stock
            {
                BlanketId = stockCreateDto.BlanketId,
                Quantity = stockCreateDto.Quantity,
                ProductionCapacityPerWeek = stockCreateDto.ProductionCapacityPerWeek
            };

            _context.Stocks.Add(stockToCreate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // This correctly handles unique key violations.
                return Conflict("A stock entry for this blanket ID already exists.");
            }
            // It's good practice during development to have a general catch block
            // to see other unexpected errors, though you might remove it in production.
            catch (Exception ex)
            {
                // This will catch other errors (like database connection issues) and prevent a 500 error.
                // You should log the exception 'ex' here.
                return StatusCode(500, $"An unexpected internal error occurred: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetStock), new { blanketId = stockToCreate.BlanketId }, stockToCreate);
        }
        // In StockController.cs

        /// <summary>
        /// Partially updates the stock for a specific blanket. Used here to increase or decrease quantity.
        /// </summary>
        /// <param name="blanketId">The ID of the blanket to update.</param>
        /// <param name="stockQuantityUpdateDto">The data required to update the quantity.</param>
        /// <returns>The updated stock information.</returns>
        // PATCH: api/stock/5
        [HttpPatch("{blanketId}")]
        public async Task<IActionResult> PatchStock(int blanketId, [FromBody] StockQuantityUpdateDto stockQuantityUpdateDto)
        {
            var stockItem = await _context.Stocks.FirstOrDefaultAsync(s => s.BlanketId == blanketId);

            if (stockItem == null)
            {
                return NotFound("Stock information not found for the specified blanket ID.");
            }

            
            if (stockItem.Quantity + stockQuantityUpdateDto.QuantityChange < 0)
            {
                return BadRequest("Update would result in negative stock quantity.");
            }

            // Apply the change
            stockItem.Quantity += stockQuantityUpdateDto.QuantityChange;

            await _context.SaveChangesAsync();

            return Ok(stockItem); // Return the updated object
        }
    }
}