// CatalogServiceClient class to get items from catalogservice
// By Maitham Al-rubaye

using System;
using System.Net.Http;
using System.Text.Json;
using CartService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartService.Services
{
    public class CatalogServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string CatalogServiceBaseUrl = "http://localhost:5002";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogProductDTO>> GetItemsAsync()
        {
            var response = await _httpClient.GetAsync($"{CatalogServiceBaseUrl}/Catalog/product");
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

        public async Task<CatalogProductDTO> GetItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{CatalogServiceBaseUrl}/Catalog/product/{id}");
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

        public async Task<int> GetItemStockFromCatalogAsync(int itemId)
        {
            var response = await _httpClient.GetAsync($"{CatalogServiceBaseUrl}/Catalog/inventory/stock/{itemId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<int>(content, _jsonOptions);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the product stock data.", ex);
            }
        }
    }
}
