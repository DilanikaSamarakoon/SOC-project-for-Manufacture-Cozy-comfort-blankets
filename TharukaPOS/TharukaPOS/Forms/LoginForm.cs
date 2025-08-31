// TharukaPOS\Forms\LoginForm.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using TharukaPOS.Forms._Repositories;
using TharukaPOS.Forms.Common;
using TharukaPOS.Forms.Models;

namespace TharukaPOS
{
    public partial class LoginForm : Form
    {
        private readonly string connectionString;

        public LoginForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.AcceptButton = btnLogin;

            // Set the password field to hide characters by default
            txtPassword.PasswordChar = '*'; // Use '*' or any other character you prefer

            // Hook up the CheckBox event handler
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged; // ADDED
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserRepository userRepository = new UserRepository(this.connectionString);

            try
            {
                UserModel authenticatedUser = userRepository.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                if (authenticatedUser != null)
                {
                    TharukaPOS.Forms.Common.CurrentUser.SetUser(
                        authenticatedUser.Username,
                        authenticatedUser.RoleId,
                        authenticatedUser.RoleName
                    );

                    MessageBox.Show(
                        $"Welcome, {authenticatedUser.Username}!\nLogin Successful.",
                        "Login Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle when the user closes the form via the 'X' button
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void iconButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ADDED: CheckBox CheckedChanged Event Handler
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, set PasswordChar to '\0' (null character) to show text.
            // If unchecked, set it back to '*' to hide text.
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; // Show password
            }
            else
            {
                txtPassword.PasswordChar = '*'; // Hide password
            }
        }
    }
}