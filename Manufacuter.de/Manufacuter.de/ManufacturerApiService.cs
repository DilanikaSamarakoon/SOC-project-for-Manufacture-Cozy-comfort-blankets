// =======================================================================================
// !! FINAL, COMPLETE SERVICE CLASS !!
// This file contains all the methods and data objects needed by all your forms.
// Please replace the code in your 'ManufacturerApiService.cs' file and then Rebuild.
// This will fix the errors in your 'Production_Orders' form.
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; // Make sure you have the System.Net.Http.Json NuGet package
using System.Text;
using System.Threading.Tasks;

// Make sure this namespace matches the one used in your forms
namespace Manufacuter.de
{
    #region Data Transfer Objects (DTOs) for the entire application

    // For the Blanket Models form
    public class BlanketModelDto
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateBlanketModelDto
    {
        public string ModelName { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
    }

    // For the Production Orders form
    public class ProductionOrder
    {
        public int OrderId { get; set; }
        public int BlanketId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class CreateProductionOrderDto
    {
        public int BlanketId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int InventoryCount { get; set; }
    }

    public class CreateProduct
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int InventoryCount { get; set; }
    }

    // Note: DTOs for Stock Management can be added here as well if needed

    #endregion

    public class ManufacturerApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public ManufacturerApiService()
        {
            // Set the base address once
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7212"); // Your Manufacturer API URL
            }
        }

        // --- Methods for Blanket Models ---

        public async Task<List<BlanketModelDto>> GetBlanketModelsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BlanketModelDto>>("/api/Blankets");
            }
            catch { return new List<BlanketModelDto>(); }
        }

        public async Task<BlanketModelDto> AddBlanketModelAsync(CreateBlanketModelDto newModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Blankets", newModel);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<BlanketModelDto>() : null;
        }

        public async Task<bool> UpdateBlanketModelAsync(int modelId, BlanketModelDto updatedModel)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Blankets/{modelId}", updatedModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBlanketModelAsync(int modelId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Blankets/{modelId}");
            return response.IsSuccessStatusCode;
        }

        // --- Methods for Production Orders (FIX for your errors) ---

        public async Task<List<ProductionOrder>> GetOrdersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ProductionOrder>>("/api/Orders");
            }
            catch { return new List<ProductionOrder>(); }
        }

        public async Task<bool> CreateOrderAsync(CreateProductionOrderDto newOrder)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Orders", newOrder);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Product>>("/api/Products");
            }
            catch { return new List<Product>(); }
        }

        public async Task<Product> AddProductAsync(CreateProduct newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Products", newProduct);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Product>() : null;
        }
    }
}
