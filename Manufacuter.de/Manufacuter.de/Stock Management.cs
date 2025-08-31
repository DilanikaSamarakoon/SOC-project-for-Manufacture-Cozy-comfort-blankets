// =======================================================================================
// !! FINAL, WORKING VERSION !!
// This code fixes the missing method errors by adding btnProductionOrders_Click_1 and
// groupBox1_Enter. Please replace the code in your 'Stock_Management.cs' file 
// and then Rebuild your solution.
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manufacuter.de // Make sure this is the same as your other forms
{
    public partial class Stock_Management : Form
    {
        private static readonly HttpClient httpClient;

        // Static constructor initializes the HttpClient once for the entire application
        static Stock_Management()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7212");
        }

        public Stock_Management()
        {
            InitializeComponent();
        }

        // This method runs automatically when the form opens
        private async void Stock_Management_Load(object sender, EventArgs e)
        {
            await LoadBlanketModels();
        }

        #region Model Classes for this Form
        public class Blanket
        {
            public int Id { get; set; }
            public string ModelName { get; set; }
        }

        public class Stock
        {
            public int Id { get; set; }
            public int BlanketId { get; set; }
            public int Quantity { get; set; }
            public int ProductionCapacityPerWeek { get; set; }
        }

        public class StockCreateDto
        {
            public int BlanketId { get; set; }
            public int Quantity { get; set; }
            public int ProductionCapacityPerWeek { get; set; }
        }
        #endregion

        #region Navigation Button Clicks
        private void btnBlanketModels_Click(object sender, EventArgs e)
        {
            using (var blanketForm = new BlanketModelsForm())
            {
                blanketForm.ShowDialog();
            }
        }

        private void btnProductionOrders_Click(object sender, EventArgs e)
        {
            using (var productionOrdersForm = new Production_Orders())
            {
                productionOrdersForm.ShowDialog();
            }
        }

        // FIX: This method is now present to match the designer file.
        private void btnProductionOrders_Click_1(object sender, EventArgs e)
        {
            // It simply calls the other, cleaner method to avoid duplicate code.
            btnProductionOrders_Click(sender, e);
        }
        #endregion

        #region Stock Control Logic
        private async void cmbStockBlanket_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStockBlanket.SelectedValue is int blanketId)
            {
                await LoadStockDetails(blanketId);
            }
        }

        private async void btnIncreaseStock_Click(object sender, EventArgs e)
        {
            await UpdateStock(true);
        }

        private async void btnDecreaseStock_Click(object sender, EventArgs e)
        {
            await UpdateStock(false);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            await AddNewStockItem();
        }
        #endregion

        #region Private Helper Methods (API Calls)

        private async Task LoadBlanketModels()
        {
            try
            {
                var blankets = await httpClient.GetFromJsonAsync<List<Blanket>>("/api/Blankets");
                cmbStockBlanket.DataSource = blankets;
                cmbStockBlanket.DisplayMember = "ModelName";
                cmbStockBlanket.ValueMember = "Id";

                if (cmbStockBlanket.Items.Count > 0)
                {
                    cmbStockBlanket.SelectedIndex = 0;
                    if (cmbStockBlanket.SelectedValue is int blanketId)
                    {
                        await LoadStockDetails(blanketId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load blanket models: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadStockDetails(int blanketId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"/api/Stock/{blanketId}");
                if (response.IsSuccessStatusCode)
                {
                    var stock = await response.Content.ReadFromJsonAsync<Stock>();
                    dgvStock.DataSource = new List<Stock> { stock };
                }
                else
                {
                    dgvStock.DataSource = null; // Clear the grid if stock not found
                }
            }
            catch (Exception ex)
            {
                dgvStock.DataSource = null;
                MessageBox.Show($"Failed to load stock details: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateStock(bool isIncrease)
        {
            if (!(cmbStockBlanket.SelectedValue is int blanketId))
            {
                MessageBox.Show("Please select a blanket.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HttpResponseMessage getResponse = await httpClient.GetAsync($"/api/Stock/{blanketId}");
                if (!getResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Stock data not found. Use the 'Add' button to create it first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var currentStock = await getResponse.Content.ReadFromJsonAsync<Stock>();
                int quantityChange = (int)numStockChange.Value;
                int newTotalQuantity = currentStock.Quantity + (isIncrease ? quantityChange : -quantityChange);

                if (newTotalQuantity < 0)
                {
                    MessageBox.Show("Stock cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var stockUpdateData = new
                {
                    Quantity = newTotalQuantity,
                    ProductionCapacityPerWeek = currentStock.ProductionCapacityPerWeek
                };

                HttpResponseMessage putResponse = await httpClient.PutAsJsonAsync($"/api/Stock/{blanketId}", stockUpdateData);
                if (putResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Stock updated successfully!", "Success");
                    await LoadStockDetails(blanketId);
                }
                else
                {
                    string errorMessage = await putResponse.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error updating stock: {putResponse.StatusCode}\n{errorMessage}", "API Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An application error occurred: {ex.Message}", "Error");
            }
        }

        private async Task AddNewStockItem()
        {
            if (!(cmbStockBlanket.SelectedValue is int blanketId))
            {
                MessageBox.Show("Please select a blanket to add to stock.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtProductionCap.Text))
            {
                MessageBox.Show("Please enter a production capacity.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HttpResponseMessage getResponse = await httpClient.GetAsync($"/api/Stock/{blanketId}");
                if (getResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Stock for this blanket already exists. Please use the 'Increase' or 'Decrease' buttons.", "Item Exists");
                    return;
                }

                if (getResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var newStockData = new StockCreateDto
                    {
                        BlanketId = blanketId,
                        Quantity = (int)numStockChange.Value,
                        ProductionCapacityPerWeek = int.Parse(txtProductionCap.Text.Trim())
                    };

                    HttpResponseMessage postResponse = await httpClient.PostAsJsonAsync("/api/stock", newStockData);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("New stock item added successfully!", "Success");
                        await LoadStockDetails(blanketId);
                    }
                    else
                    {
                        string errorMessage = await postResponse.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error adding stock: {postResponse.StatusCode}\n{errorMessage}", "API Error");
                    }
                }
                else
                {
                    MessageBox.Show("An error occurred while checking for existing stock.", "API Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An application error occurred: {ex.Message}", "Error");
            }
        }

        // FIX: This method is now present to match the designer file. It can be left empty.
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // No action needed here, but the method must exist.
        }

        #endregion
    }
}
