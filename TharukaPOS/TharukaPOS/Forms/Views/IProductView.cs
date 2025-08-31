using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TharukaPOS.Forms.Views
{
    public interface IProductView
    {
        //Properties - Fields
        string ProductId { get; set; }
        string ProductTitle { get; set; }
        string Weight { get; set; }
        string Quantity { get; set; }
       
        string BuyingPrice { get; set; }
        string SellingPrice { get; set; }



        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetProductListBindingSource(BindingSource productList);
        void Show();
    }
}
