// =======================================================================================
// !! FINAL, WORKING VERSION !!
// This code fixes the 'CellContentClick' error by renaming the event handler method
// to match what the form designer expects.
// =======================================================================================

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manufacuter.de // Ensure this namespace is correct
{
    public partial class BlanketModelsForm : Form
    {
        private readonly ManufacturerApiService _apiService = new ManufacturerApiService();
        private int _selectedBlanketId = 0;

        public BlanketModelsForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadBlankets();
        }

        // --- Core Logic Methods ---

        private async Task LoadBlankets()
        {
            ClearFields();
            blanketsDataGridView.DataSource = await _apiService.GetBlanketModelsAsync();
        }

        private void ClearFields()
        {
            txtModelName.Clear();
            txtMaterial.Clear();
            priceNumericUpDown.Value = 0;
            _selectedBlanketId = 0;
            blanketsDataGridView.ClearSelection();
        }

        // --- Event Handlers for Buttons ---

        private async void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtModelName.Text))
            {
                MessageBox.Show("Model Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newModel = new CreateBlanketModelDto
            {
                ModelName = txtModelName.Text,
                Material = txtMaterial.Text,
                Price = priceNumericUpDown.Value
            };

            var createdModel = await _apiService.AddBlanketModelAsync(newModel);

            if (createdModel != null)
            {
                MessageBox.Show("Blanket model added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBlankets();
            }
            else
            {
                MessageBox.Show("Failed to add blanket model. Please check the API.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            if (_selectedBlanketId == 0)
            {
                MessageBox.Show("Please select a blanket from the grid to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedModel = new BlanketModelDto
            {
                Id = _selectedBlanketId,
                ModelName = txtModelName.Text,
                Material = txtMaterial.Text,
                Price = priceNumericUpDown.Value
            };

            bool success = await _apiService.UpdateBlanketModelAsync(_selectedBlanketId, updatedModel);

            if (success)
            {
                MessageBox.Show("Blanket model updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadBlankets();
            }
            else
            {
                MessageBox.Show("Failed to update blanket model.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedBlanketId == 0)
            {
                MessageBox.Show("Please select a blanket from the grid to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this model?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool success = await _apiService.DeleteBlanketModelAsync(_selectedBlanketId);
                if (success)
                {
                    MessageBox.Show("Blanket model deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadBlankets();
                }
                else
                {
                    MessageBox.Show("Failed to delete blanket model.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // FIX: The method has been renamed from 'blanketsDataGridView_CellClick' to 
        // 'blanketsDataGridView_CellContentClick' to match the designer file.
        private void blanketsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.blanketsDataGridView.Rows[e.RowIndex];

                _selectedBlanketId = Convert.ToInt32(row.Cells["Id"].Value);
                txtModelName.Text = row.Cells["ModelName"].Value.ToString();
                txtMaterial.Text = row.Cells["Material"].Value.ToString();
                priceNumericUpDown.Value = Convert.ToDecimal(row.Cells["Price"].Value);
            }
        }

        // --- Event Handlers for Navigation ---

        private void btClickProductionOrders(object sender, EventArgs e)
        {
            Production_Orders productionOrders = new Production_Orders();
            productionOrders.Show();
        }

        private void btClickStockManagement(object sender, EventArgs e)
        {
            Stock_Management stockManagement = new Stock_Management();
            stockManagement.Show();
        }

        private void btClickManageProducts(object sender, EventArgs e)
        {
            ManageProductsForm manageProductsForm = new ManageProductsForm();
            manageProductsForm.Show();
        }
    }
}
