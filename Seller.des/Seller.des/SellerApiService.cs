// All using statements needed for this file
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

// The namespace for your project
namespace Seller.des
{
    // ====================================================================
    // DTO DEFINITIONS (These are correct)
    // ====================================================================
    public class SellerStockItem
    {
        public int BlanketId { get; set; }
        public string BlanketName { get; set; }
        public int Quantity { get; set; }
    }

    public class SellerOrder
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
    }

    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        public int SellerId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }

    // ====================================================================
    // YOUR API SERVICE CLASS
    // ====================================================================
    public class SellerApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:44328";

        public SellerApiService()
        {
            _httpClient = new HttpClient();
        }

        // Method to get a seller's stock
        public async Task<List<SellerStockItem>> GetMyStockAsync(int sellerId)
        {
            var fullUrl = $"{_apiBaseUrl}/api/Sellers/{sellerId}/stock";
            var response = await _httpClient.GetAsync(fullUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SellerStockItem>>(jsonString);
            }
            return new List<SellerStockItem>();
        }

        // Method to get a seller's order history
        public async Task<List<SellerOrder>> GetMyOrdersAsync()
        {
            var fullUrl = $"{_apiBaseUrl}/api/orders";
            var response = await _httpClient.GetAsync(fullUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SellerOrder>>(jsonString);
            }
            return new List<SellerOrder>();
        }

        // ====================================================================
        // THIS IS THE METHOD THAT WAS MOVED TO THE CORRECT LOCATION
        // ====================================================================
        public async Task<bool> CreateOrderAsync(CreateOrderDto newOrder)
        {
            // Convert the C# order object to a JSON string
            var jsonContent = JsonConvert.SerializeObject(newOrder);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Send the POST request to the /api/orders endpoint
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/orders", httpContent);

            // Return true if the API responded with a success code (like 201 Created)
            return response.IsSuccessStatusCode;
        }

    } // <--- The SellerApiService class ends HERE

} // <--- The namespace ends HERE