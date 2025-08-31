using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TharukaPOS.Forms.Views
{
    public partial class ProductView : Form, IProductView
    {
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        public ProductView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPageProductDetails);
        }

        //Constructor
        private void AssociateAndRaiseViewEvents()
        {
            //Search 
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearchProduct.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //add new
            btnAdd.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageProductList);
                tabControl1.TabPages.Add(tabPageProductDetails);
                tabPageProductDetails.Text = "Add New Product";
            };

            //edit
            btnEdit.Click += delegate 
            { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageProductList);
                tabControl1.TabPages.Add(tabPageProductDetails);
                tabPageProductDetails.Text = "Edit Product";

            };

            //save changes
            btnSave.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if(isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPageProductDetails);
                    tabControl1.TabPages.Add(tabPageProductList);
                }
                MessageBox.Show(Message);
            };

            //cancel
            btnCancel.Click += delegate 
            { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageProductDetails);
                tabControl1.TabPages.Add(tabPageProductList);
            };

            //delete
            btnDelete.Click += delegate 
            {  
                var result = MessageBox.Show("Are you sure you want to delete the selected product?",
                             "Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }


        //Properties
        public string ProductId 
        { 
            get { return txtProductId.Text; }
            set { txtProductId.Text = value; }
        }

        public string ProductTitle
        {
            get { return txtProductName.Text; }
            set { txtProductName.Text = value; }
        }
        public string Weight 
        { 
            get { return txtWeight.Text; }
            set { txtWeight.Text = value; }
        }
        public string Quantity 
        { 
            get { return txtQuantity.Text; }
            set { txtQuantity.Text = value; }
        }
       
        public string BuyingPrice 
        { 
            get { return txtBuyingPrice.Text; }
            set {  txtBuyingPrice.Text = value; }
        }
        public string SellingPrice 
        { 
            get { return txtSellingPrice.Text; }
            set {  txtSellingPrice.Text = value; } 
        }
        public string SearchValue
        {
            get { return txtSearchProduct.Text; }
            set { txtSearchProduct.Text = value; }
        }
        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        


        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;


        //Methods
        public void SetProductListBindingSource(BindingSource productList)
        {
            dataGridViewProducts2.DataSource = productList;
        }

       
    }
}
