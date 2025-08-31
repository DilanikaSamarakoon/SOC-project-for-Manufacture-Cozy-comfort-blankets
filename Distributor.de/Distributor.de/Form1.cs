using Distributor.de;
using DistributorDashboard; // Corrected namespace
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DistributorDashboard // Use the namespace of your new project
{
    public partial class Form1 : Form
    {
        private readonly ApiService _apiService;

        public Form1()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            var allOrders = await _apiService.GetDistributorOrders();
            var allItems = new BindingList<DistributorOrderItemDto>();
            if (allOrders != null)
            {
                foreach (var order in allOrders)
                {
                    order.Items.ForEach(item => allItems.Add(item));
                }
            }
            dgvMyInventory.DataSource = allItems;
            MessageBox.Show("Inventory has been refreshed from the API.");
        }

        private void BtnManufacturerInteraction_Click(object sender, EventArgs e)
        {
            var manufacturerForm = new Manufacturer_Interaction(_apiService);
            manufacturerForm.Show();
        }

        private void BtnSellerOrders_Click(object sender, EventArgs e)
        {
            var sellerOrdersForm = new Seller_Orders(_apiService);
            sellerOrdersForm.Show();
        }
    }
}
