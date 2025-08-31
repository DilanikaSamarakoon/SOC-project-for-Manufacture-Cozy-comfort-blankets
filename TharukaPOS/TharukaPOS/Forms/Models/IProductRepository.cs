using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TharukaPOS.Forms.Models
{
    public interface IProductRepository
    {
        
            void Add(ProductModel productModel);
            void Edit(ProductModel productModel);
            void Delete(int id);
            IEnumerable<ProductModel> GetAll();
            IEnumerable<ProductModel> GetByValue(string value);//Searchs

        IEnumerable<ProductModel> GetLowStockProducts(int threshold);

        // NEW METHOD: Update product stock
        // quantityChange will be negative for a sale (decrease)
        // Returns true if update was successful (e.g., sufficient stock), false otherwise
        bool UpdateStock(int productId, int quantityChange);

        // NEW METHOD: Get current stock for a product
        int GetStock(int productId);
    }
}
