// Shipping Services class
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using ShippingService.Models;
using ShippingService.Data;
using ShippingService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ShippingService.Services
{
    public class ShippingServices : IShippingRepository
    {
        private readonly ShippingDbContext _context;

        public ShippingServices(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipping>> GetAllShippingsAsync()
        {
            var results = await _context.Shippings.ToListAsync();
            if (results == null)
            {
                throw new Exception("No shippings found");
            }
            return results;
        }

        public async Task<Shipping> GetShippingByIdAsync(int id)
        {
            var results = await _context.Shippings.FindAsync(id);
            if (results == null)
            {
                throw new Exception("No shipping found");
            }
            return results;
        }

        public async Task InsertShippingAsync(Shipping shipping)
        {
            await _context.Shippings.AddAsync(shipping);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShippingAsync(int id)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                throw new Exception("No shipping found");
            }
            _context.Shippings.Remove(shipping);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShippingAsync(Shipping shipping)
        {
            _context.Entry(shipping).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Shipping.ShippingStatus> GetShippingStatusAsync(int id)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                throw new Exception("No shipping found");
            }
            return shipping.Status;
        }

        public async Task UpdateShippingStatusAsync(int id, Shipping.ShippingStatus shippingStatus)
        {
            var shipping = await _context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                throw new Exception("No shipping found");
            }
            shipping.Status = shippingStatus;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shipping>> GetShippingsByStatusAsync(Shipping.ShippingStatus shippingStatus)
        {
            var results = await _context.Shippings.Where(s => s.Status == shippingStatus).ToListAsync();
            if (results == null)
            {
                throw new Exception("No shippings found");
            }
            return results;
        }
    }
}