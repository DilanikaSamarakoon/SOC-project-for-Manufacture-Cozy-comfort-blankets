using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Seller.des
{
    public partial class NewOrderForm : Form
    {
        private readonly List<CreateOrderItemDto> _orderItems = new List<CreateOrderItemDto>();
        private readonly SellerApiService _apiService;

        public NewOrderForm()
        {
            InitializeComponent();
            _apiService = new SellerApiService();
            numQuantity.Minimum = 1;
            numQuantity.Value = 1;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Try to convert the Product ID text to a number
            if (!int.TryParse(txtProductId.Text, out int productId) || productId <= 0)
            {
                MessageBox.Show("Please enter a valid, positive Product ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create the new item DTO
            var newItem = new CreateOrderItemDto
            {
                ProductId = productId,
                Quantity = (int)numQuantity.Value
            };

            // Add the item to our list
            _orderItems.Add(newItem);

            // --- THIS IS THE KEY FIX FOR THE DATAGRIDVIEW ---
            // To reliably refresh the grid, we set its source to null first,
            // then re-assign it to our list.
            dgvOrderItems.DataSource = null;
            dgvOrderItems.DataSource = _orderItems;
            // ------------------------------------------------

            // Clear the inputs for the next item
            txtProductId.Clear();
            numQuantity.Value = 1;
            txtProductId.Focus();
        }

        private async void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if (_orderItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item to the order.", "Empty Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSubmitOrder.Enabled = false;
            btnSubmitOrder.Text = "Submitting...";

            var newOrder = new CreateOrderDto
            {
                SellerId = 1, // Example SellerId
                Items = _orderItems
            };

            try
            {
                bool success = await _apiService.CreateOrderAsync(newOrder);
                if (success)
                {
                    MessageBox.Show("Order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create the order. The API reported an error.", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmitOrder.Enabled = true;
                btnSubmitOrder.Text = "Submit Order";
            }
        }
    }
}