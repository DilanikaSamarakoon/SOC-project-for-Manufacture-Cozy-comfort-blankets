using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms;
using System.ComponentModel;
using System.Windows.Forms;

namespace TharukaPOS.Forms.Views
{
    public interface ISalesView
    {
        // Properties for UI elements (read/write)
        string DealerName { get; set; }
        string ProductName { get; set; }
        string BuyingPrice { get; set; } // NOW A TEXTBOX
        string SellingPrice { get; set; }
        string Quantity { get; set; }
        string TotalAmount { get; set; } // Display only

        int SelectedDealerId { get; }
        int SelectedProductId { get; }
        int SelectedBillItemIndex { get; } // For edit/delete

        // Events for Presenter to subscribe to
        event EventHandler LoadDealers;
        event EventHandler SelectDealer;
        event EventHandler ChangeDealer;
        event EventHandler LoadProducts;
        event EventHandler SelectProduct;
        event EventHandler AddProductToBill;
        event EventHandler EditBillItem;
        event EventHandler DeleteBillItem;
        event EventHandler SaveSale;
        event EventHandler CancelSale;

        // Methods for Presenter to control View
        void SetDealerListBindingSource(IEnumerable<DealerModel> dealerList); // CHANGED: Dealer to DealerModel
        void SetProductListBindingSource(IEnumerable<ProductModel> productList); // CHANGED: Product to ProductModel
        void SetBillListBindingSource(BindingList<BillItem> billList); // Use BindingList

        void SetDealerInputState(bool enable); // enable/disable dealer textbox and OK/Change buttons
        void SetProductInputState(bool enable); // enable/disable product input fields and Add button
        void SetBillControlsState(bool enable); // enable/disable Edit/Delete buttons on bill
        void SetSaleButtonsState(bool enable); // enable/disable Save Sale / Cancel Sale buttons

        void ClearProductInput();

        // General purpose methods
        void ShowMessage(string message, string title, MessageBoxIcon icon);
        DialogResult ConfirmAction(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon);
        string ShowInputDialog(string prompt, string title, string defaultValue = "");
    }
}
