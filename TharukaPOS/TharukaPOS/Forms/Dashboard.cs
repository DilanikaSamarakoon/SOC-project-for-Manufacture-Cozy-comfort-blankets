using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TharukaPOS.Forms._Repositories;


namespace TharukaPOS.Forms
{
    public partial class DashBoard : Form
    {
        // Field to hold our DataAccess object
        private DashboardRepository model;
        private Button curentButton;// Change to DashboardRepository if you named your class that

        public DashBoard()
        {
            InitializeComponent(); // Initializes UI components from the designer
            // *** IMPORTANT: These lines are crucial for embedding this form into panelDesktop ***
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            this.model = new DashboardRepository(); // <--- ADD THIS LINE
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false; // Hide the apply button initially
            SetDateMenuButtonsUI(btnLast7Days);
        }


        public DashBoard(DashboardRepository dashboardRepository) : this() // Call the parameterless constructor first
        {
            this.model = dashboardRepository; // Assign the passed-in DataAccess instance to our field

            // Set default date range to 'Last 7 Days' on startup
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select(); // Optionally visually select the default button

            LoadData(); // Load data initially with the default range
        }

        private void DisableCustomDateControls()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }

        // Enables the custom date picker controls and shows the apply button
        private void EnableCustomDateControls()
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
        }

        

        // --- Main method to load and display dashboard data ---
        private void LoadData()
        {
            // Critical check: Ensure the DataAccess model is not null before using it
            if (model == null)
            {
                Console.WriteLine("Error: DataAccess model is null in LoadData().");
                MessageBox.Show("Internal error: Data access not initialized correctly. Please contact support.");
                return; // Exit to prevent NullReferenceException
            }


            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);

            if (refreshData) // Check if the data loading in the model was successful
            {
                // Update Labels with fetched data
                lblNumberOfOrders.Text = model.NumOrders.ToString("N0"); // "N0" for number with thousands separator
                lblTotalRevenue.Text = model.TotalRevenue.ToString("N"); // "C2" for currency with 2 decimal places (e.g., LKR 1,000.00)
                lblTotalProfit.Text = model.TotalProfit.ToString("N");

                lblNumCustomers.Text = model.NumCustomers.ToString("N0");
                lblNumSuppliers.Text = model.NumSuppliers.ToString("N0");
                lblNumProducts.Text = model.NumProducts.ToString("N0");

                    //Bind charts and DataGridView 
                    chartGrossRevenue.DataSource = model.GrossRevenueList;
                    chartGrossRevenue.Series[0].XValueMember = "Date";        // Property name in your dynamic object
                    chartGrossRevenue.Series[0].YValueMembers = "TotalAmount"; // Property name in your dynamic object
                    chartGrossRevenue.DataBind();

                    chartTopProducts.DataSource = model.TopProductsList;
                    chartTopProducts.Series[0].XValueMember = "Key";         // Property name in your dynamic object
                    chartTopProducts.Series[0].YValueMembers = "Value";      // Property name in your dynamic object
                    chartTopProducts.DataBind();

                dgvUnderstock.DataSource = model.UnderstockList;
                // Set DataGridView column headers (adjust column indices/names if your DataTable structure changes)
                if (dgvUnderstock.Columns.Count > 0)
                {
                    dgvUnderstock.Columns[0].HeaderText = "Item";
                    if (dgvUnderstock.Columns.Count > 1)
                    {
                        dgvUnderstock.Columns[1].HeaderText = "Units";
                    }
                }

                Console.WriteLine("Dashboard view loaded successfully :)");
            }
            else
            {
                Console.WriteLine("Dashboard view not loaded, data access failed or same query.");
            }
        }

        private void SetDateMenuButtonsUI(object button)
        {
            var btn = (Button)button;
            //hightlight button
            btn.BackColor = btnLast30Days.FlatAppearance.BorderColor;
            btn.ForeColor = Color.White;
            //unhighlight button
            if (curentButton != null && curentButton != btn)
            {
                curentButton.BackColor = this.BackColor;
                curentButton.ForeColor = Color.FromArgb(124, 141, 183);
            }
            curentButton = btn;
        }

        // --- Event Handlers for Date Range Buttons ---

        // Event handler for "Today" button
        private void btnToday_Click(object sender, EventArgs e)
        {
            DisableCustomDateControls(); // Disable custom date pickers
            dtpStartDate.Value = DateTime.Today; // Set start date to beginning of today
            dtpEndDate.Value = DateTime.Now;     // Set end date to current moment
            LoadData();                          // Refresh dashboard data
            btnToday.Select();                   // Visually select this button
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "Last 7 Days" button
        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            DisableCustomDateControls();
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            btnLast7Days.Select();
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "Last 30 Days" button
        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            DisableCustomDateControls();
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            btnLast30Days.Select();
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "This Month" button
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            DisableCustomDateControls();
            // Set start date to the first day of the current month
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            btnThisMonth.Select();
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "This Year" button
        private void btnThisYear_Click(object sender, EventArgs e)
        {
            DisableCustomDateControls();
            // Set start date to the first day of the current year
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            btnThisYear.Select();
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "Custom Range" button
        private void btnCustomRange_Click(object sender, EventArgs e)
        {
            EnableCustomDateControls(); // Enable the custom date pickers and show apply button
            btnCustomRange.Select();    // Visually select this button
            // Optionally, set a default range for the custom pickers for user convenience
            // e.g., last 30 days or current month
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Today;
            SetDateMenuButtonsUI(sender);
        }

        // Event handler for "Apply" button after custom dates are selected
        private void btnApplyCustomDate_Click(object sender, EventArgs e)
        {
            // Input Validation: Check if Start Date is not after End Date
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start Date cannot be greater than End Date.", "Date Range Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if dates are invalid
            }

           


            LoadData(); // Load data with the newly selected custom range
            // No button to select here, as it's an "apply" action for the date pickers.
        }

        private void btnToday_Click_1(object sender, EventArgs e)
        {
            DisableCustomDateControls(); // Disable custom date pickers
            dtpStartDate.Value = DateTime.Today; // Set start date to beginning of today
            dtpEndDate.Value = DateTime.Now;     // Set end date to current moment
            LoadData();                          // Refresh dashboard data
            btnToday.Select();                   // Visually select this button
            SetDateMenuButtonsUI(sender);
        }

       
    }
}