using AutoMapper;
using CozyComfort.Seller.API.Data;
using CozyComfort.Seller.API.DTOs;
using CozyComfort.Seller.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Seller.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SellerDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(SellerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerOrderDto>>> GetOrders()
        {
            var orders = await _context.SellerOrders
                .Include(o => o.OrderItems)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<SellerOrderDto>>(orders));
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerOrderDto>> GetOrder(int id)
        {
            var order = await _context.SellerOrders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SellerOrderDto>(order));
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<SellerOrderDto>> PostOrder(CreateSellerOrderDto createOrderDto)
        {
            var seller = await _context.Sellers.FindAsync(createOrderDto.SellerId);
            if (seller == null)
            {
                return BadRequest("Invalid Seller ID.");
            }

            var order = _mapper.Map<SellerOrder>(createOrderDto);
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Completed";
            order.TotalAmount = 0;

            foreach (var itemDto in createOrderDto.Items)
            {
                decimal placeholderPrice = 25.50m;
                var orderItem = _mapper.Map<SellerOrderItem>(itemDto);
                orderItem.UnitPrice = placeholderPrice;
                order.OrderItems.Add(orderItem);
                order.TotalAmount += orderItem.Quantity * orderItem.UnitPrice;
            }

            _context.SellerOrders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var orderItem in order.OrderItems)
            {
                var stockItem = await _context.SellerStocks.FirstOrDefaultAsync(s =>
                    s.SellerId == order.SellerId && s.ProductId == orderItem.ProductId);

                if (stockItem != null)
                {
                    stockItem.QuantityInStock += orderItem.Quantity;
                }
                else
                {
                    stockItem = new SellerStock
                    {
                        SellerId = order.SellerId,
                        ProductId = orderItem.ProductId,
                        QuantityInStock = orderItem.Quantity
                    };
                    _context.SellerStocks.Add(stockItem);
                }
            }

            await _context.SaveChangesAsync();

            var orderToReturn = _mapper.Map<SellerOrderDto>(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderToReturn);
        }

        // PUT: api/orders/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto updateOrderStatusDto)
        {
            var order = await _context.SellerOrders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = updateOrderStatusDto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}