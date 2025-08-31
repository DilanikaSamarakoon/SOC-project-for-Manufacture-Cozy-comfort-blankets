using Seller.des;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SellerDashboard
{
    public partial class Distributor_Interaction : Form
    {
        private readonly ApiService _apiService;

        public Distributor_Interaction(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;

            // It's good practice to wire up event handlers in the constructor
            // after InitializeComponent()
            this.Load += new System.EventHandler(this.Distributor_Interaction_Load);
        }

        private async void Distributor_Interaction_Load(object sender, EventArgs e)
        {
            // Load the order history when the form opens
            await LoadOrderHistory();
        }

        private async Task LoadOrderHistory()
        {
            // This will now work because dgvOrdersHistory is declared in the designer file
            List<DistributorOrderDto> allOrders = await _apiService.GetDistributorOrdersAsync();
            dgvOrdersHistory.DataSource = new BindingList<DistributorOrderDto>(allOrders);
        }

        // --- Event Handlers for your buttons (Corrected Naming) ---

        private async void BtnCheckStock_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCheckStockBlanketId.Text, out int blanketId))
            {
                MessageBox.Show("Please enter a valid Blanket ID to check.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvCheckStockResult.DataSource = null;
                return;
            }

            List<DistributorOrderDto> allOrders = await _apiService.GetDistributorOrdersAsync();
            if (allOrders == null) return;

            var matchingItems = allOrders
                .SelectMany(order => order.Items)
                .Where(item => item.BlanketId == blanketId)
                .ToList();

            if (!matchingItems.Any())
            {
                MessageBox.Show($"No stock found for Blanket ID: {blanketId}", "Not Found");
                dgvCheckStockResult.DataSource = null;
            }
            else
            {
                dgvCheckStockResult.DataSource = new BindingList<DistributorOrderItemDto>(matchingItems);
            }
        }

        private async void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPlaceOrderBlanketId.Text, out int blanketId))
            {
                MessageBox.Show("Please enter a valid Blanket ID to place an order.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newOrder = new CreateDistributorOrderDto
            {
                DistributorId = 2, // Using a different ID for the seller
                Status = "Pending",
                Items = new List<CreateDistributorOrderItemDto>
                {
                    new CreateDistributorOrderItemDto
                    {
                        BlanketId = blanketId,
                        Quantity = (int)numQuantity.Value,
                        Price = 25.0m // Example price
                    }
                }
            };

            var createdOrder = await _apiService.CreateDistributorOrderAsync(newOrder);
            if (createdOrder != null)
            {
                MessageBox.Show($"Successfully placed order #{createdOrder.Id}.", "Order Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadOrderHistory(); // Refresh the order history
            }
        }

        private void BtnMyStock_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btClickbtnNewOrderForm(object sender, EventArgs e)
        {
            NewOrderForm newOrderForm = new NewOrderForm();
            newOrderForm.Show();
        }
    }
}
