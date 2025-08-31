using Cozy_Comfort_Distributor.Dtos;

namespace Cozy_Comfort_Distributor.Services
{
    public interface IDistributorOrderService
    {
        Task<IEnumerable<DistributorOrderDto>> GetAllDistributorOrdersAsync();
        Task<DistributorOrderDto?> GetDistributorOrderByIdAsync(int id);
        Task<DistributorOrderDto> CreateDistributorOrderAsync(CreateDistributorOrderDto orderDto);
        // Add Update/Delete as needed
    }
}