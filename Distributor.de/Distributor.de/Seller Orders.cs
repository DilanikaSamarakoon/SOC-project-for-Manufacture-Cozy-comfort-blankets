using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistributorDashboard // Corrected namespace
{
    public partial class Seller_Orders : Form
    {
        private readonly ApiService _apiService;

        public Seller_Orders(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            this.Load += new EventHandler(Seller_Orders_Load);
        }

        private async Task LoadOrders()
        {
            List<DistributorOrderDto> orders = await _apiService.GetDistributorOrders();
            // This will now work because the control is declared in the designer file
            dataGridViewSellerOrders.DataSource = orders;
        }

        private async void Seller_Orders_Load(object sender, EventArgs e)
        {
            await LoadOrders();
        }

        private async void BtnRefreshOrders_Click(object sender, EventArgs e)
        {
            await LoadOrders();
        }

        private async void BtnFulfillSelected_Click(object sender, EventArgs e)
        {
            if (dataGridViewSellerOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to fulfill.", "No Order Selected");
                return;
            }

            var selectedOrder = (DistributorOrderDto)dataGridViewSellerOrders.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Are you sure you want to fulfill order #{selectedOrder.Id}?", "Confirm Fulfillment", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool success = await _apiService.FulfillOrderAsync(selectedOrder.Id);
                if (success)
                {
                    MessageBox.Show("Order fulfilled successfully!");
                    await LoadOrders(); // Refresh the list
                }
                else
                {
                    MessageBox.Show("Failed to fulfill the order.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
