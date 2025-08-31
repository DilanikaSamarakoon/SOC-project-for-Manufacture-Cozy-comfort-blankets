using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing; // Important: Add this namespace
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms._Repositories;

namespace TharukaPOS.Forms.Reports
{
    public partial class CurrentProductUserControl : UserControl
    {
        private readonly ReportRepository _reportRepository;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _rowsPerPage = 0;
        private int _currentRow = 0;

        public CurrentProductUserControl(string connectionString)
        {
            InitializeComponent();
            _reportRepository = new ReportRepository(connectionString);

            //btnGenerateReport.Click += btnGenerateReport_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Load report automatically when control opens
            btnGenerateReport_Click(this, EventArgs.Empty);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable reportData = _reportRepository.GetCurrentStockLevels();
                dgvReport.DataSource = reportData;
                ConfigureCurrentStockDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Current Stock report: " + ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in CurrentStockUserControl: {ex.ToString()}");
            }
        }

        private void ConfigureCurrentStockDataGridView()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductName", HeaderText = "Product Name", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ProductID", HeaderText = "Product ID", DataPropertyName = "ProductID", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CurrentStock", HeaderText = "Current Qty", DataPropertyName = "CurrentStock", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CostPrice", HeaderText = "Unit Cost", DataPropertyName = "CostPrice", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Format = "N" } });
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TotalStockValue", HeaderText = "Total Value", DataPropertyName = "TotalStockValue", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, DefaultCellStyle = { Format = "N" } });

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
            saveFileDialog.FileName = "CurrentStockLevels.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Write header row
                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                        {
                            sw.Write(dgvReport.Columns[i].HeaderText + (i == dgvReport.Columns.Count - 1 ? "" : ","));
                        }
                        sw.WriteLine();

                        // Write data rows
                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                string value = row.Cells[i].Value?.ToString();
                                if (value != null)
                                {
                                    value = $"\"{value.Replace("\"", "\"\"")}\"";
                                }
                                else
                                {
                                    value = "";
                                }
                                sw.Write(value + (i == dgvReport.Columns.Count - 1 ? "" : ","));
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

            // Set the document for the PrintPreviewDialog
            printPreviewDialog1.Document = printDocument1;

            // Show the print preview dialog
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                // If user clicks Print from the preview, this will execute the print
                printDocument1.Print();
            }
            // Optional: For direct printing without preview, use PrintDialog
            // PrintDialog printDialog = new PrintDialog();
            // printDialog.Document = printDocument1;
            // if (printDialog.ShowDialog() == DialogResult.OK)
            // {
            //     printDocument1.Print();
            // }
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
            Font bodyFont = new Font("Arial", 10); // Changed from 'font' to 'bodyFont' for clarity
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font titleFont = new Font("Arial", 18, FontStyle.Bold);

            float bodyFontHeight = bodyFont.GetHeight(graphics);
            float headerFontHeight = headerFont.GetHeight(graphics);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int currentY = startY;

            // --- Report Title ---
            string reportTitle = "Current Stock Levels Report";
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 20; // Space after title

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvReport.Columns)
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                // Calculate minimum width needed based on header text
                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                // Check a few rows to estimate content width (avoiding iterating all for performance)
                for (int i = 0; i < Math.Min(dgvReport.Rows.Count, 50); i++) // Check up to 50 rows
                {
                    if (dgvReport.Rows[i].Cells[dgvCol.Index].Value != null)
                    {
                        string cellValue = dgvReport.Rows[i].Cells[dgvCol.Index].Value.ToString();
                        // Apply formatting for measurement if applicable
                        if (dgvCol.DefaultCellStyle != null && !string.IsNullOrEmpty(dgvCol.DefaultCellStyle.Format))
                        {
                            if (decimal.TryParse(cellValue, out decimal numericValue))
                            {
                                cellValue = numericValue.ToString(dgvCol.DefaultCellStyle.Format);
                            }
                        }
                        requiredWidth = Math.Max(requiredWidth, graphics.MeasureString(cellValue, bodyFont).Width);
                    }
                }
                // Add some padding to the calculated width
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
                // If content is too wide, scale down proportionally
                scaleFactor = printableWidth / totalCalculatedWidth;
                foreach (var colInfo in columnsToPrint)
                {
                    colInfo.PrintWidth = (int)(colInfo.PrintWidth * scaleFactor);
                }
            }
            else if (fillColumnIndex != -1)
            {
                // If there's extra space and a fill column, give extra space to fill column
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
                if (row.IsNewRow) // Skip the new row template if it exists
                {
                    _currentRow++;
                    continue;
                }

                columnX = startX; // Reset X for each row
                foreach (var colInfo in columnsToPrint)
                {
                    string cellValue = row.Cells[colInfo.Column.Index].Value?.ToString() ?? "";

                    // Apply formatting if a DefaultCellStyle.Format is set (e.g., "N2")
                    if (colInfo.Column.DefaultCellStyle != null && !string.IsNullOrEmpty(colInfo.Column.DefaultCellStyle.Format))
                    {
                        if (decimal.TryParse(cellValue, out decimal numericValue))
                        {
                            cellValue = numericValue.ToString(colInfo.Column.DefaultCellStyle.Format);
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
