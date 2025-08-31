using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms._Repositories; // For UserRepository
using TharukaPOS.Forms.Models;     // For UserModel
using TharukaPOS.Forms.Common;
using System.Diagnostics; // For CurrentUser

namespace TharukaPOS.Forms
{
    public partial class UserManagementForm : Form
    {
        private UserRepository _userRepository;
        private string _connectionString;

        // Constructor for the form, now requires connectionString
        public UserManagementForm(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _userRepository = new UserRepository(_connectionString);

            // --- Event Handlers ---
            this.Load += UserManagementForm_Load;
            //btnAddUser.Click += btnAddUser_Click;
            //btnEditUser.Click += btnEditUser_Click;
            //btnDeleteUser.Click += btnDeleteUser_Click;
            LoadUsers();

            // --- DataGridView settings ---
            dgvUsers.AutoGenerateColumns = true; // We'll manually define columns above in designer
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Selects entire row
            dgvUsers.ReadOnly = false; // Prevents direct editing in the grid
            dgvUsers.DataBindingComplete += dgvUsers_DataBindingComplete;

            // Columns are now defined in the designer, but you can define them here if you prefer
            // Ensure DataPropertyName matches UserModel properties: UserId, Username, RoleName, RoleId

        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers(); // Load users when the form first opens
        }

        // Method to refresh the user list in the DataGridView
        private void LoadUsers()
        {
            try
            {
                List<UserModel> users = _userRepository.GetAllUsers();
                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            HideSensitiveColumns();
        }

        private void HideSensitiveColumns()
        {
            // Try to hide columns by their DataPropertyName (which matches UserModel property names)
            // Use a try-catch or null check for robustness, as columns might not always exist.

            // Password: If you have a 'Password' column from a property you manually added, hide it.
            // (Unlikely if using UserModel with hashed passwords)
            if (dgvUsers.Columns.Contains("Password"))
            {
                dgvUsers.Columns["Password"].Visible = false;
            }

            // PasswordHash: This will come from your UserModel.
            if (dgvUsers.Columns.Contains("PasswordHash"))
            {
                dgvUsers.Columns["PasswordHash"].Visible = false;
            }
            // Salt: This will come from your UserModel.
            if (dgvUsers.Columns.Contains("Salt"))
            {
                dgvUsers.Columns["Salt"].Visible = false;
            }
            // You might also want to hide RoleId if RoleName is sufficient for display
            // if (dgvUsers.Columns.Contains("RoleId"))
            // {
            //     dgvUsers.Columns["RoleId"].Visible = false;
            // }
        }


        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var userDetailForm = new UserDetailForm(_connectionString))
            {
                if (userDetailForm.ShowDialog() == DialogResult.OK)
                {
                    // REMOVE THIS VALIDATION BLOCK:
                    /*
                    if (string.IsNullOrWhiteSpace(userDetailForm.Password))
                    {
                        MessageBox.Show("Password cannot be empty. Please enter a password for the new user.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    */
                    // End of block to remove

                    try
                    {
                        // Pass the new password to AddUser.
                        // It will be null if the validation above for NewPassword set it to null for edit scenario.
                        // But for add scenario, it must contain the entered password due to validation inside UserDetailForm.
                        _userRepository.AddUser(userDetailForm.EditedUser, userDetailForm.NewPassword);
                        MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Event handler for "Edit User" button click
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get the UserId from the selected row
            // Ensure "UserId" is the correct DataPropertyName in your DGV
            int selectedUserId = (int)dgvUsers.SelectedRows[0].Cells["UserId"].Value;

            try
            {
                UserModel userToEdit = _userRepository.GetUserById(selectedUserId);
                if (userToEdit == null)
                {
                    MessageBox.Show("Selected user not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var userDetailForm = new UserDetailForm(userToEdit, _connectionString))
                {
                    if (userDetailForm.ShowDialog() == DialogResult.OK)
                    {
                        // Retrieve the updated user model and new password from the form
                        UserModel updatedUser = userDetailForm.EditedUser;
                        string newPassword = userDetailForm.NewPassword;
                        bool isPasswordChanged = userDetailForm.IsPasswordChanged;

                        if (isPasswordChanged)
                        {
                            _userRepository.UpdateUser(updatedUser, newPassword);
                        }
                        else
                        {
                            _userRepository.UpdateUser(updatedUser, null); // Pass null if password not changed
                        }

                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for "Delete User" button click
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get user ID, username, and role name from the selected row
            int selectedUserId = (int)dgvUsers.SelectedRows[0].Cells["UserId"].Value; // <--- CHANGE "UserIdColumn" to "UserId"
            string selectedUsername = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString(); // <--- CHANGE "UsernameColumn" to "Username"
            string selectedRoleName = dgvUsers.SelectedRows[0].Cells["RoleName"].Value.ToString(); // <--- Assuming you also get RoleName


            // --- IMPORTANT SECURITY CHECKS ---
            // 1. Prevent deleting the currently logged-in user's own account
            if (CurrentUser.IsLoggedIn && selectedUsername.Equals(CurrentUser.Username, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("You cannot delete your own account.", "Deletion Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Prevent deleting the last Administrator account
            if (selectedRoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                int adminCount = _userRepository.GetAdminCount(); // This method checks DB for admin roles
                if (adminCount <= 1) // If this is the only admin, prevent deletion
                {
                    MessageBox.Show("Cannot delete the last Administrator account. Please ensure there is at least one active administrator.", "Deletion Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // --- END SECURITY CHECKS ---


            // Confirmation dialog before deletion
            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete user '{selectedUsername}'?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _userRepository.DeleteUser(selectedUserId); // Call repository to delete
                    MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers(); // Refresh the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine($"CellClick: Row {e.RowIndex}, Col {e.ColumnIndex}");
            if (dgvUsers.SelectedRows.Count > 0)
            {
                Debug.WriteLine($"  Selected Row (CellClick): {dgvUsers.SelectedRows[0].Index}");
            }
            else
            {
                Debug.WriteLine("  No row selected after CellClick.");
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("SelectionChanged event fired.");
            if (dgvUsers.SelectedRows.Count > 0)
            {
                Debug.WriteLine($"  Selected Row (SelectionChanged): {dgvUsers.SelectedRows[0].Index}");
            }
            else if (dgvUsers.CurrentCell != null)
            {
                Debug.WriteLine($"  Current Cell (SelectionChanged): Row {dgvUsers.CurrentCell.RowIndex}, Col {dgvUsers.CurrentCell.ColumnIndex}");
            }
            else
            {
                Debug.WriteLine("  No row selected or current cell after SelectionChanged.");
            }
        }

        
        
    }
}
