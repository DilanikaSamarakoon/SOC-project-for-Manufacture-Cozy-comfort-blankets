using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms._Repositories;

namespace TharukaPOS.Forms
{
    public partial class SalesForm : Form
    {
        private IDealerRepository dealerRepository;
        private IProductRepository productRepository; // You will need to create this if not already present
        private ISaleRepository saleRepository;

        private DealerModel selectedDealer; // Stores the currently selected dealer
        private ProductModel selectedProduct; // Stores the currently selected product

        // This list will hold the items added to the current bill (cart)
        private BindingList<SaleDetailModel> currentBillDetails;
        private decimal currentTotalAmount = 0;

        public SalesForm(string connectionString) : this() // Call parameterless constructor
        {
            dealerRepository = new DealerRepository(connectionString);
            productRepository = new ProductRepository(connectionString); // Assuming you have a ProductRepository
            saleRepository = new SaleRepository(connectionString);

            // Initialize BindingList for the bill DataGridView
            currentBillDetails = new BindingList<SaleDetailModel>();
            dgvBill.DataSource = currentBillDetails;
            dgvBill.SelectionChanged += dgvBill_SelectionChanged; // CRITICAL for enabling/disabling
            //btnEditBillItem.Click += btnEditBillItem_Click;
            //btnDeleteBillItem.Click += btnDeleteBillItem_Click;

            // Configure dgvBill columns for display
            ConfigureBillDataGridView();

            LoadDealersToGrid();
            LoadProductsToGrid();

            ResetForm(); // Set initial state
        }

        public SalesForm()
        {
            InitializeComponent();
        }

        // --- UI State Management ---
        private void ResetForm()
        {
            selectedDealer = null;
            selectedProduct = null;
            currentBillDetails.Clear();
            currentTotalAmount = 0;
            UpdateTotalLabel();

            // Dealer section
            txtDealerName.Text = "";
            txtDealerName.Enabled = false; // Cannot be changed directly by typing
            btnDealerOK.Enabled = false;
            btnDealerChange.Enabled = true; // Allow initial dealer selection
            dgvDealers.Enabled = true; // Dealers grid active for selection
            dgvDealers.ClearSelection();

            // Product section
            txtProductName.Text = "";
            txtBuyingPrice.Text = "";
            txtSellingPrice.Text = "";
            txtQuantity.Text = "";
            txtQuantity.Enabled = false;
            btnAddProduct.Enabled = false;
            dgvProducts.Enabled = true; // Products grid active for selection
            dgvProducts.ClearSelection();

            // Bill section
            btnEditBillItem.Enabled = false;
            btnDeleteBillItem.Enabled = false;
            dgvBill.ClearSelection();

            // Sale Action buttons
            btnSaveSale.Enabled = false; // Only enable if there are items in bill
            btnCancelSale.Enabled = false;
        }

