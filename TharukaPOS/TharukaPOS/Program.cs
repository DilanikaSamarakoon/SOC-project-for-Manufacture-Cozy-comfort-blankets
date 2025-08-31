using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms.Presenters;
using TharukaPOS.Forms.Views;
using TharukaPOS.Forms._Repositories;
using System.Configuration;
using TharukaPOS.Forms.Common;

namespace TharukaPOS
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string connectionString = ConfigurationManager.ConnectionStrings["THARUKAPOS"].ConnectionString;

            // Immediate validation of the connection string
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Error: Connection string 'THARUKAPOS' is not found or is empty in App.config. Please check your configuration file.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit application if critical configuration is missing
            }

            // 1. Show Splashscreen (modally)
            using (Splashscreen splashscreen = new Splashscreen(connectionString))
            {
                splashscreen.ShowDialog(); // Program pauses until Splashscreen closes
            }

            // Loop to manage Login/Logout/Dashboard flow
            bool showLoginForm = true; // Start by showing login form
            while (showLoginForm)
            {
                // 2. Show LoginForm (modally)
                using (LoginForm loginForm = new LoginForm(connectionString))
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        // Login successful, proceed to Dashboard
                        showLoginForm = false; // Break out of this login loop for now

                        // 3. Show DashboardForm (modally)
                        using (DashboardForm dashboardForm = new DashboardForm(connectionString))
                        {
                            DialogResult dashboardResult = dashboardForm.ShowDialog(); // Program pauses until DashboardForm closes

                            if (dashboardResult == DialogResult.Cancel) // DashboardForm signals logout
                            {
                                // User logged out from Dashboard, so we need to show LoginForm again
                                showLoginForm = true; // Set flag to true to re-enter the login loop
                                CurrentUser.Logout(); // Clear the current user session
                            }
                            else if (dashboardResult == DialogResult.Abort) // DashboardForm signals full exit (e.g., 'X' button)
                            {
                                // User explicitly wants to exit the application
                                Application.Exit();
                                return; // Terminate Main method
                            }
                            // If DashboardForm closes without setting DialogResult.Cancel or Abort,
                            // the application will effectively exit as 'showLoginForm' remains false.
                        }
                    }
                    else // LoginForm.ShowDialog() was not DialogResult.OK (e.g., user closed LoginForm with 'X')
                    {
                        // Login failed or was cancelled, exit the application
                        Application.Exit();
                        return; // Terminate Main method
                    }
                }
            }
            // This part of Program.cs will only be reached if the application is intended to exit
            Application.Exit();
        }
    }
}

//data source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30
