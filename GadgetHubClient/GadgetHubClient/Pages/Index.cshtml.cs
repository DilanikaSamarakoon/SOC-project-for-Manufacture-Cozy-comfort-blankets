using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHubClient.Models; // Import your client models
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq; // For .Any()

namespace GadgetHubClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration; // To get GadgetHub API URL

        public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public CustomerOrderRequest CustomerOrder { get; set; } = new CustomerOrderRequest { OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper() };

        public GadgetHubOrderSummary? OrderSummary { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // Initialize with a default item for easier testing
            if (!CustomerOrder.Items.Any())
            {
                CustomerOrder.Items.Add(new CustomerOrderItem { ProductId = "PROD001", Quantity = 1 });
            }
        }

        public async Task<IActionResult> OnPostPlaceOrder()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the form errors.";
                return Page();
            }

            if (!CustomerOrder.Items.Any() || CustomerOrder.Items.All(item => string.IsNullOrEmpty(item.ProductId) || item.Quantity <= 0))
            {
                ErrorMessage = "Please add at least one valid product to your order.";
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("GadgetHubApi");
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(CustomerOrder, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("Orders/place-order", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    OrderSummary = await JsonSerializer.DeserializeAsync<GadgetHubOrderSummary>(
                        responseStream,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    );
                    ErrorMessage = string.Empty; // Clear any previous errors
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error placing order: {response.StatusCode} - {errorContent}";
                    OrderSummary = null;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Network error: {ex.Message}";
                OrderSummary = null;
            }
            catch (JsonException ex)
            {
                ErrorMessage = $"Error parsing API response: {ex.Message}";
                OrderSummary = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                OrderSummary = null;
            }

            return Page();
        }

        public IActionResult OnPostAddItem()
        {
            CustomerOrder.Items.Add(new CustomerOrderItem { ProductId = "", Quantity = 1 });
            return Page();
        }

        public IActionResult OnPostRemoveItem(int index)
        {
            if (index >= 0 && index < CustomerOrder.Items.Count)
            {
                CustomerOrder.Items.RemoveAt(index);
            }
            return Page();
        }
    }
}