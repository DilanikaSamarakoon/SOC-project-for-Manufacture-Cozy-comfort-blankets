using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms.Reports;

namespace TharukaPOS.Forms.Reports
{
    public partial class ReportsForm : Form
    {
        private readonly string _connectionString;

        public ReportsForm(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString; // Store the connection string

            this.Text = "THARUKAPOS - Business Reports"; // Set form title
            this.WindowState = FormWindowState.Maximized; // Optional: Start maximized for better report viewing

            // Optional: Load a default report when the form opens
            // For example, load the Daily Sales report by default
            // If you uncomment this, make sure DailySalesUserControl is created first.
            // LoadReportUserControl(new DailySalesUserControl(_connectionString));
        }

        /// <summary>
        /// Loads a specified UserControl into the pnlReportContent panel, replacing any existing content.
        /// </summary>
        /// <param name="reportUserControl">The UserControl to load.</param>
        private void LoadReportUserControl(UserControl reportUserControl)
        {
            // Clear any existing controls in the panel
            pnlReportContent.Controls.Clear();

            // Set the Dock style to Fill so the UserControl resizes with the panel
            reportUserControl.Dock = DockStyle.Fill;

            // Add the new UserControl to the panel's Controls collection
            pnlReportContent.Controls.Add(reportUserControl);
        }

        // --- Event Handlers for each of the 15 Report Buttons ---
        // You will create these click events for each button in the designer
        // by double-clicking the button or manually linking the event.

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new DailySalesUserControl(_connectionString));
        }

        private void btnProductStock_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new CurrentProductUserControl(_connectionString));
        }

        private void btnProductSales_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new ProductSalesUserControl(_connectionString));
        }

        private void btnCurrentStock_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new CurrentStockUserControl(_connectionString));
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new LowStockUserControl(_connectionString));
        }

        private void btnDealerHistory_Click(object sender, EventArgs e)
        {
            LoadReportUserControl(new DealerSalesHistoryUserControl(_connectionString));
        }
    }
}