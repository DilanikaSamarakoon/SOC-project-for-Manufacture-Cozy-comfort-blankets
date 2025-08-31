using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TharukaPOS.Forms._Repositories
{
    public interface IReportRepository
    {
        DataTable GetDailySalesSummary(DateTime reportDate);
        DataTable GetCurrentStockLevels();
        DataTable GetProductProfit(DateTime startDate, DateTime endDate);
        DataTable GetDealerHistory(int dealerId);
        // Add declarations for all other report methods here
        DataTable GetProductSales(DateTime startDate, DateTime endDate, string productName);
    }
}
