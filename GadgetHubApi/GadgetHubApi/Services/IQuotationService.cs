using GadgetHubApi.Models;
using System.Threading.Tasks;

namespace GadgetHubApi.Services
{
    public interface IQuotationService
    {
        Task<GadgetHubOrderSummary> ProcessCustomerOrder(CustomerOrderRequest customerOrder);
    }
}
