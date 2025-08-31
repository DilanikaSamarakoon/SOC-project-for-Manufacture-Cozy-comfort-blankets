using AutoMapper;
using Cozy_Comfort_Distributor.Data;    // Correct namespace for DbContext
using Cozy_Comfort_Distributor.Dtos;    // Correct namespace for Dtos
using Cozy_Comfort_Distributor.Models;  // Correct namespace for Models
using Microsoft.EntityFrameworkCore;    // CRITICAL: Ensure this is present

namespace Cozy_Comfort_Distributor.Services // Correct namespace for Services
{
    public class DistributorOrderService : IDistributorOrderService
    {
        private readonly DistributorDbContext _context;
        private readonly IMapper _mapper;

        public DistributorOrderService(DistributorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DistributorOrderDto>> GetAllDistributorOrdersAsync()
        {
            var orders = await _context.DistributorOrders
                                       .Include(o => o.Distributor) // Eager load the Distributor
                                       .Include(o => o.Items)!       // Eager load the collection of items
                                                                     // No .ThenInclude(item => item.Blanket) here as Blanket model isn't in this project
                                       .ToListAsync();
            return _mapper.Map<IEnumerable<DistributorOrderDto>>(orders);
        }

        public async Task<DistributorOrderDto?> GetDistributorOrderByIdAsync(int id)
        {
            var order = await _context.DistributorOrders
                                      .Include(o => o.Distributor) // Eager load the Distributor
                                      .Include(o => o.Items)!       // Eager load the collection of items
                                                                    // No .ThenInclude(item => item.Blanket) here
                                      .FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<DistributorOrderDto>(order);
        }

        public async Task<DistributorOrderDto> CreateDistributorOrderAsync(CreateDistributorOrderDto orderDto)
        {
            var distributorOrder = _mapper.Map<DistributorOrder>(orderDto);

            // Calculate TotalAmount based on items
            decimal totalAmount = 0;
            if (orderDto.Items != null)
            {
                foreach (var itemDto in orderDto.Items)
                {
                    totalAmount += itemDto.Quantity * itemDto.UnitPrice;
                }
            }
            distributorOrder.TotalAmount = totalAmount;

            _context.DistributorOrders.Add(distributorOrder);
            await _context.SaveChangesAsync(); // Save the order and its items first

            // --- CRITICAL SECTION: Ensure this is copied EXACTLY ---
            // Manual loading of navigation properties for the returned DTO
            // AFTER the entity has been saved and has its ID.
            // This ensures DistributorName and nested items are populated for the returned DTO.

            // Load Distributor reference
            await _context.Entry(distributorOrder)
                          .Reference(o => o.Distributor) // Correct lambda expression: o => o.Distributor
                          .LoadAsync();

            // Load Items collection
            await _context.Entry(distributorOrder)
                          .Collection(o => o.Items!)     // Correct lambda expression: o => o.Items!
                          .LoadAsync();
            // --- END CRITICAL SECTION ---


            // As explained before, DO NOT try to load item.Blanket here.
            // Blanket is not a navigation property in DistributorOrderItem in this project.
            // If you need Blanket details (like ModelName) for the DTO, you'd fetch them
            // from the Manufacturer API based on BlanketId when constructing the DTO.

            return _mapper.Map<DistributorOrderDto>(distributorOrder);
        }
    }
}