using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistributorDashboard
{
    public partial class Manufacturer_Interaction : Form
    {
        private readonly ApiService _apiService;

        public Manufacturer_Interaction(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPlaceOrderBlanketId.Text, out int blanketId))
            {
                MessageBox.Show("Please enter a valid Blanket ID for placing an order.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newOrder = new CreateDistributorOrderDto
            {
                DistributorId = 1, 
                Status = "Pending",
                Items = new List<CreateDistributorOrderItemDto>
                {
                    new CreateDistributorOrderItemDto
                    {
                        BlanketId = blanketId,
                        Quantity = (int)numQuantity.Value,
                        Price = 15.0m 
                    }
                }
            };

            var createdOrder = await _apiService.CreateDistributorOrderAsync(newOrder);
            if (createdOrder != null)
            {
                MessageBox.Show($"Successfully placed order #{createdOrder.Id}.", "Order Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

       


        private void btnMyInventory_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnSellerOrders_Click(object sender, EventArgs e)
        {

            var sellerOrdersForm = new Seller_Orders(_apiService);
            sellerOrdersForm.Show();
            this.Close(); 
        }

 

        private void Manufacturer_Interaction_Load(object sender, EventArgs e)
        {

        }

        private async void btnCheckManufacturer_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtCheckStockBlanketId.Text))
            {
                MessageBox.Show("Please enter a Blanket ID to check.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvStockCheck.DataSource = null;
                return;
            }

            
            if (!int.TryParse(txtCheckStockBlanketId.Text, out int blanketId))
            {
                MessageBox.Show("Please enter a valid numeric Blanket ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvStockCheck.DataSource = null;
                return;
            }

            

            try
            {
                
                List<DistributorOrderDto> allOrders = await _apiService.GetDistributorOrders();
                if (allOrders == null)
                {
                    MessageBox.Show("Could not retrieve orders from the API. The result was empty.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                var matchingItems = allOrders
                    .SelectMany(order => order.Items)
                    .Where(item => item.BlanketId == blanketId)
                    .ToList();

                
                if (!matchingItems.Any())
                {
                    MessageBox.Show($"No stock or order history found for Blanket ID: {blanketId}", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvStockCheck.DataSource = null;
                }
                else
                {
                    
                    dgvStockCheck.DataSource = new BindingList<DistributorOrderItemDto>(matchingItems);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
