using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TharukaPOS.Forms.Models;
using TharukaPOS.Forms._Repositories;
using TharukaPOS.Forms.Views;
using System.ComponentModel;
using System.Windows.Forms;

namespace TharukaPOS.Forms.Presenters
{
    public class SalesPresenter
    {
        private ISalesView _view;
        private IDealerRepository _dealerRepository;
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;

        // Stores the currently selected dealer
        private DealerModel _selectedDealer; // CHANGED: Dealer to DealerModel
        // Stores the currently selected product for adding to bill
        private ProductModel _selectedProduct; // CHANGED: Product to ProductModel
        // Stores items in the current bill
        private BindingList<BillItem> _billItems;

        // Constructor
        public SalesPresenter(ISalesView view, IDealerRepository dealerRepository, IProductRepository productRepository, ISaleRepository saleRepository)
        {
            _view = view;
            _dealerRepository = dealerRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;

            // Initialize bill items
            _billItems = new BindingList<BillItem>();
            _view.SetBillListBindingSource(_billItems);

            // Subscribe to events from the view
            _view.LoadDealers += LoadAllDealers;
            _view.SelectDealer += SelectDealerFromGrid;
            _view.ChangeDealer += HandleChangeDealer;
            _view.LoadProducts += LoadAllProducts;
            _view.SelectProduct += SelectProductFromGrid;
            _view.AddProductToBill += AddProductToBill;
            _view.EditBillItem += EditBillItemQuantity;
            _view.DeleteBillItem += DeleteBillItem;
            _view.SaveSale += SaveSale;
            _view.CancelSale += CancelSale;

            // Initial load of data
            LoadAllDealers(this, EventArgs.Empty);
            LoadAllProducts(this, EventArgs.Empty);

            // Set initial UI state
            _view.SetDealerInputState(true);
            _view.ClearProductInput();
            _view.SetProductInputState(false);
            _view.SetBillControlsState(false);
            _view.SetSaleButtonsState(false);
        }

        private void LoadAllDealers(object sender, EventArgs e)
        {
            try
            {
                // Ensure your IDealerRepository.GetAll() returns IEnumerable<DealerModel>
                var dealers = _dealerRepository.GetAll();
                _view.SetDealerListBindingSource(dealers);
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Error loading dealers: " + ex.Message, "Error", MessageBoxIcon.Error);
            }
        }

        private void SelectDealerFromGrid(object sender, EventArgs e)
        {
            if (_view.SelectedDealerId > 0)
            {
                // Ensure your IDealerRepository.GetById() returns DealerModel
                _selectedDealer = _dealerRepository.GetById(_view.SelectedDealerId);
                if (_selectedDealer != null)
                {
                    _view.DealerName = _selectedDealer.DealerName;
                    _view.SetDealerInputState(false);
                    _view.SetProductInputState(true);
                }
            }
        }

        private void HandleChangeDealer(object sender, EventArgs e)
        {
            _selectedDealer = null;
            _view.DealerName = "";
            _view.SetDealerInputState(true);
            _view.SetProductInputState(false);
            CancelSale(this, EventArgs.Empty);
        }

        private void LoadAllProducts(object sender, EventArgs e)
        {
            try
            {
                // Ensure your IProductRepository.GetAll() returns IEnumerable<ProductModel>
                var products = _productRepository.GetAll();
                _view.SetProductListBindingSource(products);
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Error loading products: " + ex.Message, "Error", MessageBoxIcon.Error);
            }
        }

        private void SelectProductFromGrid(object sender, EventArgs e)
        {
            if (_view.SelectedProductId > 0)
            {
                // Ensure your IProductRepository.GetById() returns ProductModel
                _selectedProduct = _productRepository.GetById(_view.SelectedProductId);
                if (_selectedProduct != null)
                {
                    _view.ProductName = _selectedProduct.ProductName;
                    _view.BuyingPrice = _selectedProduct.BuyingPrice.ToString("N2");
                    _view.SellingPrice = _selectedProduct.SellingPrice.ToString("N2");
                    _view.Quantity = "";
                }
            }
        }

