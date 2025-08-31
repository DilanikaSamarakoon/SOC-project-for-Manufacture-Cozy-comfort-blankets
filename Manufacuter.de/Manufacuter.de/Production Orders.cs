// =======================================================================================
// !! FINAL, WORKING VERSION !!
// This code fixes all errors by correctly inheriting from the Form class and
// including all the necessary button click methods.
// =======================================================================================

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

// Make sure this namespace is the same as your other files
namespace Manufacuter.de
{
    // FIX: Added ": Form" to tell the compiler this is a window
    public partial class Production_Orders : Form
    {
        private readonly ManufacturerApiService _apiService = new ManufacturerApiService();

        public Production_Orders()
        {
            InitializeComponent();
        }

        // This method runs automatically when the form first opens
        private void Production_Orders_Load(object sender, EventArgs e)
        {
            Task.Run(() => btnRefreshOrders_Click(null, null));
        }

        // This is the method for your "Refresh" button
        private async void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            try
            {
                var orders = await _apiService.GetOrdersAsync();
                // Use Invoke to safely update the UI from a different thread
                if (dgvOrders.InvokeRequired)
                {
                    dgvOrders.Invoke(new Action(() => dgvOrders.DataSource = orders));
                }
                else
                {
                    dgvOrders.DataSource = orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load orders: {ex.Message}", "Error");
            }
        }

        // This is the method for your "Create Production Order" button
        private async void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBlanketId.Text, out int blanketId) || blanketId <= 0)
            {
                MessageBox.Show("Please enter a valid, positive Blanket ID.");
                return;
            }

            var newOrderDto = new CreateProductionOrderDto
            {
                BlanketId = blanketId,
                Quantity = (int)numQuantity.Value,
                Status = txtStatus.Text
            };

            try
            {
                bool success = await _apiService.CreateOrderAsync(newOrderDto);
                if (success)
                {
                    MessageBox.Show("Production order created successfully!");
                    // Automatically refresh the grid
                    await Task.Run(() => btnRefreshOrders_Click(null, null));
                }
                else
                {
                    MessageBox.Show("Failed to create order. The API reported an error.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }

        // --- Navigation Button Handlers ---

        private void btClickBlanketModels(object sender, EventArgs e)
        {
            BlanketModelsForm blanketModelForm = new BlanketModelsForm();
            blanketModelForm.Show();
        }

        private void btClickStockmanagement(object sender, EventArgs e)
        {
            Stock_Management stockManagementForm = new Stock_Management();
            stockManagementForm.Show();
        }
    }
}
