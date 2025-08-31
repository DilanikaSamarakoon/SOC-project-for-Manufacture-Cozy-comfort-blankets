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
using TharukaPOS.Forms._Repositories; // For DealerRepository, SaleRepository
using TharukaPOS.Forms.Models; // For DealerModel, DealerSalesHistoryReportModel

namespace TharukaPOS.Forms.Reports
{
    public partial class DealerSalesHistoryUserControl : UserControl
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly SaleRepository _saleRepository;
        private readonly string _connectionString;

        // Variables for printing (to keep track of rows for multi-page printing)
        private int _currentRow = 0;

        // Ensure these are declared in your Designer.cs, or manually here if not using designer
        // (If you dragged them from the toolbox, these will be in DealerSalesHistoryUserControl.Designer.cs)
        //private System.Drawing.Printing.PrintDocument printDocument1;
        //private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        // Assuming btnPrint is also in the designer

        public DealerSalesHistoryUserControl(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _dealerRepository = new DealerRepository(connectionString);
            _saleRepository = new SaleRepository(connectionString);

            // Attach event handlers
            // Make sure these are hooked up to the designer buttons or explicitly here
            //btnGenerateReport.Click += btnGenerateReport_Click;
            //btnExportExcel.Click += btnExportExcel_Click;
            //btnPrint.Click += btnPrint_Click; // ADDED: Hook up print button click

            // Configure PrintDocument event handlers
            // If printDocument1 is not created by designer, create it here:
            // this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDocument1.BeginPrint += printDocument1_BeginPrint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            // Load initial data (dealers for dropdown, and an initial report)
            this.Load += DealerSalesHistoryUserControl_Load;
        }

        private void DealerSalesHistoryUserControl_Load(object sender, EventArgs e)
        {
            LoadDealersDropdown();
            // Set default date range (e.g., last 30 days)
            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Value = DateTime.Today.AddMonths(-1);
            btnGenerateReport_Click(sender, e); // Generate initial report
        }

