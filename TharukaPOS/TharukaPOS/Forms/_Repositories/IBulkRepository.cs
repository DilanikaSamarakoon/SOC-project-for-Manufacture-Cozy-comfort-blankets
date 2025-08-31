using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;

namespace TharukaPOS.Forms._Repositories
{
    public interface IBulkRepository
    {
        void Add(BulkProductModel bulkProduct);
        void Update(BulkProductModel bulkProduct);
        void Delete(int bulkProductId);
        IEnumerable<BulkProductModel> GetAll();
        IEnumerable<BulkProductModel> GetByValue(string value); // For search functionality
        IEnumerable<BulkProductModel> GetAllBulkProducts();
        IEnumerable<BulkProductModel> SearchBulkProducts(string searchTerm);
    }
}
