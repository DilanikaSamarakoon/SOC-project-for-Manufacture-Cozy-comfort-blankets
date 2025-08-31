using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using TharukaPOS.Forms._Repositories;
using TharukaPOS.Forms.Common;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms.Presenters;
using TharukaPOS.Forms.Reports;
using TharukaPOS.Forms.Views;

namespace TharukaPOS.Forms
{
    public partial class DashboardForm : Form
    {
        //Fields
        private readonly string _connectionString;
        private DashboardRepository _dashboardRepository;
        private int borderSize = 2;
        private Size formSize; //Keep form size when it is minimized and restored.
                               //Since the form is resized because it takes into account the size of the title bar and borders.
        private IconButton currentBtn;
        private Color activeColor = Color.FromArgb(107, 83, 255); // The highlight color for the active button
        private Color inactiveColor = Color.FromArgb(42, 45, 86);


        public DashboardForm(string connectionString)
        {
            InitializeComponent();
            this._connectionString = connectionString; // Store the passed connection string
            // Initialize DashboardRepository using the stored connection string
            _dashboardRepository = new DashboardRepository(this._connectionString);

            CollapseMenu();
            this.Padding = new Padding(borderSize);

            ActivateButton(iconButtonDashboard); // Automatically activate Dashboard on startup


            UpdateUserRoleLabel();

            ApplyUserRoleRestrictions(); // Now _connectionString and _dashboardRepository are ready
            LoadDashboardContent();
        }

        // Method to update the user role label based on CurrentUser status
        private void UpdateUserRoleLabel()
        {
            if (CurrentUser.IsLoggedIn)
            {
                if (CurrentUser.IsAdmin)
                {
                    lblUserRole.Text = "Admin";
                }
                else if (CurrentUser.IsRegularUser)
                {
                    lblUserRole.Text = "User";
                }
                else
                {
                    lblUserRole.Text = "Unknown";
                }
            }
            else
            {
                lblUserRole.Text = "Guest";
            }
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DisableButton(); // First, reset the previously active button

                // Current Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = activeColor; // Set active background color
                currentBtn.ForeColor = Color.WhiteSmoke; // Keep text color consistent or change if needed
                currentBtn.IconColor = Color.WhiteSmoke; // Keep icon color consistent or change if needed
                currentBtn.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, ((byte)(0))); // Add underline or bolder font

