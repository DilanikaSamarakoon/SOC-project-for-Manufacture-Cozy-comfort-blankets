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
using System.IO;
using TharukaPOS.Forms._Repositories; // For ProductRepository
using TharukaPOS.Forms.Models; // For ProductModel


namespace TharukaPOS.Forms.Reports
{
    public partial class LowStockUserControl : UserControl
    {
        private readonly IProductRepository _productRepository;
        private readonly string _connectionString;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _currentRow = 0;

        // Ensure these are declared in your Designer.cs, or manually here if not using designer
        // (If you dragged them from the toolbox, these will be in LowStockUserControl.Designer.cs)
        //private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        // Assuming btnPrint is also in the designer

        public LowStockUserControl(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _productRepository = new ProductRepository(connectionString);

            // Attach event handlers
            //btnGenerateReport.Click += btnGenerateReport_Click;
            //btnExportExcel.Click += btnExportExcel_Click; // Ensure this is uncommented if you use it
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            // If printDocument1 is not created by designer, create it here:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Load initial data when the control loads
            this.Load += LowStockUserControl_Load;
        }

        private void LowStockUserControl_Load(object sender, EventArgs e)
        {
            // Load low stock data with the default threshold value
            LoadLowStockData(Convert.ToInt32(nudThreshold.Value));
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            int threshold = Convert.ToInt32(nudThreshold.Value);
            LoadLowStockData(threshold);
        }

        private void LoadLowStockData(int threshold)
        {
            try
            {
                // Call the repository to get low stock products
                var products = _productRepository.GetLowStockProducts(threshold).ToList();
                dgvLowStock.DataSource = products;
                ConfigureLowStockDataGridView(); // Apply column settings

                if (products.Count == 0)
                {
                    MessageBox.Show($"No products found with quantity <= {threshold}.", "No Low Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // FIX: Use MessageBoxIcon.Error (as per previous discussions)
                MessageBox.Show("Error loading low stock data: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in LowStockUserControl (Load): {ex.ToString()}");
            }
        }

        private void ConfigureLowStockDataGridView()
        {
            dgvLowStock.AutoGenerateColumns = false; // Set to false for manual control
            dgvLowStock.Columns.Clear();

            // Define columns manually to control their appearance and formatting
            // DataPropertyName must match the property name in ProductModel
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ProductId",
                HeaderText = "Product ID",
                DataPropertyName = "ProductId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Weight",
                HeaderText = "Weight",
                DataPropertyName = "Weight",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Quantity",
                HeaderText = "Current Qty",
                DataPropertyName = "Quantity",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BuyingPrice",
                HeaderText = "Buying Price",
                DataPropertyName = "BuyingPrice",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SellingPrice",
                HeaderText = "Selling Price",
                DataPropertyName = "SellingPrice",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });

            dgvLowStock.Refresh();
        }

        // <--- EXPORT TO EXCEL FUNCTIONALITY --->
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvLowStock.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV (Comma Separated Values) (*.csv)|*.csv", // Changed filter to .csv
                FileName = "LowStockReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv" // Changed default extension
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Use StreamWriter to write directly to the file
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        // Write Headers
                        for (int i = 0; i < dgvLowStock.Columns.Count; i++)
                        {
                            if (dgvLowStock.Columns[i].Visible) // Only export visible columns
                            {
                                sw.Write(EscapeCsvField(dgvLowStock.Columns[i].HeaderText));
                                if (i < dgvLowStock.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                        }
                        sw.WriteLine(); // New line after headers

                        // Write Rows
                        foreach (DataGridViewRow row in dgvLowStock.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the new row template if it exists

                            for (int i = 0; i < dgvLowStock.Columns.Count; i++)
                            {
                                if (dgvLowStock.Columns[i].Visible) // Only export visible columns
                                {
                                    object cellValue = row.Cells[i].Value;
                                    sw.Write(EscapeCsvField((cellValue == null || cellValue == DBNull.Value) ? "" : cellValue.ToString()));
                                    if (i < dgvLowStock.Columns.Count - 1)
                                    {
                                        sw.Write(",");
                                    }
                                }
                            }
                            sw.WriteLine(); // New line after each row
                        }
                    }

                    MessageBox.Show("Report exported successfully to:\n" + sfd.FileName, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Error exporting to CSV: {ex.ToString()}");
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
            if (dgvLowStock.Rows.Count == 0)
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
            Font subTitleFont = new Font("Arial", 12, FontStyle.Regular); // For threshold

            float bodyFontHeight = bodyFont.GetHeight(graphics);
            float headerFontHeight = headerFont.GetHeight(graphics);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int currentY = startY;

            // --- Report Title ---
            string reportTitle = "Low Stock Report";
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 5; // Space after title

            // --- Subtitle (Threshold) ---
            string subTitle = $"Threshold: {nudThreshold.Value}";
            SizeF subTitleSize = graphics.MeasureString(subTitle, subTitleFont);
            graphics.DrawString(subTitle, subTitleFont, Brushes.Black, startX, currentY);
            currentY += (int)subTitleSize.Height + 20; // Space after subtitle

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvLowStock.Columns)
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                // Check a few rows to estimate content width (avoiding iterating all for performance)
                for (int i = 0; i < Math.Min(dgvLowStock.Rows.Count, 50); i++)
                {
                    if (dgvLowStock.Rows[i].Cells[dgvCol.Index].Value != null)
                    {
                        string cellValue = dgvLowStock.Rows[i].Cells[dgvCol.Index].Value.ToString();
                        if (dgvCol.DefaultCellStyle != null && !string.IsNullOrEmpty(dgvCol.DefaultCellStyle.Format))
                        {
                            // Apply formatting for measurement if applicable (e.g., "N2")
                            if (dgvCol.ValueType == typeof(decimal) && decimal.TryParse(cellValue, out decimal numericValue))
                            {
                                cellValue = numericValue.ToString(dgvCol.DefaultCellStyle.Format);
                            }
                            // No DateTime formatting needed for this report's columns
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
            while (_currentRow < dgvLowStock.Rows.Count)
            {
                DataGridViewRow row = dgvLowStock.Rows[_currentRow];
                if (row.IsNewRow)
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
                        if (colInfo.Column.ValueType == typeof(decimal) && decimal.TryParse(cellValue, out decimal numericValue))
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