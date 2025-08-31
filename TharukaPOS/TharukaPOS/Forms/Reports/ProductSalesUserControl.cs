using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing; // IMPORTANT: Add this namespace
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // For CSV export
using TharukaPOS.Forms._Repositories; // Your repository namespace
using TharukaPOS.Forms.Models; // Your models namespace (for CategoryModel if you have one)

namespace TharukaPOS.Forms.Reports
{
    public partial class ProductSalesUserControl : UserControl
    {
        private readonly ReportRepository _reportRepository;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _currentRow = 0;

        // Ensure these are declared in your Designer.cs, or manually here if not using designer
        // (If you dragged them from the toolbox, these will be in ProductSalesUserControl.Designer.cs)
        //private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        // Assuming btnPrint is also in the designer

        public ProductSalesUserControl(string connectionString)
        {
            InitializeComponent();
            _reportRepository = new ReportRepository(connectionString);

            // Set default date range
            dtpStartDate.Value = DateTime.Today.AddMonths(-1); // Last month
            dtpEndDate.Value = DateTime.Today;

            //btnGenerateReport.Click += btnGenerateReport_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            // If printDocument1 is not created by designer, create it here:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Generate report on load (optional, you can remove this if you prefer manual generation)
            btnGenerateReport_Click(this, EventArgs.Empty);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                string productName = txtProductName.Text.Trim();

                DataTable reportData = _reportRepository.GetProductSales(startDate, endDate, productName);
                dgvReport.DataSource = reportData;
                ConfigureProductSalesDataGridView();

                if (reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No product sales found for the selected criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Product Sales report: " + ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in ProductSalesUserControl: {ex.ToString()}");
            }
        }

        private void ConfigureProductSalesDataGridView()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            // These column names must match what your ReportRepository.GetProductSales SQL returns.
            // Based on the new GetProductSales SQL:
            // ProductName, TotalQuantitySold, TotalRevenue, TotalCostOfGoodsSold, TotalProfit

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductName", HeaderText = "Product Name", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TotalQuantitySold", HeaderText = "Qty Sold", DataPropertyName = "TotalQuantitySold", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TotalRevenue", HeaderText = "Total Revenue", DataPropertyName = "TotalRevenue", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } }); // Changed to N2
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TotalCostOfGoodsSold", HeaderText = "Total Cost", DataPropertyName = "TotalCostOfGoodsSold", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } }); // Changed to N2
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TotalProfit", HeaderText = "Total Profit", DataPropertyName = "TotalProfit", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } }); // Changed to N2

