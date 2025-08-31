using Cozy_Comfort_Distributor.Dtos;

namespace Cozy_Comfort_Distributor.Services
{
    public interface IDistributorService
    {
        Task<IEnumerable<DistributorDto>> GetAllDistributorsAsync();
        Task<DistributorDto?> GetDistributorByIdAsync(int id);
        Task<DistributorDto> CreateDistributorAsync(CreateDistributorDto distributorDto);
        Task<bool> UpdateDistributorAsync(int id, CreateDistributorDto distributorDto);
        Task<bool> DeleteDistributorAsync(int id);
    }
}