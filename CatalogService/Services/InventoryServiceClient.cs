// InventoryServiceClient class for InventoryService
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using CatalogService.DTOs;

namespace CatalogService.Services
{
    public class InventoryServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string InventoryServiceBaseUrl = "http://localhost:5001";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public InventoryServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogProductDTO>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{InventoryServiceBaseUrl}/app/product");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<IEnumerable<CatalogProductDTO>>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the products data.", ex);
            }
        }

        public async Task<IEnumerable<CatalogCategoryDTO>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"{InventoryServiceBaseUrl}/app/category");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<IEnumerable<CatalogCategoryDTO>>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the categories data.", ex);
            }
        }

        public async Task<CatalogProductDTO> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{InventoryServiceBaseUrl}/app/product/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<CatalogProductDTO>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the product data.", ex);
            }
        }

        public async Task<CatalogCategoryDTO> GetCategoryAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{InventoryServiceBaseUrl}/app/category/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<CatalogCategoryDTO>(content, _jsonOptions);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the category data.", ex);
            }
        }

        // search products by name or description or category name
        public async Task<IEnumerable<CatalogProductDTO>> SearchProductsAsync(string searchTerm)
        {
            // Fetch all products from Inventory Service
            var products = await GetProductsAsync();

            // Filter based on the search term. This can be enhanced for better search capabilities.
            return products.Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (p.Category != null && p.Category.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            );
        }

        public async Task<IEnumerable<CatalogCategoryDTO>> SearchCategoriesAsync(string searchTerm)
        {
            // fetch all categories from Inventory Service
            var categories = await GetCategoriesAsync();

            // Filter based on the search term. This can be enhanced for better search capabilities.
            return categories.Where(c =>
                c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        public async Task<IEnumerable<CatalogProductDTO>> SortProductsAsync(string sortCriteria)
        {
            // Fetch all products from Inventory Service
            var products = await GetProductsAsync();

            // Sort based on the criteria. This can be enhanced for more sort options.
            if (sortCriteria == "name")
            {
                return products.OrderBy(p => p.Name);
            }
            else if (sortCriteria == "description")
            {
                return products.OrderBy(p => p.Description);
            }
            else if (sortCriteria == "price")
            {
                return products.OrderBy(p => p.Price);
            }
            else
            {
                return products; // Default or handle other sort criteria
            }
        }
    }
}