        private void ConfigureBillDataGridView()
        {
            dgvBill.AutoGenerateColumns = false; // Manually define columns
            dgvBill.Columns.Clear();

            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Product", ReadOnly = true });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty", ReadOnly = true });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BuyingPriceAtSale", HeaderText = "Buyiny Price", ReadOnly = true });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SellingPrice", HeaderText = "Selling Price", ReadOnly = true });
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LineTotal", HeaderText = "Line Total", ReadOnly = true });

            // Optional: Hide internal IDs if not relevant to the user
            dgvBill.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", Visible = false });
            
        }


        private void UpdateTotalLabel()
        {
            lblTotalAmount.Text = currentTotalAmount.ToString("N2"); // Format as currency
        }

        private void CheckEnableSaveCancelButtons()
        {
            btnSaveSale.Enabled = currentBillDetails.Any();
            btnCancelSale.Enabled = currentBillDetails.Any();
        }

        // --- Data Loading ---
        private void LoadDealersToGrid()
        {
            try
            {
                // Only show DealerName in the grid
                var dealers = dealerRepository.GetAll().Select(d => new { d.DealerId, d.DealerName }).ToList();
                dgvDealers.DataSource = dealers;
                dgvDealers.Columns["DealerId"].Visible = false; // Hide ID
                dgvDealers.Columns["DealerName"].HeaderText = "Dealer Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dealers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductsToGrid()
        {
            try
            {
                // Assuming ProductRepository has GetAll method
                var products = productRepository.GetAll().ToList(); // Get all product details
                dgvProducts.DataSource = products;
                // You might want to hide some columns like ProductId, Stock, etc.
                if (dgvProducts.Columns.Contains("ProductId")) dgvProducts.Columns["ProductId"].Visible = false;
                if (dgvProducts.Columns.Contains("ProductDescription")) dgvProducts.Columns["ProductDescription"].Visible = false;
                if (dgvProducts.Columns.Contains("Stock")) dgvProducts.Columns["Stock"].Visible = false; // Assuming Stock column
                // Ensure correct header texts
                if (dgvProducts.Columns.Contains("ProductName")) dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                if (dgvProducts.Columns.Contains("BuyingPrice")) dgvProducts.Columns["BuyingPrice"].HeaderText = "Buying Price";
                if (dgvProducts.Columns.Contains("SellingPrice")) dgvProducts.Columns["SellingPrice"].HeaderText = "Sell Price";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Dealer Section Logic ---
        private void dgvDealers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDealers.SelectedRows.Count > 0)
            {
                // Retrieve the full DealerModel using the DealerId from the selected row
                int dealerId = Convert.ToInt32(dgvDealers.SelectedRows[0].Cells["DealerId"].Value);
                // Assuming you can get a single dealer by ID from your repository
                // You might need to add GetById method to IDealerRepository and DealerRepository
                // For simplicity, let's just use the selected name for now
                selectedDealer = dealerRepository.GetAll().FirstOrDefault(d => d.DealerId == dealerId); // A bit inefficient, but works if GetAll is small
                if (selectedDealer != null)
                {
                    txtDealerName.Text = selectedDealer.DealerName;
                    btnDealerOK.Enabled = true;
                }
            }
            else
            {
                txtDealerName.Text = "";
                btnDealerOK.Enabled = false;
                selectedDealer = null;
            }
        }

        private void btnDealerOK_Click(object sender, EventArgs e)
        {
            if (selectedDealer != null)
            {
                dgvDealers.Enabled = false; // Disable dealer selection grid
                btnDealerOK.Enabled = false; // Disable OK button
                btnDealerChange.Enabled = true; // Enable Change button
                // Optionally disable search for dealers
            }
            else
            {
                MessageBox.Show("Please select a dealer first.", "No Dealer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDealerChange_Click(object sender, EventArgs e)
        {
            selectedDealer = null;
            txtDealerName.Text = "";
            dgvDealers.Enabled = true; // Re-enable dealer selection
            btnDealerOK.Enabled = false; // Disable OK until new selection
            btnDealerChange.Enabled = true; // Keep change enabled for initial selection
            dgvDealers.ClearSelection();
            // Optionally enable search for dealers
        }

        // --- Product Section Logic ---
        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["ProductId"].Value);
                // Get the full ProductModel from the repository
                selectedProduct = productRepository.GetAll().FirstOrDefault(p => p.ProductId == productId); // Again, GetAll() might be inefficient

                if (selectedProduct != null)
                {
                    txtProductName.Text = selectedProduct.ProductName;
                    txtBuyingPrice.Text = selectedProduct.BuyingPrice.ToString("N2");
                    txtSellingPrice.Text = selectedProduct.SellingPrice.ToString("N2");
                    txtQuantity.Enabled = true; // Enable quantity input
                    txtQuantity.Text = "1"; // Default quantity to 1
                    txtQuantity.Focus();
                    btnAddProduct.Enabled = true; // Enable add button
                }
            }
            else
            {
                selectedProduct = null;
                txtProductName.Text = "";
                txtBuyingPrice.Text = "";
                txtSellingPrice.Text = "";
                txtQuantity.Text = "";
                txtQuantity.Enabled = false;
                btnAddProduct.Enabled = false;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (selectedDealer == null)
            {
                MessageBox.Show("Please select a dealer first.", "Missing Dealer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product first.", "Missing Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (a positive number).", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            // Check if product already exists in bill to update quantity
            SaleDetailModel existingItem = currentBillDetails.FirstOrDefault(item => item.ProductId == selectedProduct.ProductId);

            if (existingItem != null)
            {
                // Update quantity of existing item
                existingItem.Quantity += quantity;
                existingItem.LineTotal = existingItem.Quantity * existingItem.SellingPrice;
            }
            else
            {
                // Add new item to bill
                SaleDetailModel newDetail = new SaleDetailModel
                {
                    ProductId = selectedProduct.ProductId,
                    ProductName = selectedProduct.ProductName,
                    Quantity = quantity,
                    SellingPrice = selectedProduct.SellingPrice,
                    BuyingPriceAtSale = selectedProduct.BuyingPrice, // Store buying price at time of sale
                    LineTotal = quantity * selectedProduct.SellingPrice
                };
                currentBillDetails.Add(newDetail);
            }

            UpdateBillAndTotal();
            ClearProductSelection(); // Clear product input fields for next item
            CheckEnableSaveCancelButtons();
        }

        private void ClearProductSelection()
        {
            selectedProduct = null;
            txtProductName.Text = "";
            txtBuyingPrice.Text = "";
            txtSellingPrice.Text = "";
            txtQuantity.Text = "";
            txtQuantity.Enabled = false;
            btnAddProduct.Enabled = false;
            dgvProducts.ClearSelection();
        }

        private void UpdateBillAndTotal()
        {
            currentTotalAmount = currentBillDetails.Sum(item => item.LineTotal);
            UpdateTotalLabel();
            dgvBill.Refresh(); // Ensure DataGridView updates
        }

        // --- Bill Management Logic ---
        private void dgvBill_SelectionChanged(object sender, EventArgs e)
        {
            // Enable Edit/Delete buttons if a row is selected in the bill grid
            if (dgvBill.SelectedRows.Count > 0)
            {
                btnEditBillItem.Enabled = true;
                btnDeleteBillItem.Enabled = true;
            }
            else
            {
                // Disable buttons if no row is selected
                btnEditBillItem.Enabled = false;
                btnDeleteBillItem.Enabled = false;
            }
        }

        private void btnEditBillItem_Click(object sender, EventArgs e)
        {
            if (dgvBill.SelectedRows.Count > 0)
            {
                SaleDetailModel selectedItem = dgvBill.SelectedRows[0].DataBoundItem as SaleDetailModel;
                if (selectedItem != null)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter new quantity:", "Edit Quantity", selectedItem.Quantity.ToString());
                    if (int.TryParse(input, out int newQuantity) && newQuantity >= 0)
                    {
                        if (newQuantity == 0) // If quantity is 0, delete the item
                        {
                            currentBillDetails.Remove(selectedItem);
                        }
                        else
                        {
                            selectedItem.Quantity = newQuantity;
                            selectedItem.LineTotal = newQuantity * selectedItem.SellingPrice;
                        }
                        UpdateBillAndTotal();
                    }
                    else if (!string.IsNullOrWhiteSpace(input)) // If input was not empty but invalid
                    {
                        MessageBox.Show("Invalid quantity entered. Please enter a positive number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }




        private void btnDeleteBillItem_Click(object sender, EventArgs e)
        {
            if (dgvBill.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to remove this item from the bill?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    SaleDetailModel selectedItem = dgvBill.SelectedRows[0].DataBoundItem as SaleDetailModel;
                    if (selectedItem != null)
                    {
                        currentBillDetails.Remove(selectedItem);
                        UpdateBillAndTotal();
                        CheckEnableSaveCancelButtons();
                    }
                }
            }
        }

        // --- Sale Action Buttons ---
        private void btnSaveSale_Click(object sender, EventArgs e)
        {
            if (selectedDealer == null)
            {
                MessageBox.Show("Please select a dealer before saving the sale.", "Missing Dealer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!currentBillDetails.Any())
            {
                MessageBox.Show("The bill is empty. Add products before saving the sale.", "Empty Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaleModel newSale = new SaleModel
            {
                DealerId = selectedDealer.DealerId,
                SaleDate = DateTime.Now,
                TotalAmount = currentTotalAmount,
                SaleDetails = currentBillDetails.ToList() // Convert BindingList to List for the model
            };

            try
            {
                saleRepository.SaveSale(newSale);
                MessageBox.Show("Sale saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadAllProducts();// Clear the form after saving
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save sale: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllProducts()
        {
            try
            {
                IEnumerable<ProductModel> products = productRepository.GetAll();
                dgvProducts.DataSource = products.ToList(); // Convert to List for BindingSource
                                                                     // If you're not using BindingSource, assign directly:
                                                                     // dgvProducts.DataSource = products.ToList();

                // Also, consider refreshing the low stock products display if you have one
                // RefreshLowStockProductsDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancelSale_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to cancel the current sale and clear the bill?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                ResetForm(); // Reset the entire form
            }
        }

        
    }
}
