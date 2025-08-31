using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manufacuter.de
{
    public partial class ManageProductsForm : Form
    {
        private readonly ManufacturerApiService _apiService = new ManufacturerApiService();

        public ManageProductsForm()
        {
            InitializeComponent();
        }

        private async void ManageProductsForm_Load(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            // Get data from the API and show it in the grid
            dgvProducts.DataSource = await _apiService.GetProductsAsync();
        }

        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Product Name cannot be empty.", "Validation Error");
                return;
            }

            // 1. Create the data object from the form's inputs
            var newProduct = new CreateProduct
            {
                Name = txtName.Text,
                Cost = numCost.Value,
                InventoryCount = (int)numInventory.Value
            };

            // 2. Call the service to send the data to the API
            var result = await _apiService.AddProductAsync(newProduct);

            // 3. Check the result and give feedback
            if (result != null)
            {
                MessageBox.Show("Product added successfully!");
                txtName.Clear();
                numCost.Value = 0;
                numInventory.Value = 0;
                await LoadProducts(); // Refresh the grid to show the new product
            }
            else
            {
                MessageBox.Show("Failed to add product. Please check the API is running.", "API Error");
            }
        }
    }
}