                // You might also want to change the icon for the active button to give it more prominence
                // Example:
                // switch (currentBtn.Tag.ToString())
                // {
                //     case "Dashboard":
                //         currentBtn.IconChar = IconChar.Desktop; // A slightly different icon
                //         break;
                //     // Add cases for other buttons if you want different active icons
                // }
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = inactiveColor; // Reset to default background color
                currentBtn.ForeColor = Color.WhiteSmoke; // Reset text color
                currentBtn.IconColor = Color.WhiteSmoke; // Reset icon color
                currentBtn.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); // Reset font style
                                                                                                                            // Reset active icon back to its original if you changed it in ActivateButton
                                                                                                                            // Example:
                                                                                                                            // switch (currentBtn.Tag.ToString())
                                                                                                                            // {
                                                                                                                            //     case "Dashboard":
                                                                                                                            //         currentBtn.IconChar = IconChar.DesktopAlt;
                                                                                                                            //         break;
                                                                                                                            // }
            }
        }

        private void ApplyUserRoleRestrictions()
        {
            if (CurrentUser.IsLoggedIn)
            {
                if (CurrentUser.IsAdmin)
                {
                    iconButtonProducts.Visible = true;
                    iconButtonProducts.Enabled = true;
                    // ... make other admin-specific buttons visible/enabled ...
                    iconButtonSuppliers.Visible = true;
                    iconButtonSuppliers.Enabled = true;
                    iconButtonDealers.Visible = true;
                    iconButtonDealers.Enabled = true;

                    iconButtonDashboard.Visible = true;
                    iconButtonDashboard.Enabled = true;
                    iconButtonSales.Visible = true;
                    iconButtonSales.Enabled = true;
                    iconButtonStock.Visible = true;
                    iconButtonStock.Enabled = true;
                    iconButtonInvoices.Visible = true;
                    iconButtonInvoices.Enabled = true;
                }
                else if (CurrentUser.IsRegularUser)
                {
                    iconButtonDashboard.Visible = true;
                    iconButtonDashboard.Enabled = true;
                    iconButtonSales.Visible = true;
                    iconButtonSales.Enabled = true;
                    iconButtonStock.Visible = true;
                    iconButtonStock.Enabled = true;
                    iconButtonInvoices.Visible = true;
                    iconButtonInvoices.Enabled = true;

                    // Admin-only functions - hide and disable
                    iconButtonProducts.Visible = false;
                    iconButtonProducts.Enabled = false;
                    iconButtonSuppliers.Visible = false;
                    iconButtonSuppliers.Enabled = false;
                    iconButtonDealers.Visible = false;
                    iconButtonDealers.Enabled = false;
                    iconButtonSettings.Visible = false;
                    iconButtonSettings.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("No user is logged in. Redirecting to login...", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Hide();
                this.Close();
            }

            panelMenu.Invalidate();
            panelMenu.Update();
            panelMenu.Refresh();
        }



        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;

            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right


            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          

                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion

            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void DashboardForm_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }


        //buttons
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) // Menu is currently expanded, collapsing it
            {
                panelMenu.Width = 140;
                // pictureBox1.Visible = false; // Remove this line
                btnMenu.Dock = DockStyle.Top;

                // Remove ALL lines related to lblUserRole here:
                // lblUserRole.AutoSize = false;
                // lblUserRole.Width = panelMenu.Width - 10;
                // lblUserRole.Location = new Point(5, lblUserRole.Location.Y);
                // lblUserRole.TextAlign = ContentAlignment.MiddleCenter;
                // if (CurrentUser.IsLoggedIn) { ... } else { ... } // Remove all this text setting

                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else // Menu is currently collapsed, expanding it
            {
                panelMenu.Width = 224;
                // pictureBox1.Visible = true; // Remove this line
                btnMenu.Dock = DockStyle.None;

                // Remove ALL lines related to lblUserRole here:
                // UpdateUserRoleLabel();
                // lblUserRole.AutoSize = true;
                // lblUserRole.TextAlign = ContentAlignment.MiddleLeft;
                // lblUserRole.Location = _lblUserRoleInitialLocation; // Remove this

                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
            // Ensure the panel redraws (keep these)
            panelMenu.Invalidate();
            panelMenu.Update();
            panelMenu.Refresh();
        }

        //Child form
        private Form activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelDesktop.Controls.Clear(); // optional: remove previous child form
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            // MVP wiring for ProductView
            string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["THARUKAPOS"].ConnectionString;

            IProductView productView = new ProductView();
            IProductRepository productRepository = new ProductRepository(sqlConnectionString);
            new ProductPresenter(productView, productRepository);

            OpenChildForm((Form)productView);
        }


        // *** ESSENTIAL: This constructor receives the DataAccess instance from Program.cs ***
        public DashboardForm(DashboardRepository dashboardRepository) 
        {
            _dashboardRepository = dashboardRepository; 

            
            LoadDashboardContent();
        }

        // Method specifically to create and load the DashBoard content form
        private void LoadDashboardContent()
        {
            // Check if _dashboardRepository has been initialized.
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Internal error: Dashboard repository not initialized correctly. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DashBoard dashboardContentForm = new DashBoard(_dashboardRepository);
            OpenChildForm(dashboardContentForm);
        }


        private void iconButtonDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            // When the Dashboard button is clicked, load the dashboard content
            LoadDashboardContent();

        }

        private void iconButtonStock_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Data access model not initialized in DashboardForm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of the BulkForm, passing the connection string
            StockForm stockForm = new StockForm(_dashboardRepository.connectionString); // Pass the connection string from DataAccess

            // Use your OpenChildForm method to display it in panelDesktop
            OpenChildForm(stockForm);
        }

        private void iconButtonDealers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Data access model not initialized in DashboardForm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of the DealerForm, passing the connection string
            DealerForm dealerForm = new DealerForm(_dashboardRepository.connectionString);

            // Use your OpenChildForm method to display it in panelDesktop
            OpenChildForm(dealerForm);
        }

        private void iconButtonSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Data access model not initialized in DashboardForm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of the SalesForm, passing the connection string
            SalesForm salesForm = new SalesForm(_dashboardRepository.connectionString);

            // Use your OpenChildForm method to display it in panelDesktop
            OpenChildForm(salesForm);
        }

        private void iconButtonSuppliers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            // Check if _dataAccess has been initialized. It should be in the DashboardForm's constructor.
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Internal error: Data access not initialized correctly. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of the SupplierForm, passing the connection string
            // The SupplierForm constructor needs the connection string to initialize its repository
            SupplierForm supplierForm = new SupplierForm(_dashboardRepository.connectionString);

            // Use your OpenChildForm method to display it in panelDesktop
            OpenChildForm(supplierForm);
        }

        private void iconButtonInvoices_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (_dashboardRepository == null)
            {
                MessageBox.Show("Internal error: Data access not initialized correctly. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create an instance of the InvoiceForm, passing the connection string
            InvoiceForm invoiceForm = new InvoiceForm(_dashboardRepository.connectionString);

            // Use your OpenChildForm method to display it in panelDesktop
            OpenChildForm(invoiceForm);
        }

        private void iconButtonLogout_Click(object sender, EventArgs e)
        {
            // Display a confirmation dialog
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?", // Message to display
                "Confirm Logout",                    // Title of the message box
                MessageBoxButtons.YesNo,             // Buttons to show (Yes, No)
                MessageBoxIcon.Question);            // Icon to display (Question mark)

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                // If user confirms logout, proceed with existing logout logic
                this.DialogResult = DialogResult.Cancel; // <--- Set DialogResult for logout
                this.Close(); // Close the DashboardForm
            }

            //this.DialogResult = DialogResult.Cancel; // <--- Set DialogResult for logout
            //this.Close(); // Close the DashboardForm
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If no specific DialogResult has been set by a button (like logout),
            // assume it's a full application exit.
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Abort; // <--- Indicate full exit
            }
        }

        private void iconButtonReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            // Make sure your DashboardForm's _connectionString is available here.
            // If you're using OpenChildForm method, ensure it can handle opening a Form.
            // If OpenChildForm takes a Form, it will be:
            ReportsForm reportsDashboard = new ReportsForm(this._connectionString);
            OpenChildForm(reportsDashboard); // This method should load the form into a panel on your Dashboard, or ShowDialog().
                                             // If you don't have OpenChildForm, you might use:
                                             // reportsDashboard.ShowDialog(); // Opens as a new modal window

        }

        private void iconButtonSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (this._connectionString == null)
            {
                MessageBox.Show("Database connection not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Pass the connection string to UserManagementForm
            UserManagementForm userManagementForm = new UserManagementForm(this._connectionString);
            OpenChildForm(userManagementForm);
        }

    }
        
}
