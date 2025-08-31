using AutoMapper;
using Cozy_Comfort_Distributor.Data;    // Correct namespace for DbContext
using Cozy_Comfort_Distributor.Dtos;    // Correct namespace for Dtos
using Cozy_Comfort_Distributor.Models;  // Correct namespace for Models
using Microsoft.EntityFrameworkCore;

namespace Cozy_Comfort_Distributor.Services // Correct namespace for Services
{
    public class DistributorService : IDistributorService
    {
        private readonly DistributorDbContext _context;
        private readonly IMapper _mapper;

        public DistributorService(DistributorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DistributorDto>> GetAllDistributorsAsync()
        {
            var distributors = await _context.Distributors.ToListAsync();
            return _mapper.Map<IEnumerable<DistributorDto>>(distributors);
        }

        public async Task<DistributorDto?> GetDistributorByIdAsync(int id)
        {
            var distributor = await _context.Distributors.FirstOrDefaultAsync(d => d.Id == id);
            return _mapper.Map<DistributorDto>(distributor);
        }

        public async Task<DistributorDto> CreateDistributorAsync(CreateDistributorDto distributorDto)
        {
            var distributor = _mapper.Map<Distributor>(distributorDto);
            _context.Distributors.Add(distributor);
            await _context.SaveChangesAsync();
            return _mapper.Map<DistributorDto>(distributor);
        }

        public async Task<bool> UpdateDistributorAsync(int id, CreateDistributorDto distributorDto)
        {
            var existingDistributor = await _context.Distributors.FirstOrDefaultAsync(d => d.Id == id);
            if (existingDistributor == null)
            {
                return false;
            }

            _mapper.Map(distributorDto, existingDistributor); // Update properties from DTO
            _context.Entry(existingDistributor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDistributorAsync(int id)
        {
            var distributor = await _context.Distributors.FirstOrDefaultAsync(d => d.Id == id);
            if (distributor == null)
            {
                return false;
            }

            _context.Distributors.Remove(distributor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}