            dgvReport.Refresh();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FileName = "ProductSales.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Write header row
                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                        {
                            if (dgvReport.Columns[i].Visible) // Only export visible columns
                            {
                                sw.Write(EscapeCsvField(dgvReport.Columns[i].HeaderText));
                                if (i < dgvReport.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                        }
                        sw.WriteLine();

                        // Write data rows
                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the new row template if it exists

                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                if (dgvReport.Columns[i].Visible) // Only export visible columns
                                {
                                    object cellValue = row.Cells[i].Value;
                                    sw.Write(EscapeCsvField((cellValue == null || cellValue == DBNull.Value) ? "" : cellValue.ToString()));
                                    if (i < dgvReport.Columns.Count - 1)
                                    {
                                        sw.Write(",");
                                    }
                                }
                            }
                            sw.WriteLine();
                        }
                    }
                    MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper method to properly format fields for CSV (handle commas and quotes within data)
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return "";
            }
            // If the field contains a comma, double-quote, or newline, enclose it in double quotes
            if (field.Contains(",") || field.Contains("\"") || field.Contains(Environment.NewLine))
            {
                // Escape existing double quotes by doubling them
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }

        // ADDED: Print Button Click Event
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // If printPreviewDialog1 is not created by designer, create it here:
            // this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        // ADDED: PrintDocument BeginPrint Event
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentRow = 0; // Reset row counter for each new print job
        }

        // ADDED: PrintDocument PrintPage Event (This is where the drawing happens)
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font bodyFont = new Font("Arial", 10);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font subTitleFont = new Font("Arial", 12, FontStyle.Regular);

            float bodyFontHeight = bodyFont.GetHeight(graphics);
            float headerFontHeight = headerFont.GetHeight(graphics);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int currentY = startY;

            // --- Report Title ---
            string reportTitle = "Product Sales Report";
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 5; // Space after title

            // --- Subtitle (Date Range and Product Filter) ---
            string dateRange = $"Date Range: {dtpStartDate.Value.ToShortDateString()} - {dtpEndDate.Value.ToShortDateString()}";
            string productFilter = string.IsNullOrEmpty(txtProductName.Text.Trim()) ? "" : $"Product: {txtProductName.Text.Trim()}";

            string subTitle = dateRange;
            if (!string.IsNullOrEmpty(productFilter))
            {
                subTitle += $" | {productFilter}";
            }

            SizeF subTitleSize = graphics.MeasureString(subTitle, subTitleFont);
            graphics.DrawString(subTitle, subTitleFont, Brushes.Black, startX, currentY);
            currentY += (int)subTitleSize.Height + 20; // Space after subtitle

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvReport.Columns)
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                // Check a few rows to estimate content width (avoiding iterating all for performance)
                for (int i = 0; i < Math.Min(dgvReport.Rows.Count, 50); i++)
                {
                    if (dgvReport.Rows[i].Cells[dgvCol.Index].Value != null)
                    {
                        string cellValue = dgvReport.Rows[i].Cells[dgvCol.Index].Value.ToString();
                        if (dgvCol.DefaultCellStyle != null && !string.IsNullOrEmpty(dgvCol.DefaultCellStyle.Format))
                        {
                            // Apply formatting for measurement if applicable (e.g., "N2")
                            if ((dgvCol.ValueType == typeof(decimal) || dgvCol.ValueType == typeof(double) || dgvCol.ValueType == typeof(int)) && decimal.TryParse(cellValue, out decimal numericValue))
                            {
                                cellValue = numericValue.ToString(dgvCol.DefaultCellStyle.Format);
                            }
                            // Add other type formats if needed, e.g., DateTime
                        }
                        requiredWidth = Math.Max(requiredWidth, graphics.MeasureString(cellValue, bodyFont).Width);
                    }
                }
                requiredWidth += 10; // Padding

                columnsToPrint.Add(new ColumnPrintInfo { Column = dgvCol, PrintWidth = (int)requiredWidth });
                totalCalculatedWidth += requiredWidth;
            }

            // Adjust widths if total width exceeds printable area, or allocate remaining space for 'Fill' column
            float printableWidth = e.MarginBounds.Width;
            float scaleFactor = 1.0f;
            int fillColumnIndex = columnsToPrint.FindIndex(c => c.Column.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill);

            if (totalCalculatedWidth > printableWidth)
            {
                scaleFactor = printableWidth / totalCalculatedWidth;
                foreach (var colInfo in columnsToPrint)
                {
                    colInfo.PrintWidth = (int)(colInfo.PrintWidth * scaleFactor);
                }
            }
            else if (fillColumnIndex != -1)
            {
                float remainingSpace = printableWidth - totalCalculatedWidth;
                columnsToPrint[fillColumnIndex].PrintWidth += (int)remainingSpace;
            }

            // --- Print Column Headers ---
            int columnX = startX;
            foreach (var colInfo in columnsToPrint)
            {
                graphics.DrawString(colInfo.Column.HeaderText, headerFont, Brushes.Black, columnX, currentY);
                columnX += colInfo.PrintWidth;
            }
            currentY += (int)headerFontHeight + 10; // Space after headers

            // --- Print Data Rows ---
            // If the dgvReport.DataSource is a DataTable, you might need to iterate through DataRows
            // or ensure DataGridView.Rows access works correctly for all data types.
            // For a DataTable directly bound, row.Cells[col.Index].Value will work.

            while (_currentRow < dgvReport.Rows.Count)
            {
                DataGridViewRow row = dgvReport.Rows[_currentRow];
                if (row.IsNewRow) // Skip the "new row" placeholder if AutoGenerateColumns is true or if it's there
                {
                    _currentRow++;
                    continue;
                }

                columnX = startX; // Reset X for each row
                foreach (var colInfo in columnsToPrint)
                {
                    // Ensure you are accessing the correct cell value based on the column's DataPropertyName or index
                    string cellValue = row.Cells[colInfo.Column.Index].Value?.ToString() ?? "";

                    // Apply formatting if a DefaultCellStyle.Format is set (e.g., "N2")
                    if (colInfo.Column.DefaultCellStyle != null && !string.IsNullOrEmpty(colInfo.Column.DefaultCellStyle.Format))
                    {
                        if ((colInfo.Column.ValueType == typeof(decimal) || colInfo.Column.ValueType == typeof(double) || colInfo.Column.ValueType == typeof(int)) && decimal.TryParse(cellValue, out decimal numericValue))
                        {
                            cellValue = numericValue.ToString(colInfo.Column.DefaultCellStyle.Format);
                        }
                        // Add other type formats if needed, e.g., DateTime
                    }

                    graphics.DrawString(cellValue, bodyFont, Brushes.Black, columnX, currentY);
                    columnX += colInfo.PrintWidth;
                }
                currentY += (int)bodyFontHeight + 2; // Move to next row, adding small line spacing
                _currentRow++;

                // Check if there's enough space for the next row on the current page
                if (currentY + bodyFontHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // More pages are needed
                    return; // Stop drawing on current page
                }
            }
            e.HasMorePages = false; // No more pages
        }

        // Helper class to store column information for printing
        private class ColumnPrintInfo
        {
            public DataGridViewColumn Column { get; set; }
            public int PrintWidth { get; set; }
        }
    }
}