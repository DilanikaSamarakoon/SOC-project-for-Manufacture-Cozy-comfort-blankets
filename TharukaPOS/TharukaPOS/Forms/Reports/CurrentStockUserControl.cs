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
using TharukaPOS.Forms.Models;

namespace TharukaPOS.Forms.Reports
{
    public partial class CurrentStockUserControl : UserControl
    {
        private readonly IBulkRepository _bulkProductRepository;
        private readonly string _connectionString;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _currentRow = 0;

        // Ensure these are declared in your Designer.cs, or manually here if not using designer
        // (If you dragged them from the toolbox, these will be in CurrentStockUserControl.Designer.cs)
        //private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        // Assuming btnPrint is also in the designer

        public CurrentStockUserControl(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _bulkProductRepository = new BulkRepository(connectionString);

            // Attach event handlers
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            // If printDocument1 is not created by designer, create it here:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Load initial data when the control loads
            this.Load += CurrentStockUserControl_Load;
        }

        private void CurrentStockUserControl_Load(object sender, EventArgs e)
        {
            LoadStockData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchProduct.Clear();
            LoadStockData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchProduct.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadStockData();
                return;
            }

            try
            {
                // Assuming SearchBulkProducts returns IEnumerable<BulkProductModel>
                var products = _bulkProductRepository.SearchBulkProducts(searchTerm).ToList();
                dgvStock.DataSource = products;
                ConfigureStockDataGridView();

                if (products.Count == 0)
                {
                    MessageBox.Show("No products found matching your search term.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching for products: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in CurrentStockUserControl (Search): {ex.ToString()}");
            }
        }

        private void LoadStockData()
        {
            try
            {
                // Assuming GetAllBulkProducts returns IEnumerable<BulkProductModel>
                var products = _bulkProductRepository.GetAllBulkProducts().ToList();
                dgvStock.DataSource = products;
                ConfigureStockDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading current stock data: " + ex.Message, "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in CurrentStockUserControl (Load): {ex.ToString()}");
            }
        }

        private void ConfigureStockDataGridView()
        {
            dgvStock.AutoGenerateColumns = false;
            dgvStock.Columns.Clear();

            dgvStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BulkProductId",
                HeaderText = "Product ID",
                DataPropertyName = "BulkProductId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BulkProductName",
                HeaderText = "Product Name",
                DataPropertyName = "BulkProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SupplierId",
                HeaderText = "Supplier ID",
                DataPropertyName = "SupplierId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BulkQuantity",
                HeaderText = "Quantity",
                DataPropertyName = "BulkQuantity",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Price",
                HeaderText = "Unit Price",
                DataPropertyName = "Price",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2 for 2 decimal places
            });

            dgvStock.Refresh();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvStock.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FileName = "BulkProductStock.csv"; // Changed filename for clarity

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        // Write header row
                        for (int i = 0; i < dgvStock.Columns.Count; i++)
                        {
                            if (dgvStock.Columns[i].Visible) // Only export visible columns
                            {
                                sw.Write(dgvStock.Columns[i].HeaderText + (i == dgvStock.Columns.Count - 1 ? "" : ","));
                            }
                        }
                        sw.WriteLine();

                        // Write data rows
                        foreach (DataGridViewRow row in dgvStock.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the empty new row at the bottom

                            for (int i = 0; i < dgvStock.Columns.Count; i++)
                            {
                                if (dgvStock.Columns[i].Visible) // Only export visible columns
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
                                    sw.Write(value + (i == dgvStock.Columns.Count - 1 ? "" : ","));
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
            if (dgvStock.Rows.Count == 0)
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
            string reportTitle = "Current Bulk Product Stock Report"; // Changed title
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 20; // Space after title

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvStock.Columns) // Changed dgvReport to dgvStock
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                // Calculate minimum width needed based on header text
                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                // Check a few rows to estimate content width (avoiding iterating all for performance)
                for (int i = 0; i < Math.Min(dgvStock.Rows.Count, 50); i++) // Changed dgvReport to dgvStock
                {
                    if (dgvStock.Rows[i].Cells[dgvCol.Index].Value != null) // Changed dgvReport to dgvStock
                    {
                        string cellValue = dgvStock.Rows[i].Cells[dgvCol.Index].Value.ToString();
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
            while (_currentRow < dgvStock.Rows.Count) // Changed dgvReport to dgvStock
            {
                DataGridViewRow row = dgvStock.Rows[_currentRow]; // Changed dgvReport to dgvStock
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