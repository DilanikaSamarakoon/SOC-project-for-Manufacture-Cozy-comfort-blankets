using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;

namespace TharukaPOS.Forms._Repositories
{
    public interface ISupplierRepository
    {
        void Add(SupplierModel supplier);
        void Update(SupplierModel supplier);
        void Delete(int supplierId);
        IEnumerable<SupplierModel> GetAll();
        IEnumerable<SupplierModel> GetByValue(string value); // For search functionality
    }
}
