using System;
using System.ComponentModel;
using System.Windows.Forms;
using SellerDashboard; // This namespace must match your other project files like ApiService.cs

namespace Seller.des
{
    // This partial class declaration links this file to the designer file.
    public partial class MyStock : Form
    {
        private readonly ApiService _apiService;

        public MyStock()
        {
            // This call is crucial. It runs the code in the .Designer.cs file
            // to create and set up all your buttons, text boxes, etc.
            InitializeComponent();

            _apiService = new ApiService();
        }

        // This method should be connected to the "Distributor Interaction" button's Click event.
        private void BtnDistributorInteraction_Click(object sender, EventArgs e)
        {
            // Create an instance of the correct form class.
            var distributorInteraction = new Distributor_Interaction(_apiService);
            distributorInteraction.Show();
        }

        // This method should be connected to the "Refresh" button's Click event.
        private async void BtnRefreshMyStock_Click(object sender, EventArgs e)
        {
            // This will now work because dgvMyStock is defined in the designer file.
            var allOrders = await _apiService.GetDistributorOrdersAsync();
            var allItems = new BindingList<DistributorOrderItemDto>();
            if (allOrders != null)
            {
                // This flattens all items from all orders into a single list for the "stock" view.
                foreach (var order in allOrders)
                {
                    order.Items.ForEach(item => allItems.Add(item));
                }
            }
            dgvMyStock.DataSource = allItems;
        }

        private void btClickNewOrderForm(object sender, EventArgs e)
        {
            NewOrderForm newOrderForm = new NewOrderForm();
            newOrderForm.Show();
        }
    }


    namespace Seller.des
    {
        // This is your main form class.
        public partial class Form1 : Form
        {
            // It now knows about BOTH API services.
            private readonly SellerApiService _sellerApiService; // For the new Seller API
            private readonly ApiService _distributorApiService;  // For the original Distributor API

            public Form1()
            {
                // Sets up the UI from the designer file.
                InitializeComponent();

                // Create an instance of each service.
                _sellerApiService = new SellerApiService();
                _distributorApiService = new ApiService();
            }

            // This is connected to your "Refresh My Stock" button.
            private async void btnRefreshMyStock_Click(object sender, EventArgs e)
            {
                this.btnRefreshMyStock.Enabled = false;
                this.btnRefreshMyStock.Text = "Refreshing...";
                try
                {
                    int currentSellerId = 1; // Example Seller ID
                                             // It correctly calls the NEW SellerApiService to get the seller's own stock.
                    var stockItems = await _sellerApiService.GetMyStockAsync(currentSellerId);
                    // The name 'dgvMyStock' must match the control name in your Form1.Designer.cs file.
                    dgvMyStock.DataSource = stockItems;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while connecting to the Seller API: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.btnRefreshMyStock.Enabled = true;
                    this.btnRefreshMyStock.Text = "Refresh My Stock";
                }
            }

            // This is connected to your "Distributor Interaction" button.
            private void btnDistributorInteraction_Click(object sender, EventArgs e)
            {
                // This now correctly passes the distributor's ApiService to the other form.
                // This resolves the "no argument given" error.
                var distributorForm = new Distributor_Interaction(_distributorApiService);
                distributorForm.Show();
            }
        }
    }
}



