using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms._Repositories; // For repositories

namespace TharukaPOS.Forms
{
    public partial class InvoiceForm : Form
    {
        private ISaleRepository saleRepository;
        // private IDealerRepository dealerRepository; // If you have a separate IDealerRepository and use it here

        private int? selectedDealerId = null;
        private DealerModel selectedDealer = null; // Store the full DealerModel
        private DateTime? selectedInvoiceDate = null;

        private BindingList<DealerModel> dealerList;
        private BindingList<SaleDetailModel> salesDetailsList;

        // For Printing
        private int currentPage = 0;
        private int currentPrintRow = 0;


        // Constructor
        public InvoiceForm(string connectionString)
        {
            InitializeComponent();
            saleRepository = new SaleRepository(connectionString);
            // If you have a separate IDealerRepository and use it here:
            // dealerRepository = new DealerRepository(connectionString);

            InitializeFormState();
            HookEvents();
        }

        private void HookEvents()
        {
            // Date Controls
            btnDateOk.Click += btnDateOk_Click;
            btnDateChange.Click += btnDateChange_Click;

            // Dealer Search and Selection Controls
            btnDealerSearch.Click += btnDealerSearch_Click;
            txtDealerSearch.KeyPress += txtDealerSearch_KeyPress;
            dgvDealerList.SelectionChanged += dgvDealerList_SelectionChanged;
            btnDealerOk.Click += btnDealerOk_Click;
            btnDealerChange.Click += btnDealerChange_Click;

            // Form Actions
            btnPrint.Click += btnPrint_Click;
            btnCancel.Click += btnCancel_Click;

            // Printing Events - This hookup is correct
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }

        private void InitializeFormState()
        {
            dtpInvoiceDate.Enabled = true;
            btnDateOk.Enabled = true;
            btnDateChange.Enabled = false;

            txtDealerSearch.Enabled = false;
            btnDealerSearch.Enabled = false;
            dgvDealerList.Enabled = false;
            btnDealerOk.Enabled = false;
            btnDealerChange.Enabled = false;
            lblSelectedDealerName.Text = "Dealer's Name";
            dgvDealerList.DataSource = null;

            dgvSalesDetails.DataSource = null;
            dgvSalesDetails.Enabled = false;
            btnPrint.Enabled = false;

            btnCancel.Enabled = true;

            ClearData();
        }

        private void ClearData()
        {
            selectedDealerId = null;
            selectedDealer = null; // Clear full dealer object
            selectedInvoiceDate = null;
            dtpInvoiceDate.Value = DateTime.Today;

            ClearDealerSelection();
            ClearSalesDetails();
            lblSelectedDealerName.Text = "Dealer's Name";
            txtDealerSearch.Text = "";
        }

        private void ClearDealerSelection()
        {
            dgvDealerList.DataSource = null;
            if (dealerList != null) dealerList.Clear(); // Clear BindingList if it exists
            selectedDealerId = null;
            selectedDealer = null; // Clear full dealer object
            dgvDealerList.ClearSelection();
        }

        private void ClearSalesDetails()
        {
            dgvSalesDetails.DataSource = null;
            if (salesDetailsList != null) salesDetailsList.Clear(); // Clear BindingList if it exists
            dgvSalesDetails.ClearSelection();
        }

        // --- Date Selection Logic ---
        private void btnDateOk_Click(object sender, EventArgs e)
        {
            selectedInvoiceDate = dtpInvoiceDate.Value.Date;
            dtpInvoiceDate.Enabled = false;
            btnDateOk.Enabled = false;
            btnDateChange.Enabled = true;

            txtDealerSearch.Enabled = true;
            btnDealerSearch.Enabled = true;
            dgvDealerList.Enabled = true;
            btnDealerOk.Enabled = true; // Enable OK for dealer selection
            LoadDealersForSelectedDate();
        }

        private void btnDateChange_Click(object sender, EventArgs e)
        {
            dtpInvoiceDate.Enabled = true;
            btnDateOk.Enabled = true;
            btnDateChange.Enabled = false;

            txtDealerSearch.Enabled = false;
            btnDealerSearch.Enabled = false;
            dgvDealerList.Enabled = false;
            btnDealerOk.Enabled = false;
            btnDealerChange.Enabled = false;
            lblSelectedDealerName.Text = "Dealer's Name";
            ClearDealerSelection();
            ClearSalesDetails();
            dgvSalesDetails.Enabled = false;
            btnPrint.Enabled = false;
        }

