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

namespace TharukaPOS.Forms
{
    public partial class UserDetailForm : Form
    {
        // Remove 'public UserModel User { get; private set; }'
        // We will use _currentUser internally and expose it via EditedUser.
        // public string Password { get; private set; } // This is also redundant with NewPassword

        private UserRepository _userRepository;
        private readonly string connectionString; // Use 'readonly' as it's set in constructor

        // These are the public properties that UserManagementForm will read
        public UserModel EditedUser { get; private set; }
        public string NewPassword { get; private set; }
        public bool IsPasswordChanged { get; private set; }

        private UserModel _currentUser; // This holds the user data being edited/added

        // Constructor for ADDING a new user
        public UserDetailForm(string connectionString)
        {
            InitializeComponent();
            this.Text = "Add New User";
            this.connectionString = connectionString;
            _userRepository = new UserRepository(connectionString);

            _currentUser = new UserModel(); // Initialize for a new user (UserId will be 0)
            InitializeRolesComboBox();

            // For adding, password fields should be mandatory
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;
        }

        // Constructor for EDITING an existing user
        public UserDetailForm(UserModel userToEdit, string connectionString)
        {
            InitializeComponent();
            this.Text = "Edit User";
            this.connectionString = connectionString;
            _userRepository = new UserRepository(connectionString);

            _currentUser = userToEdit; // Assign the user model passed for editing
            InitializeRolesComboBox();
            LoadUserData(); // Populate controls with existing user data

            // For editing, password fields are optional (enabled but can be left blank)
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;

            // Optional: Disable username editing if you don't want it changeable for existing users
            // txtUsername.ReadOnly = true;
        }

        private void InitializeRolesComboBox()
        {
            try
            {
                // Use the _userRepository field initialized in the constructor
                Dictionary<int, string> roles = _userRepository.GetAllRoles();

                cmbRole.DataSource = null; // Clear existing items
                cmbRole.Items.Clear();

                cmbRole.DataSource = new BindingSource(roles, null);
                cmbRole.DisplayMember = "Value"; // RoleName
                cmbRole.ValueMember = "Key";     // RoleId

                if (cmbRole.Items.Count > 0)
                {
                    // If editing, try to select the current user's role
                    if (_currentUser != null && _currentUser.RoleId > 0)
                    {
                        cmbRole.SelectedValue = _currentUser.RoleId;
                    }
                    else
                    {
                        cmbRole.SelectedIndex = 0; // Otherwise, select the first role
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserData()
        {
            if (_currentUser != null)
            {
                txtUsername.Text = _currentUser.Username;
                // DO NOT pre-fill password fields for security reasons
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";

                // Select the correct role in the ComboBox
                if (cmbRole.Items.Count > 0 && _currentUser.RoleId > 0)
                {
                    cmbRole.SelectedValue = _currentUser.RoleId;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- START VALIDATION ---
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRole.SelectedValue == null)
            {
                MessageBox.Show("Please select a user role.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password validation logic
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (_currentUser.UserId == 0) // It's a new user, password is required
                {
                    MessageBox.Show("Password cannot be empty. Please enter a password for the new user.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else // Existing user, password field is empty -> no password change
                {
                    NewPassword = null;
                    IsPasswordChanged = false;
                }
            }
            else
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NewPassword = txtPassword.Text;
                IsPasswordChanged = true;
            }
            // --- END VALIDATION ---

            // Populate the EditedUser property from form controls
            // This ensures the original UserId is maintained for edits, or is 0 for new users.
            EditedUser = _currentUser; // Assign the internal _currentUser to the public property
            EditedUser.Username = txtUsername.Text.Trim();
            EditedUser.RoleId = (int)cmbRole.SelectedValue;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}