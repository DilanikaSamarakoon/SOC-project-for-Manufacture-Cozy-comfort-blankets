using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DistributorDashboard
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:44395/";

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<DistributorOrderDto>> GetDistributorOrders()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/DistributorOrders");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<DistributorOrderDto>>(json);
                }
                MessageBox.Show($"Error fetching orders: {response.StatusCode}", "API Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred fetching orders: {ex.Message}", "API Error");
            }
            return new List<DistributorOrderDto>();
        }

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
                string errorContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error creating order: {response.StatusCode}\n{errorContent}", "API Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred creating the order: {ex.Message}", "API Error");
            }
            return null;
        }

        // --- FIX APPLIED HERE ---
        public async Task<bool> FulfillOrderAsync(int orderId)
        {
            try
            {
                // For a PATCH request, it's common to send only the fields you want to change.
                // This JSON Patch format is a standard way to do this.
                var patchDoc = new[]
                {
                    new { op = "replace", path = "/status", value = "Fulfilled" }
                };

                var json = JsonConvert.SerializeObject(patchDoc);
                var content = new StringContent(json, Encoding.UTF8, "application/json-patch+json");

                // Manually create and send a PATCH request.
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/DistributorOrders/{orderId}")
                {
                    Content = content
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Failed to fulfill order. Status: {response.StatusCode}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred fulfilling the order: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
