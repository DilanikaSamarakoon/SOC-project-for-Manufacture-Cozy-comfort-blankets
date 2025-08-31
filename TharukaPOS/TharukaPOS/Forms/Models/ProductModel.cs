using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TharukaPOS.Forms.Models
{
    public class ProductModel
    {
        // Fields
        private int productId;
        private string productName;
        private decimal weight;
        private int quantity;
       
        private decimal buyingPrice;
        private decimal sellingPrice;

        // Properties with Validation

        [DisplayName("Product ID")]
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters")]
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        [DisplayName("Weight (g)")]
        [Required(ErrorMessage = "Weight is required")]
        [Range(1, 1000, ErrorMessage = "Weight must be between 1g and 1000 g")]
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        

        [DisplayName("Buying Price")]
        [Required(ErrorMessage = "Buying price is required")]
        [Range(0.01, 100000, ErrorMessage = "Buying price must be a positive number")]
        public decimal BuyingPrice
        {
            get { return buyingPrice; }
            set { buyingPrice = value; }
        }

        [DisplayName("Selling Price")]
        [Required(ErrorMessage = "Selling price is required")]
        [Range(0.01, 100000, ErrorMessage = "Selling price must be a positive number")]
        public decimal SellingPrice
        {
            get { return sellingPrice; }
            set { sellingPrice = value; }
        }
    }
}
