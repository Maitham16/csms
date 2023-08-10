// IShipping Repository Interface
// By Maitham Al-rubaye

using ShippingService.Models;
using System;
using System.Collections.Generic;

namespace ShippingService.Repositories
{
    public interface IShippingRepository
    {
        Task<IEnumerable<Shipping>> GetAllShippingsAsync();
        Task<Shipping> GetShippingByIdAsync(int id);
        Task InsertShippingAsync(Shipping shipping);
        Task DeleteShippingAsync(int id);
        Task UpdateShippingAsync(Shipping shipping);
        Task<Shipping.ShippingStatus> GetShippingStatusAsync(int id);
        Task UpdateShippingStatusAsync(int id, Shipping.ShippingStatus shippingStatus);
    }
}