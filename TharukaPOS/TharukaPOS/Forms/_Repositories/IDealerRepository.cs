using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;


namespace TharukaPOS.Forms._Repositories
{
    public interface IDealerRepository
    {
        void Add(DealerModel dealer);
        void Update(DealerModel dealer);
        void Delete(int dealerId);
        IEnumerable<DealerModel> GetAll();
        IEnumerable<DealerModel> GetByValue(string value); // For search functionality

    }
}
