using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.Manufacturer.API.Data;
using CozyComfort.Manufacturer.API.Models;
using CozyComfort.Manufacturer.API.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CozyComfort.Manufacturer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products (Gets all products)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5 (Gets a single product by ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products (Creates a new product)
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDto productDto)
        {
            // This is the complete and correct version of the method
            var product = new Product
            {
                Name = productDto.Name,
                Cost = productDto.Cost,
                InventoryCount = productDto.InventoryCount
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // This line now works because the 'product' variable has been created
            // and the 'GetProduct' method exists.
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}