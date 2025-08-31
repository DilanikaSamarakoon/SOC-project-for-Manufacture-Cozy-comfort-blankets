using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms._Repositories;
using System.Windows.Controls;

namespace TharukaPOS.Forms
{
    public partial class StockForm : Form
    {
        private IBulkRepository bulkRepository;
        private ISupplierRepository supplierRepository;
        private bool isEditMode = false; // Flag to indicate if we are in Add or Edit mode

        // Constructor to receive the connection string from DashboardForm
        public StockForm(string connectionString)
        {
            InitializeComponent();
            bulkRepository = new BulkRepository(connectionString); // Initialize the repository
            supplierRepository = new SupplierRepository(connectionString);

            // Initial UI state
            DisableInputFields();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            // Hook up DataGridView events
            dgvBulkProducts.SelectionChanged += dgvBulkProducts_SelectionChanged; // When selection changes
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyPress += txtSearch_KeyPress; // Allow pressing Enter for search

            LoadSupplierComboBox();
            LoadBulkProductList(); // Load initial data
        }

        private void LoadSupplierComboBox()
        {
            try
            {
                var suppliers = supplierRepository.GetAll().ToList();
                cmbSupplierName.DataSource = suppliers;
                cmbSupplierName.DisplayMember = "SupplierName"; // What the user sees
                cmbSupplierName.ValueMember = "SupplierId";     // The actual value (SupplierId)
                cmbSupplierName.SelectedIndex = -1; // No selection initially
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- UI State Management ---
        private void EnableInputFields()
        {
            txtBulkProductName.Enabled = true;
            cmbSupplierName.Enabled = true;
            txtBulkQuantity.Enabled = true;
            txtPrice.Enabled = true;
            txtBulkProductId.Enabled = true;
        }

        private void DisableInputFields()
        {
            txtBulkProductName.Enabled = false;
            cmbSupplierName.Enabled = false;
            txtBulkQuantity.Enabled = false;
            txtPrice.Enabled = false;
            txtBulkProductId.Enabled = false;
        }

        private void ClearInputFields()
        {
            txtBulkProductId.Text = "";
            txtBulkProductName.Text = "";
            cmbSupplierName.SelectedIndex = -1;
            txtBulkQuantity.Text = "";
            txtPrice.Text = "";
        }

        // --- Data Loading and Display ---
        private void LoadBulkProductList()
        {
            try
            {
                dgvBulkProducts.DataSource = bulkRepository.GetAll().ToList();

                // Optional: Customize DataGridView columns after binding
                if (dgvBulkProducts.Columns.Contains("SupplierId"))
                {
                    dgvBulkProducts.Columns["SupplierId"].Visible = false; // Hide SupplierId
                }
                if (dgvBulkProducts.Columns.Contains("SupplierName"))
                {
                    dgvBulkProducts.Columns["SupplierName"].Visible = true; // Ensure SupplierName is visible
                                                                            // You can also change the display header text if needed
                    dgvBulkProducts.Columns["SupplierName"].HeaderText = "Supplier Name";
                    // And potentially change the display order
                    // dgvBulkProducts.Columns["SupplierName"].DisplayIndex = ...;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bulk products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBulkProductList(string searchValue)
        {
            try
            {
                dgvBulkProducts.DataSource = bulkRepository.GetByValue(searchValue).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching bulk products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- DataGridView Events ---
        private void dgvBulkProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBulkProducts.SelectedRows.Count > 0)
            {
                if (!isEditMode)
                {
                    DataGridViewRow selectedRow = dgvBulkProducts.SelectedRows[0];
                    txtBulkProductId.Text = selectedRow.Cells["BulkProductId"].Value.ToString();
                    txtBulkProductName.Text = selectedRow.Cells["BulkProductName"].Value.ToString();

                    // IMPORTANT: Still use SupplierId to set the ComboBox selected value
                    int supplierId = Convert.ToInt32(selectedRow.Cells["SupplierId"].Value);
                    cmbSupplierName.SelectedValue = supplierId;

                    txtBulkQuantity.Text = selectedRow.Cells["BulkQuantity"].Value.ToString();
                    txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
                }
            }
            else
            {
                if (!isEditMode)
                {
                    ClearInputFields();
                }
            }
        }

        // --- Button Click Events ---

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false; // It's an Add operation, not an Edit
            ClearInputFields();
            EnableInputFields();
            txtBulkProductId.Enabled = true; // *** Enable BulkProductId for manual entry ***
            txtBulkProductId.Focus(); // Focus on the ID field first
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            LoadSupplierComboBox();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBulkProducts.SelectedRows.Count > 0)
            {
                isEditMode = true;
                EnableInputFields();
                // Ensure the selected row data is loaded
                dgvBulkProducts_SelectionChanged(this, EventArgs.Empty);
                txtBulkProductName.Focus();
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a bulk product to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBulkProducts.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this bulk product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int bulkProductId = Convert.ToInt32(dgvBulkProducts.SelectedRows[0].Cells["BulkProductId"].Value);
                        bulkRepository.Delete(bulkProductId);
                        MessageBox.Show("Bulk Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBulkProductList(); // Refresh the grid
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting bulk product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a bulk product to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input validation for all fields
            if (string.IsNullOrWhiteSpace(txtBulkProductId.Text) ||
                string.IsNullOrWhiteSpace(txtBulkProductName.Text) ||
                cmbSupplierName.SelectedValue == null || // CHANGED: Check ComboBox selection
                string.IsNullOrWhiteSpace(txtBulkQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CHANGED: Get SupplierId from ComboBox.SelectedValue
            if (!int.TryParse(txtBulkProductId.Text, out int bulkProductId) ||
                !int.TryParse(txtBulkQuantity.Text, out int bulkQuantity) ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter valid numeric values for Bulk Product ID, Bulk Quantity, and Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get SupplierId from the ComboBox
            int supplierId = (int)cmbSupplierName.SelectedValue; // Get the SupplierId from the selected item

            BulkProductModel bulkProduct = new BulkProductModel
            {
                BulkProductId = bulkProductId,
                BulkProductName = txtBulkProductName.Text,
                SupplierId = supplierId, // Use the ID from the ComboBox
                BulkQuantity = bulkQuantity,
                Price = price
            };

            try
            {
                if (!isEditMode)
                {
                    bulkRepository.Add(bulkProduct);
                    MessageBox.Show("Bulk Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bulkRepository.Update(bulkProduct);
                    MessageBox.Show("Bulk Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadBulkProductList();
                ClearInputFields();
                DisableInputFields();
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                isEditMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving bulk product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            DisableInputFields();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            isEditMode = false;
            LoadBulkProductList(); // Reload to ensure correct selection if any
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                LoadBulkProductList(); // If search box is empty, show all
            }
            else
            {
                LoadBulkProductList(searchValue);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // If Enter key is pressed
            {
                btnSearch_Click(sender, e); // Trigger search button click
                e.Handled = true; // Prevent the 'ding' sound
            }
        }
    }
}
