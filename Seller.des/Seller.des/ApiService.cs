using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

// It's good practice to use a namespace that reflects your new project
namespace SellerDashboard
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        // The BaseUrl should point to your running API project
        private const string BaseUrl = "https://localhost:44395/";

        public ApiService()
        {
            // This handler is to bypass SSL certificate validation for local development
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Retrieves all distributor orders from the API.
        /// </summary>
        public async Task<List<DistributorOrderDto>> GetDistributorOrdersAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/DistributorOrders");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<DistributorOrderDto>>(json);
                }
                MessageBox.Show($"Error fetching orders: {response.StatusCode}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching orders: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new List<DistributorOrderDto>(); // Return empty list on error
        }

        /// <summary>
        /// Creates a new distributor order via the API.
        /// </summary>
        public async Task<DistributorOrderDto> CreateDistributorOrderAsync(CreateDistributorOrderDto order)
        {
            try
            {
                string json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/DistributorOrders", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DistributorOrderDto>(responseJson);
                }
                // Provide more detailed error info if available
                string errorContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error creating order: {response.StatusCode}\n{errorContent}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating the order: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null; // Return null on error
        }
    }
}
