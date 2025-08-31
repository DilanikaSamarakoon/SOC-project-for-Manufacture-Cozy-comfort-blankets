using AutoMapper;
using CozyComfort.Seller.API.Data;
using CozyComfort.Seller.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Seller.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly SellerDbContext _context;
        private readonly IMapper _mapper;

        public SellersController(SellerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerDto>>> GetSellers()
        {
            var sellers = await _context.Sellers.ToListAsync();
            var sellerDtos = _mapper.Map<IEnumerable<SellerDto>>(sellers);
            return Ok(sellerDtos);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> GetSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SellerDto>(seller));
        }

        // In SellersController.cs

        // The method now accepts the new DTO without an Id field
        [HttpPost]
        public async Task<ActionResult<SellerDto>> PostSeller(CreateSellerDto createSellerDto)
        {
            // Map from the CreateSellerDto to the Seller entity
            var seller = _mapper.Map<Models.Seller>(createSellerDto);

            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync(); // This will no longer throw the exception

            // Map the final entity (with its new DB-generated Id) back to the full SellerDto
            var sellerToReturn = _mapper.Map<SellerDto>(seller);

            return CreatedAtAction(nameof(GetSeller), new { id = sellerToReturn.Id }, sellerToReturn);
        }


        [HttpGet("{id}/stock")]
        public async Task<ActionResult<IEnumerable<SellerStockDto>>> GetSellerStock(int id)
        {
            
            var sellerExists = await _context.Sellers.AnyAsync(s => s.Id == id);
            if (!sellerExists)
            {
                return NotFound($"Seller with ID {id} not found.");
            }

            var stock = await _context.SellerStocks
                .Where(s => s.SellerId == id)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<SellerStockDto>>(stock));
        }
    }
}