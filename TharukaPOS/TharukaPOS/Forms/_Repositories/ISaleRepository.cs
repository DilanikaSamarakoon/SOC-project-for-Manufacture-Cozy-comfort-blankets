using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;

namespace TharukaPOS.Forms._Repositories
{
    public interface ISaleRepository
    {
        // Method to save an entire sale, including its details
        void SaveSale(SaleModel sale);
        // You might add methods to GetSaleById, GetSalesByDateRange, etc. later

        IEnumerable<SaleDetailModel> GetSaleDetailsByDealerAndDate(int dealerId, DateTime saleDate);

        // New method for InvoiceForm: Get Dealers who made sales on a specific date
        IEnumerable<DealerModel> GetDealersBySaleDate(DateTime saleDate, string searchName = null);
    }
}
