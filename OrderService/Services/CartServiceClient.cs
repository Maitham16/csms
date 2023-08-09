// CartServiceClient class
// By Maitham Al-rubaye

using System;
using System.Net.Http;
using System.Text.Json;
using OrderService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class CartServiceClient
    {
        private readonly HttpClient _httpClient;
        private const string CartServiceBaseUrl = "http://cart:80";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CartServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderCartDTO>> GetItemsAsync()
        {
            var response = await _httpClient.GetAsync($"{CartServiceBaseUrl}/Cart");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<IEnumerable<OrderCartDTO>>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the products data.", ex);
            }
        }
    }
}