        private void LoadDealersDropdown()
        {
            try
            {
                var dealers = _dealerRepository.GetAll().ToList();
                // Add a "All Dealers" option
                dealers.Insert(0, new DealerModel { DealerId = 0, DealerName = "All Dealers" }); // Use 0 as a special ID

                cmbDealer.DataSource = dealers;
                cmbDealer.DisplayMember = "DealerName";
                cmbDealer.ValueMember = "DealerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dealers for dropdown: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in LoadDealersDropdown: {ex.ToString()}");
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            int? selectedDealerId = null;
            if (cmbDealer.SelectedValue != null && Convert.ToInt32(cmbDealer.SelectedValue) != 0)
            {
                selectedDealerId = Convert.ToInt32(cmbDealer.SelectedValue);
            }

            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start Date cannot be after End Date.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var salesHistory = _saleRepository.GetDealerSalesHistory(selectedDealerId, startDate, endDate).ToList();
                dgvSalesHistory.DataSource = salesHistory;
                ConfigureSalesHistoryDataGridView();

                if (salesHistory.Count == 0)
                {
                    MessageBox.Show("No sales found for the selected criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating sales history report: " + ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error in btnGenerateReport_Click: {ex.ToString()}");
            }
        }

        private void ConfigureSalesHistoryDataGridView()
        {
            dgvSalesHistory.AutoGenerateColumns = false;
            dgvSalesHistory.Columns.Clear();

            // Define columns for DealerSalesHistoryReportModel
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SaleId",
                HeaderText = "Sale ID",
                DataPropertyName = "SaleId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SaleDate",
                HeaderText = "Date",
                DataPropertyName = "SaleDate",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm" } // Or just "yyyy-MM-dd"
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DealerName",
                HeaderText = "Dealer Name",
                DataPropertyName = "DealerName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ProductName",
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Fills remaining space
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "QuantitySold",
                HeaderText = "Qty",
                DataPropertyName = "QuantitySold",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "UnitPrice",
                HeaderText = "Unit Price",
                DataPropertyName = "UnitPrice",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LineTotal",
                HeaderText = "Line Total",
                DataPropertyName = "LineTotal",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SaleTotalAmount",
                HeaderText = "Sale Total",
                DataPropertyName = "SaleTotalAmount",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });
            dgvSalesHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SaleDiscount",
                HeaderText = "Discount",
                DataPropertyName = "SaleDiscount",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } // Changed to N2
            });

            dgvSalesHistory.Refresh();
        }

        // --- EXPORT TO CSV FUNCTIONALITY (reused from previous examples) ---
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvSalesHistory.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV (Comma Separated Values) (*.csv)|*.csv",
                FileName = "DealerSalesHistoryReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        // Write Headers
                        for (int i = 0; i < dgvSalesHistory.Columns.Count; i++)
                        {
                            if (dgvSalesHistory.Columns[i].Visible) // Only export visible columns
                            {
                                sw.Write(EscapeCsvField(dgvSalesHistory.Columns[i].HeaderText));
                                if (i < dgvSalesHistory.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                        }
                        sw.WriteLine();

                        // Write Rows
                        foreach (DataGridViewRow row in dgvSalesHistory.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the new row template if it exists

                            for (int i = 0; i < dgvSalesHistory.Columns.Count; i++)
                            {
                                if (dgvSalesHistory.Columns[i].Visible) // Only export visible columns
                                {
                                    object cellValue = row.Cells[i].Value;
                                    sw.Write(EscapeCsvField((cellValue == null || cellValue == DBNull.Value) ? "" : cellValue.ToString()));
                                    if (i < dgvSalesHistory.Columns.Count - 1)
                                    {
                                        sw.Write(",");
                                    }
                                }
                            }
                            sw.WriteLine();
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

        // Helper method for CSV escaping (reuse this if you have it elsewhere)
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return "";
            }
            if (field.Contains(",") || field.Contains("\"") || field.Contains(Environment.NewLine))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }

        // ADDED: Print Button Click Event
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvSalesHistory.Rows.Count == 0)
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
            Font bodyFont = new Font("Arial", 8); // Slightly smaller font for more columns
            Font headerFont = new Font("Arial", 9, FontStyle.Bold); // Slightly smaller font
            Font titleFont = new Font("Arial", 16, FontStyle.Bold); // Slightly smaller title

            float bodyFontHeight = bodyFont.GetHeight(graphics);
            float headerFontHeight = headerFont.GetHeight(graphics);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int currentY = startY;

            // --- Report Title ---
            string dealerName = (cmbDealer.SelectedItem as DealerModel)?.DealerName ?? "All Dealers";
            string reportTitle = $"Dealer Sales History for {dealerName} ({dtpStartDate.Value.ToShortDateString()} to {dtpEndDate.Value.ToShortDateString()})";
            SizeF titleSize = graphics.MeasureString(reportTitle, titleFont);
            graphics.DrawString(reportTitle, titleFont, Brushes.Black, startX, currentY);
            currentY += (int)titleSize.Height + 20; // Space after title

            // --- Calculate Column Widths for Printing ---
            List<ColumnPrintInfo> columnsToPrint = new List<ColumnPrintInfo>();
            float totalCalculatedWidth = 0;

            foreach (DataGridViewColumn dgvCol in dgvSalesHistory.Columns) // Changed to dgvSalesHistory
            {
                if (!dgvCol.Visible) continue; // Skip hidden columns

                float requiredWidth = graphics.MeasureString(dgvCol.HeaderText, headerFont).Width;

                // Check a few rows to estimate content width (avoiding iterating all for performance)
                for (int i = 0; i < Math.Min(dgvSalesHistory.Rows.Count, 50); i++) // Changed to dgvSalesHistory
                {
                    if (dgvSalesHistory.Rows[i].Cells[dgvCol.Index].Value != null)
                    {
                        string cellValue = dgvSalesHistory.Rows[i].Cells[dgvCol.Index].Value.ToString();
                        if (dgvCol.DefaultCellStyle != null && !string.IsNullOrEmpty(dgvCol.DefaultCellStyle.Format))
                        {
                            // Apply formatting for measurement if applicable (e.g., "N2", "yyyy-MM-dd HH:mm")
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
                requiredWidth += 8; // Slightly less padding for more columns

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
            while (_currentRow < dgvSalesHistory.Rows.Count) // Changed to dgvSalesHistory
            {
                DataGridViewRow row = dgvSalesHistory.Rows[_currentRow]; // Changed to dgvSalesHistory
                if (row.IsNewRow)
                {
                    _currentRow++;
                    continue;
                }

                columnX = startX; // Reset X for each row
                foreach (var colInfo in columnsToPrint)
                {
                    string cellValue = row.Cells[colInfo.Column.Index].Value?.ToString() ?? "";

                    // Apply formatting if a DefaultCellStyle.Format is set (e.g., "N2", "yyyy-MM-dd HH:mm")
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