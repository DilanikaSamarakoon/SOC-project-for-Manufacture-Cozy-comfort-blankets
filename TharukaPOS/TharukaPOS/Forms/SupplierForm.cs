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
using System.Data.SqlClient;

namespace TharukaPOS.Forms
{
    public partial class SupplierForm : Form
    {
        private ISupplierRepository supplierRepository;
        private bool isEditMode = false; // Flag to indicate if we are in Add or Edit mode

        // Parameterless constructor for the designer
        public SupplierForm()
        {
            InitializeComponent();
        }

        // Constructor to receive the connection string from DashboardForm
        public SupplierForm(string connectionString) : this() // Calls the parameterless constructor first
        {
            supplierRepository = new SupplierRepository(connectionString); // Initialize the repository

            // Initial UI state
            DisableInputFields();
            btnAdd.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            // Hook up DataGridView events
            dgvSuppliers.SelectionChanged += dgvSuppliers_SelectionChanged; // When selection changes
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyPress += txtSearch_KeyPress; // Allow pressing Enter for search

            LoadSupplierList(); // Load initial data
        }

        // --- UI State Management ---
        private void EnableInputFields()
        {
            txtSupplierName.Enabled = true;
            txtSupplierAddress.Enabled = true;
            txtSupplierPhone.Enabled = true;
            // txtSupplierId enabled state will be managed in btnAdd/btnEdit
        }

        private void DisableInputFields()
        {
            txtSupplierId.Enabled = false; // Always disable after save/cancel or initial load
            txtSupplierName.Enabled = false;
            txtSupplierAddress.Enabled = false;
            txtSupplierPhone.Enabled = false;
        }

        private void ClearInputFields()
        {
            txtSupplierId.Text = "";
            txtSupplierName.Text = "";
            txtSupplierAddress.Text = "";
            txtSupplierPhone.Text = "";
        }

        // --- Data Loading and Display ---
        private void LoadSupplierList()
        {
            try
            {
                dgvSuppliers.DataSource = supplierRepository.GetAll().ToList(); // Convert to List for DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSupplierList(string searchValue)
        {
            try
            {
                dgvSuppliers.DataSource = supplierRepository.GetByValue(searchValue).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- DataGridView Events ---
        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count > 0)
            {
                // Load selected row data into text boxes (only when not in add/edit mode)
                if (!isEditMode) // Only auto-fill if not currently adding or editing
                {
                    DataGridViewRow selectedRow = dgvSuppliers.SelectedRows[0];
                    txtSupplierId.Text = selectedRow.Cells["SupplierId"].Value.ToString();
                    txtSupplierName.Text = selectedRow.Cells["SupplierName"].Value.ToString();
                    txtSupplierAddress.Text = selectedRow.Cells["SupplierAddress"].Value.ToString();
                    txtSupplierPhone.Text = selectedRow.Cells["SupplierPhone"].Value.ToString();
                }
            }
            else
            {
                // Clear fields if no row is selected, but not if in edit mode
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
            txtSupplierId.Enabled = true; // *** Enable SupplierId for manual entry ***
            txtSupplierId.Focus(); // Set focus to the ID field first
            btnAdd.Enabled = true;
            btnCancel.Enabled = true;
            btnAdd.Enabled = false; // Disable Add, Edit, Delete when in Add/Edit mode
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count > 0)
            {
                isEditMode = true; // It's an Edit operation
                EnableInputFields();
                txtSupplierId.Enabled = false; // *** Disable SupplierId during edit (don't allow changing ID) ***
                // Ensure the selected row data is loaded
                dgvSuppliers_SelectionChanged(this, EventArgs.Empty); // Manually trigger to load current selection
                txtSupplierName.Focus(); // Set focus to the first editable field
                btnAdd.Enabled = true;
                btnCancel.Enabled = true;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a supplier to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int supplierIdToDelete = Convert.ToInt32(dgvSuppliers.SelectedRows[0].Cells["SupplierId"].Value);
                        supplierRepository.Delete(supplierIdToDelete);
                        MessageBox.Show("Supplier deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSupplierList(); // Refresh the grid
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtSupplierId.Text) || // Validate SupplierId
                string.IsNullOrWhiteSpace(txtSupplierName.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierAddress.Text) ||
                string.IsNullOrWhiteSpace(txtSupplierPhone.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSupplierId.Text, out int supplierId))
            {
                MessageBox.Show("Please enter a valid numeric value for Supplier ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Basic validation for phone number (e.g., ensure it's numeric if desired)
            // if (!long.TryParse(txtSupplierPhone.Text, out long phoneNumber)) // if you want to enforce numeric phone
            // {
            //     MessageBox.Show("Please enter a valid numeric value for Supplier Phone.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return;
            // }

            SupplierModel supplier = new SupplierModel
            {
                SupplierId = supplierId, // Assign parsed SupplierId
                SupplierName = txtSupplierName.Text,
                SupplierAddress = txtSupplierAddress.Text,
                SupplierPhone = txtSupplierPhone.Text
            };

            try
            {
                if (!isEditMode) // Add mode
                {
                    supplierRepository.Add(supplier);
                    MessageBox.Show("Supplier added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Edit mode
                {
                    supplierRepository.Update(supplier);
                    MessageBox.Show("Supplier updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadSupplierList(); // Refresh the grid
                ClearInputFields();
                DisableInputFields();
                btnAdd.Enabled = false;
                btnCancel.Enabled = false;
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                isEditMode = false;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL errors, e.g., duplicate primary key for SupplierId
                if (ex.Number == 2627 || ex.Number == 2601) // Primary key violation
                {
                    MessageBox.Show("A supplier with this ID already exists. Please use a unique Supplier ID.", "Duplicate ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error saving supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            DisableInputFields();
            btnAdd.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            isEditMode = false;
            LoadSupplierList(); // Reload to ensure correct selection if any
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                LoadSupplierList(); // If search box is empty, show all
            }
            else
            {
                LoadSupplierList(searchValue);
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
