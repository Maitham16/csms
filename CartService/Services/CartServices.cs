// CartServices class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Data;
using CartService.Models;
using CartService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CartService.Services
{
    public class CartServices : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartServices(CartDbContext context)
        {
            _context = context;
        }

        // CRUD Operations for Cart
        public async Task<Cart> GetCartAsync(int cartId)
        {
            var result = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);
            if (result == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }
            return result;
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCartAsync(int cartId)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                return false;
            }

            _context.Carts.Remove(cart);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        // return all carts
        public async Task<IEnumerable<Cart>> GetCartsAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        // Operations for Cart Items
        public async Task<IEnumerable<CartItem>> GetItemsInCartAsync(int cartId)
        {
            var cart = await GetCartAsync(cartId);
            if (cart.Items == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }
            return cart.Items;
        }

        public async Task<bool> AddItemToCartAsync(int cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                return false;
            }
            
            if (cart.Items == null)
            {
                cart.Items = new List<CartItem>();
            }

            cart.Items.Add(item);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> RemoveItemFromCartAsync(int cartId, int itemId)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                return false;
            }

            var item = await GetCartItemByIdAsync(itemId);
            if (item == null)
            {
                return false;
            }

            if (cart.Items == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }

            cart.Items.Remove(item);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UpdateItemInCartAsync(int cartId, CartItem item)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                return false;
            }

            var existingItem = await GetCartItemByIdAsync(item.Id);
            if (existingItem == null)
            {
                return false;
            }

            existingItem.Quantity = item.Quantity;
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<CartItem> GetCartItemByIdAsync(int itemId)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(i => i.Id == itemId);
            if (result == null)
            {
                throw new Exception($"Item with id {itemId} not found.");
            }
            return result; 
        }

        public async Task<decimal> CalculateCartTotalAsync(int cartId)
        {
            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }
            
            if (cart.Items == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }

            var total = cart.Items.Sum(i => i.Quantity * i.UnitPrice);
            return total;
        }

        public async Task<bool> CheckItemAvailabilityAsync(int itemId)
        {
            var item = await GetCartItemByIdAsync(itemId);
            if (item == null)
            {
                return false;
            }

            return item.Quantity > 0;
        }
    }
}