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
    public partial class DealerForm : Form
    {
        private IDealerRepository dealerRepository;
        private bool isEditMode = false; // Flag to indicate if we are in Add or Edit mode

        // Constructor to receive the connection string from DashboardForm
        public DealerForm(string connectionString) 
        {
            InitializeComponent();
            dealerRepository = new DealerRepository(connectionString); // Initialize the repository

            // Initial UI state
            DisableInputFields();
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            // Hook up DataGridView events
            dgvDealers.SelectionChanged += dgvDealers_SelectionChanged; // When selection changes
            btnSearch.Click += btnSearch_Click;
            txtSearch.KeyPress += txtSearch_KeyPress; // Allow pressing Enter for search

            LoadDealerList(); // Load initial data
        }

        // --- UI State Management ---
        private void EnableInputFields()
        {
            txtDealerName.Enabled = true;
            txtDealerAddress.Enabled = true;
            txtPhone.Enabled = true;
            // txtDealerId enabled state will be managed in btnAdd/btnEdit
        }

        private void DisableInputFields()
        {
            txtDealerId.Enabled = false; // Always disable after save/cancel or initial load
            txtDealerName.Enabled = false;
            txtDealerAddress.Enabled = false;
            txtPhone.Enabled = false;
        }

        private void ClearInputFields()
        {
            txtDealerId.Text = "";
            txtDealerName.Text = "";
            txtDealerAddress.Text = "";
            txtPhone.Text = "";
        }

        // --- Data Loading and Display ---
        private void LoadDealerList()
        {
            try
            {
                dgvDealers.DataSource = dealerRepository.GetAll().ToList(); // Convert to List for DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dealers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDealerList(string searchValue)
        {
            try
            {
                dgvDealers.DataSource = dealerRepository.GetByValue(searchValue).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching dealers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- DataGridView Events ---
        private void dgvDealers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDealers.SelectedRows.Count > 0)
            {
                // Load selected row data into text boxes (only when not in add/edit mode)
                if (!isEditMode) // Only auto-fill if not currently adding or editing
                {
                    DataGridViewRow selectedRow = dgvDealers.SelectedRows[0];
                    txtDealerId.Text = selectedRow.Cells["DealerId"].Value.ToString();
                    txtDealerName.Text = selectedRow.Cells["DealerName"].Value.ToString();
                    txtDealerAddress.Text = selectedRow.Cells["DealerAddress"].Value.ToString();
                    txtPhone.Text = selectedRow.Cells["DealerPhone"].Value.ToString(); // Note: Model uses DealerPhone
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
            txtDealerId.Enabled = true; // *** Enable DealerId for manual entry ***
            txtDealerId.Focus(); // Set focus to the ID field first
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnAdd.Enabled = false; // Disable Add, Edit, Delete when in Add/Edit mode
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDealers.SelectedRows.Count > 0)
            {
                isEditMode = true; // It's an Edit operation
                EnableInputFields();
                txtDealerId.Enabled = false; // *** Disable DealerId during edit (don't allow changing ID) ***
                // Ensure the selected row data is loaded
                dgvDealers_SelectionChanged(this, EventArgs.Empty); // Manually trigger to load current selection
                txtDealerName.Focus(); // Set focus to the first editable field
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please select a dealer to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDealers.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this dealer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int dealerIdToDelete = Convert.ToInt32(dgvDealers.SelectedRows[0].Cells["DealerId"].Value);
                        dealerRepository.Delete(dealerIdToDelete);
                        MessageBox.Show("Dealer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDealerList(); // Refresh the grid
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting dealer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a dealer to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtDealerId.Text) || // Validate DealerId
                string.IsNullOrWhiteSpace(txtDealerName.Text) ||
                string.IsNullOrWhiteSpace(txtDealerAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDealerId.Text, out int dealerId))
            {
                MessageBox.Show("Please enter a valid numeric value for Dealer ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Basic validation for phone number (e.g., ensure it's numeric if desired)
            // if (!long.TryParse(txtContactNumber.Text, out long phoneNumber)) // if you want to enforce numeric phone
            // {
            //     MessageBox.Show("Please enter a valid numeric value for Contact Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return;
            // }

            DealerModel dealer = new DealerModel
            {
                DealerId = dealerId, // Assign parsed DealerId
                DealerName = txtDealerName.Text,
                DealerAddress = txtDealerAddress.Text,
                DealerPhone = txtPhone.Text // Assuming string
            };

            try
            {
                if (!isEditMode) // Add mode
                {
                    dealerRepository.Add(dealer);
                    MessageBox.Show("Dealer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Edit mode
                {
                    dealerRepository.Update(dealer);
                    MessageBox.Show("Dealer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadDealerList(); // Refresh the grid
                ClearInputFields();
                DisableInputFields();
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                isEditMode = false;
            }
            catch (SqlException ex)
            {
                // Handle specific SQL errors, e.g., duplicate primary key for DealerId
                if (ex.Number == 2627 || ex.Number == 2601) // Primary key violation
                {
                    MessageBox.Show("A dealer with this ID already exists. Please use a unique Dealer ID.", "Duplicate ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database error saving dealer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving dealer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadDealerList(); // Reload to ensure correct selection if any
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                LoadDealerList(); // If search box is empty, show all
            }
            else
            {
                LoadDealerList(searchValue);
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
