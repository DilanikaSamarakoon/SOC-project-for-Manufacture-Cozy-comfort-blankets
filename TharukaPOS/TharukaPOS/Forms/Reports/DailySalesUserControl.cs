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
using TharukaPOS.Forms._Repositories; // Make sure this is present for ReportRepository

namespace TharukaPOS.Forms.Reports
{
    public partial class DailySalesUserControl : UserControl
    {
        private readonly ReportRepository _reportRepository;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _currentRow = 0;

        // Ensure these are declared in your Designer.cs or manually here if not using designer
        // (If you dragged them from the toolbox, these will be in DailySalesUserControl.Designer.cs)
        //private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        // Assuming btnPrint is also in the designer

        public DailySalesUserControl(string connectionString)
        {
            InitializeComponent();
            _reportRepository = new ReportRepository(connectionString);

            // Hook up event handlers as needed
            //btnGenerateReport.Click += btnGenerateReport_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            // If printDocument1 is not created by designer, create it here:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Set initial date values (e.g., today's date)
            dtpReportDate.Value = DateTime.Today;

            // Ensure the DataGridView is initially empty or populated
            dgvReport.DataSource = null;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dtpReportDate.Value.Date;
                DataTable dailySales = _reportRepository.GetDailySalesSummary(selectedDate);
                dgvReport.DataSource = dailySales;

                ConfigureDailySalesDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Daily Sales report: " + ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in DailySalesUserControl: {ex.ToString()}");
            }
        }

        private void ConfigureDailySalesDataGridView()
        {
            dgvReport.AutoGenerateColumns = true; // Start with auto-generate

            // Then customize as needed based on GetDailySalesSummary output (SaleDate, NumberOfTransactions, TotalSales)
            if (dgvReport.Columns.Contains("SaleDate"))
            {
                dgvReport.Columns["SaleDate"].HeaderText = "Date";
                dgvReport.Columns["SaleDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
                dgvReport.Columns["SaleDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvReport.Columns.Contains("NumberOfTransactions"))
            {
                dgvReport.Columns["NumberOfTransactions"].HeaderText = "Transactions";
                dgvReport.Columns["NumberOfTransactions"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvReport.Columns.Contains("TotalSales"))
            {
                dgvReport.Columns["TotalSales"].HeaderText = "Total Sales";
                dgvReport.Columns["TotalSales"].DefaultCellStyle.Format = "N2"; // Currency format, 2 decimal places
                dgvReport.Columns["TotalSales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReport.Columns["TotalSales"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            // Add more specific column configurations if your query returns more columns.
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
            saveFileDialog.FileName = $"DailySalesSummary_{dtpReportDate.Value.ToString("yyyyMMdd")}.csv";

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
                                sw.Write(dgvReport.Columns[i].HeaderText + (i == dgvReport.Columns.Count - 1 ? "" : ","));
                            }
                        }
                        sw.WriteLine();

                        // Write data rows
                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the empty new row at the bottom

                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                if (dgvReport.Columns[i].Visible) // Only export visible columns
                                {
                                    string value = row.Cells[i].Value?.ToString();
                                    if (value != null)
                                    {
                                        value = $"\"{value.Replace("\"", "\"\"")}\""; // CSV escaping
                                    }
                                    else
                                    {
                                        value = "";
                                    }
                                    sw.Write(value + (i == dgvReport.Columns.Count - 1 ? "" : ","));
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

            float bodyFontHeight = bodyFont.GetHeight(graphics);
            float headerFontHeight = headerFont.GetHeight(graphics);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int currentY = startY;

            // --- Report Title ---
            string reportTitle = $"Daily Sales Summary for {dtpReportDate.Value.ToShortDateString()}"; // Dynamic title
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 20; // Space after title

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvReport.Columns)
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                for (int i = 0; i < Math.Min(dgvReport.Rows.Count, 50); i++) // Check up to 50 rows
                {
                    if (dgvReport.Rows[i].Cells[dgvCol.Index].Value != null)
                    {
                        string cellValue = dgvReport.Rows[i].Cells[dgvCol.Index].Value.ToString();
                        if (dgvCol.DefaultCellStyle != null && !string.IsNullOrEmpty(dgvCol.DefaultCellStyle.Format))
                        {
                            // Apply formatting for measurement if applicable (e.g., "N2", "yyyy-MM-dd")
                            if (dgvCol.ValueType == typeof(decimal) && decimal.TryParse(cellValue, out decimal numericValue))
                            {
                                cellValue = numericValue.ToString(dgvCol.DefaultCellStyle.Format);
                            }
                            else if (dgvCol.ValueType == typeof(DateTime) && DateTime.TryParse(cellValue, out DateTime dateValue))
                            {
                                cellValue = dateValue.ToString(dgvCol.DefaultCellStyle.Format);
                            }
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
            while (_currentRow < dgvReport.Rows.Count)
            {
                DataGridViewRow row = dgvReport.Rows[_currentRow];
                if (row.IsNewRow)
                {
                    _currentRow++;
                    continue;
                }

                columnX = startX; // Reset X for each row
                foreach (var colInfo in columnsToPrint)
                {
                    string cellValue = row.Cells[colInfo.Column.Index].Value?.ToString() ?? "";

                    // Apply formatting if a DefaultCellStyle.Format is set (e.g., "N2", "yyyy-MM-dd")
                    if (colInfo.Column.DefaultCellStyle != null && !string.IsNullOrEmpty(colInfo.Column.DefaultCellStyle.Format))
                    {
                        if (colInfo.Column.ValueType == typeof(decimal) && decimal.TryParse(cellValue, out decimal numericValue))
                        {
                            cellValue = numericValue.ToString(colInfo.Column.DefaultCellStyle.Format);
                        }
                        else if (colInfo.Column.ValueType == typeof(DateTime) && DateTime.TryParse(cellValue, out DateTime dateValue))
                        {
                            cellValue = dateValue.ToString(colInfo.Column.DefaultCellStyle.Format);
                        }
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