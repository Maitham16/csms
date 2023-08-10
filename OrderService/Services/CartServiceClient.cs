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

        public async Task<OrderCartDTO> GetUserCartAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"{CartServiceBaseUrl}/Cart/{userId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<OrderCartDTO>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the products data.", ex);
            }
        }

        public async Task<bool> EmptyUserCartAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"{CartServiceBaseUrl}/Cart/{userId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<OrderCartDTO> GetCartByIdAsync(string cartId)
        {
            var response = await _httpClient.GetAsync($"{CartServiceBaseUrl}/Cart/{cartId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<OrderCartDTO>(content, _jsonOptions)
                    ?? throw new Exception("Deserialized data is null.");
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing the cart data.", ex);
            }
        }

        public async Task<bool> EmptyCartAsync(string cartId)
        {
            var response = await _httpClient.DeleteAsync($"{CartServiceBaseUrl}/Cart/{cartId}");
            return response.IsSuccessStatusCode;
        }
    }
}