        private void LoadDealersForSelectedDate(string searchName = null)
        {
            if (selectedInvoiceDate.HasValue)
            {
                try
                {
                    IEnumerable<DealerModel> dealers = saleRepository.GetDealersBySaleDate(selectedInvoiceDate.Value, searchName);
                    dealerList = new BindingList<DealerModel>(dealers.ToList());
                    dgvDealerList.DataSource = dealerList;
                    ConfigureDealerListDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading dealers: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigureDealerListDataGridView()
        {
            dgvDealerList.AutoGenerateColumns = false;
            dgvDealerList.Columns.Clear();
            dgvDealerList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DealerId",
                HeaderText = "ID",
                DataPropertyName = "DealerId",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvDealerList.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DealerName",
                HeaderText = "Dealer Name",
                DataPropertyName = "DealerName",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvDealerList.Refresh();
        }

        // --- Dealer Search and Selection Logic ---
        private void btnDealerSearch_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceDate.HasValue)
            {
                LoadDealersForSelectedDate(txtDealerSearch.Text.Trim());
            }
            else
            {
                MessageBox.Show("Please select a date first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDealerSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDealerSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dgvDealerList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDealerList.SelectedRows.Count > 0 && dgvDealerList.Enabled)
            {
                // Store the full selected DealerModel when selected in the grid
                selectedDealer = dgvDealerList.SelectedRows[0].DataBoundItem as DealerModel;
                if (selectedDealer != null)
                {
                    lblSelectedDealerName.Text = selectedDealer.DealerName; // Update label immediately on selection
                    btnDealerOk.Enabled = true;
                }
            }
            else
            {
                // Only clear if no selection AND not locked
                if (dgvDealerList.Enabled)
                {
                    selectedDealer = null;
                    lblSelectedDealerName.Text = "Dealer's Name";
                    btnDealerOk.Enabled = false;
                }
            }
        }

        private void btnDealerOk_Click(object sender, EventArgs e)
        {
            if (selectedDealer != null)
            {
                selectedDealerId = selectedDealer.DealerId; // Set ID from the full model
                lblSelectedDealerName.Text = selectedDealer.DealerName; // Reconfirm label

                // Lock dealer controls
                txtDealerSearch.Enabled = false;
                btnDealerSearch.Enabled = false;
                dgvDealerList.Enabled = false;
                btnDealerOk.Enabled = false;
                btnDealerChange.Enabled = true;

                // Enable sales details grid and print button
                dgvSalesDetails.Enabled = true;
                btnPrint.Enabled = true;

                LoadSalesDetailsForSelectedDealerAndDate();
            }
            else
            {
                MessageBox.Show("Please select a dealer from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDealerChange_Click(object sender, EventArgs e)
        {
            txtDealerSearch.Enabled = true;
            btnDealerSearch.Enabled = true;
            dgvDealerList.Enabled = true;
            btnDealerOk.Enabled = false; // Disable OK until new selection
            btnDealerChange.Enabled = true; // Keep enabled to allow immediate re-selection

            selectedDealerId = null;
            selectedDealer = null; // Clear full dealer object
            lblSelectedDealerName.Text = "Dealer's Name";
            ClearSalesDetails();
            dgvDealerList.ClearSelection(); // Clear selection in DGV
            btnPrint.Enabled = false;
            dgvSalesDetails.Enabled = false;

            LoadDealersForSelectedDate(); // Reload dealers for the current date to allow new selection
        }

        private void LoadSalesDetailsForSelectedDealerAndDate()
        {
            if (selectedDealerId.HasValue && selectedInvoiceDate.HasValue)
            {
                try
                {
                    IEnumerable<SaleDetailModel> details = saleRepository.GetSaleDetailsByDealerAndDate(selectedDealerId.Value, selectedInvoiceDate.Value);
                    salesDetailsList = new BindingList<SaleDetailModel>(details.ToList());
                    dgvSalesDetails.DataSource = salesDetailsList;
                    ConfigureSalesDetailsDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading sales details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ClearSalesDetails();
            }
        }

        private void ConfigureSalesDetailsDataGridView()
        {
            dgvSalesDetails.AutoGenerateColumns = false;
            dgvSalesDetails.Columns.Clear();

            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ProductName",
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Quantity",
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SellingPrice",
                HeaderText = "Selling Price",
                DataPropertyName = "SellingPrice",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LineTotal",
                HeaderText = "Line Total",
                DataPropertyName = "LineTotal",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvSalesDetails.Refresh();
        }


        // InvoiceForm.cs (inside the printDocument1_PrintPage method)

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font bodyFont = new Font("Arial", 10); // Font for regular text
            Font headerFont = new Font("Arial", 12, FontStyle.Bold); // Font for sub-headers
            Font titleFont = new Font("Arial", 16, FontStyle.Bold); // Font for main titles
            SolidBrush brush = new SolidBrush(Color.Black);

            float lineHeight = bodyFont.GetHeight(graphics) + 4; // Spacing between lines
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float pageWidth = e.MarginBounds.Width; // Usable page width

            // Define fixed column positions (can be adjusted)
            float colProduct = x;
            float colQuantity = x + 200;
            float colSellingPrice = x + 300;
            float colLineTotal = x + 400;

            // --- Company Information (Only on the first page) ---
            if (currentPage == 0)
            {
                // Company Name - Centered
                string companyName = "THARUKA FOOD PRODUCTS";
                SizeF companyNameSize = graphics.MeasureString(companyName, new Font("Arial", 20, FontStyle.Bold));
                float xCenteredCompanyName = x + (pageWidth - companyNameSize.Width) / 2;
                graphics.DrawString(companyName, new Font("Arial", 20, FontStyle.Bold), brush, xCenteredCompanyName, y);
                y += companyNameSize.Height + 5;

                // Address - Centered
                string address = "247/20, Narndanawatta, Udagama, Ampitiya, Kandy.";
                SizeF addressSize = graphics.MeasureString(address, bodyFont);
                float xCenteredAddress = x + (pageWidth - addressSize.Width) / 2;
                graphics.DrawString(address, bodyFont, brush, xCenteredAddress, y);
                y += lineHeight;

                // Telephone - Centered
                string telephone = "Tel: 077 155 5397, 075 910 4316";
                SizeF telephoneSize = graphics.MeasureString(telephone, bodyFont);
                float xCenteredTelephone = x + (pageWidth - telephoneSize.Width) / 2;
                graphics.DrawString(telephone, bodyFont, brush, xCenteredTelephone, y);
                y += lineHeight;

                // Email - Centered
                string email = "Email: tharukafoodproducts1@gmail.com";
                SizeF emailSize = graphics.MeasureString(email, bodyFont);
                float xCenteredEmail = x + (pageWidth - emailSize.Width) / 2;
                graphics.DrawString(email, bodyFont, brush, xCenteredEmail, y);
                y += lineHeight * 2; // Extra space after company info

                // --- Sale Information (Left Aligned for these specific details, as usually not centered) ---
                // If you want these centered, apply the same centering logic as above.
                // I'll keep them left-aligned as it's more common for invoice details after the main header.
                int saleId = salesDetailsList.FirstOrDefault()?.SaleId ?? 0;
                graphics.DrawString($"Sale ID: {saleId}", headerFont, brush, x, y);
                y += lineHeight;
                graphics.DrawString($"Date: {selectedInvoiceDate?.ToShortDateString()}", headerFont, brush, x, y);
                y += lineHeight;
                graphics.DrawString($"Dealer: {selectedDealer?.DealerName}", headerFont, brush, x, y);
                y += lineHeight;
                graphics.DrawString($"Address: {selectedDealer?.DealerAddress}", bodyFont, brush, x, y);
                y += lineHeight;
                graphics.DrawString($"Phone: {selectedDealer?.DealerPhone}", bodyFont, brush, x, y);
                y += lineHeight * 2; // Extra space before product details table header

                // --- Column Headers for Products (Left aligned for table columns) ---
                graphics.DrawString("Product", headerFont, brush, colProduct, y);
                graphics.DrawString("Qty", headerFont, brush, colQuantity, y);
                graphics.DrawString("Selling Price", headerFont, brush, colSellingPrice, y);
                graphics.DrawString("Line Total", headerFont, brush, colLineTotal, y);
                y += headerFont.GetHeight(graphics) + 2; // Space for header line
                graphics.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y); // Separator line
                y += 5; // Small gap after line
            }
            else // For subsequent pages
            {
                y = e.MarginBounds.Top; // Start from top of new page
                                        // Optional: Repeat column headers on every page if the invoice can be very long
                /*
                graphics.DrawString("Product", headerFont, brush, colProduct, y);
                graphics.DrawString("Qty", headerFont, brush, colQuantity, y);
                graphics.DrawString("Selling Price", headerFont, brush, colSellingPrice, y);
                graphics.DrawString("Line Total", headerFont, brush, colLineTotal, y);
                y += headerFont.GetHeight(graphics) + 2;
                graphics.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y);
                y += 5;
                */
            }

            // --- Product Details (Loop through salesDetailsList) ---
            // Make sure 'currentPrintRow' is declared at the class level (private int currentPrintRow = 0;)
            int rowsPerPage = (int)((e.MarginBounds.Bottom - y) / lineHeight);

            for (; currentPrintRow < salesDetailsList.Count; currentPrintRow++)
            {
                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    currentPage++;
                    return;
                }

                SaleDetailModel detail = salesDetailsList[currentPrintRow];

                graphics.DrawString(detail.ProductName, bodyFont, brush, colProduct, y);
                graphics.DrawString(detail.Quantity.ToString(), bodyFont, brush, colQuantity, y);
                graphics.DrawString(detail.SellingPrice.ToString("F2"), bodyFont, brush, colSellingPrice, y);
                graphics.DrawString(detail.LineTotal.ToString("F2"), bodyFont, brush, colLineTotal, y);

                y += lineHeight;
            }

            // --- Total Amount and Signatures (Only on the LAST page) ---
            if (!e.HasMorePages)
            {
                // Ensure space for total and signatures
                if (y + (lineHeight * 5) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    currentPage++;
                    return;
                }

                y += lineHeight * 2; // Space before total

                decimal invoiceTotal = salesDetailsList.Sum(item => item.LineTotal);
                string totalString = $"Total: {invoiceTotal.ToString("N")}";
                SizeF totalStringSize = graphics.MeasureString(totalString, headerFont);
                // Align total to the right, under the 'Line Total' column or slightly to its left
                float xTotal = colLineTotal - totalStringSize.Width + (graphics.MeasureString("Line Total", headerFont).Width); // Align with right of Line Total column

                graphics.DrawString(totalString, headerFont, brush, xTotal, y);
                y += lineHeight * 3; // Space before signatures

                // Signature Fields (Left and Right aligned)
                float signatureLineLength = 200; // Length of the signature line
                float signatureY = y;

                // Customer Signature
                float customerSignatureX = x + 50; // Offset from left margin
                graphics.DrawString("-------------------------", bodyFont, brush, customerSignatureX, signatureY);
                graphics.DrawString("Customer Signature", bodyFont, brush, customerSignatureX, signatureY + lineHeight);

                // Authorized Signature
                float authorizedSignatureX = e.MarginBounds.Right - 200; // Offset from right margin
                graphics.DrawString("-------------------------", bodyFont, brush, authorizedSignatureX, signatureY);
                graphics.DrawString("Authorized Signature", bodyFont, brush, authorizedSignatureX, signatureY + lineHeight);
            }

            e.HasMorePages = false;
            currentPage = 0;
            currentPrintRow = 0;
        }




        // --- Cancel Button ---
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitializeFormState(); // Reset the entire form to its initial state
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (salesDetailsList != null && salesDetailsList.Any())
            {
                currentPage = 0;
                currentPrintRow = 0;// Always reset the page counter for a new print job
                printPreviewDialog1.Document = printDocument1; // Make sure this is set, usually done in designer
                printPreviewDialog1.ShowDialog(); // This is the ONLY place this should be called
            }
            else
            {
                MessageBox.Show("No sales details to print.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
    }
}