        private void AddProductToBill(object sender, EventArgs e)
        {
            if (_selectedProduct == null)
            {
                _view.ShowMessage("Please select a product.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(_view.Quantity, out int quantity) || quantity <= 0)
            {
                _view.ShowMessage("Please enter a valid quantity.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }

            // Check if product already exists in bill
            BillItem existingItem = _billItems.FirstOrDefault(item => item.ProductId == _selectedProduct.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.LineTotal = existingItem.Quantity * existingItem.SellingPrice;
            }
            else
            {
                _billItems.Add(new BillItem
                {
                    ProductId = _selectedProduct.ProductId,
                    ProductName = _selectedProduct.ProductName,
                    Quantity = quantity,
                    BuyingPrice = _selectedProduct.BuyingPrice,
                    SellingPrice = _selectedProduct.SellingPrice,
                    LineTotal = quantity * _selectedProduct.SellingPrice
                });
            }

            UpdateTotal();
            _view.ClearProductInput();
            _view.SetBillControlsState(true);
            _view.SetSaleButtonsState(true);
        }

        private void EditBillItemQuantity(object sender, EventArgs e)
        {
            if (_view.SelectedBillItemIndex >= 0 && _view.SelectedBillItemIndex < _billItems.Count)
            {
                BillItem itemToEdit = _billItems[_view.SelectedBillItemIndex];

                string newQuantityStr = _view.ShowInputDialog("Enter new quantity for " + itemToEdit.ProductName + ":", "Edit Quantity", itemToEdit.Quantity.ToString());

                if (int.TryParse(newQuantityStr, out int newQuantity) && newQuantity > 0)
                {
                    itemToEdit.Quantity = newQuantity;
                    itemToEdit.LineTotal = newQuantity * itemToEdit.SellingPrice;
                    _billItems.ResetItem(_view.SelectedBillItemIndex);
                    UpdateTotal();
                }
                else if (!string.IsNullOrEmpty(newQuantityStr))
                {
                    _view.ShowMessage("Invalid quantity entered.", "Validation Error", MessageBoxIcon.Warning);
                }
            }
        }

        private void DeleteBillItem(object sender, EventArgs e)
        {
            if (_view.SelectedBillItemIndex >= 0 && _view.SelectedBillItemIndex < _billItems.Count)
            {
                if (_view.ConfirmAction("Are you sure you want to remove this item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _billItems.RemoveAt(_view.SelectedBillItemIndex);
                    UpdateTotal();

                    if (_billItems.Count == 0)
                    {
                        _view.SetBillControlsState(false);
                        _view.SetSaleButtonsState(false);
                    }
                }
            }
        }

        private void UpdateTotal()
        {
            decimal total = _billItems.Sum(item => item.LineTotal);
            _view.TotalAmount = total.ToString("N2");
        }

        private void SaveSale(object sender, EventArgs e)
        {
            if (_selectedDealer == null)
            {
                _view.ShowMessage("Please select a dealer before saving the sale.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }
            if (!_billItems.Any())
            {
                _view.ShowMessage("No products in the bill to save.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Create Order
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    DealerId = _selectedDealer.DealerId, // This assumes DealerModel has DealerId
                    TotalAmount = _billItems.Sum(item => item.LineTotal)
                };

                int orderId = _saleRepository.SaveOrder(order);

                // 2. Create OrderDetails
                var orderDetails = new List<OrderDetail>();
                foreach (var item in _billItems)
                {
                    orderDetails.Add(new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Discount = 0,
                        LineTotal = item.LineTotal
                    });
                }

                _saleRepository.SaveOrderDetails(orderDetails);

                _view.ShowMessage("Sale saved successfully!", "Success", MessageBoxIcon.Information);
                CancelSale(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Error saving sale: " + ex.Message, "Error", MessageBoxIcon.Error);
            }
        }

        private void CancelSale(object sender, EventArgs e)
        {
            _billItems.Clear();
            UpdateTotal();
            _view.ClearProductInput();
            _view.SetBillControlsState(false);
            _view.SetSaleButtonsState(false);

            _selectedDealer = null;
            _view.DealerName = "";
            _view.SetDealerInputState(true);
            _view.SetProductInputState(false);
            _view.ClearProductInput();
        }
    }
}
