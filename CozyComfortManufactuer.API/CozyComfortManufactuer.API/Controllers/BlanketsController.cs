
using CozyComfort.Manufacturer.API.Models;
using CozyComfort.Manufacturer.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CozyComfort.Manufacturer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlanketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public BlanketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Blankets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blanket>>> GetBlankets()
        {
            return await _context.Blankets.ToListAsync();
        }

        // POST: api/Blankets
        [HttpPost]
        public async Task<ActionResult<Blanket>> CreateBlanket(Blanket blanket)
        {


            var newBlanket = new Blanket
            {
                
                ModelName = blanket.ModelName,
                Material = blanket.Material,
                Price = blanket.Price
                
            };

            
            _context.Blankets.Add(newBlanket);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetBlanket), new { id = newBlanket.Id }, newBlanket);
        }

        // GET: api/Blankets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blanket>> GetBlanket(int id)
        {
            var blanket = await _context.Blankets.FindAsync(id);

            if (blanket == null)
            {
                return NotFound();
            }

            return blanket;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlanket(int id, Blanket blanket)
        {
            
            if (id != blanket.Id)
            {
                return BadRequest();
            }

            
            _context.Entry(blanket).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!BlanketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
            return NoContent();
        }

        
        private bool BlanketExists(int id)
        {
            return _context.Blankets.Any(e => e.Id == id);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlanket(int id)
        {
            
            var blanket = await _context.Blankets.FindAsync(id);
            if (blanket == null)
            {
                
                return NotFound();
            }

            
            _context.Blankets.Remove(blanket);
            
            await _context.SaveChangesAsync();

            
            return NoContent();
        }
    }
}
