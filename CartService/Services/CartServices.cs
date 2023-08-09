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
using CartService.DTOs;

namespace CartService.Services
{
    public class CartServices : ICartRepository
    {
        private readonly CartDbContext _context;
        private readonly CatalogServiceClient _catalogServiceClient;

        public CartServices(CartDbContext context, CatalogServiceClient catalogServiceClient)
        {
            _context = context;
            _catalogServiceClient = catalogServiceClient;
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
            // Check if the product is available in the inventory
            var stock = await _catalogServiceClient.GetItemStockFromCatalogAsync(item.ProductId);
            if (stock < item.Quantity)
            {
                throw new Exception("Insufficient stock available for the product.");
            }

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
            // Check if the product is available in the inventory
            var stock = await _catalogServiceClient.GetItemStockFromCatalogAsync(item.ProductId);
            var existingItem = await GetCartItemByIdAsync(item.Id);

            // Find the difference in quantity between what's already in the cart and what the user wants to add/update
            var additionalQuantityRequired = item.Quantity - (existingItem?.Quantity ?? 0);
            if (stock < additionalQuantityRequired)
            {
                throw new Exception("Insufficient stock available for the updated quantity.");
            }

            var cart = await GetCartAsync(cartId);
            if (cart == null)
            {
                return false;
            }

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

            var total = cart.Items.Sum(i => i.Quantity * i.UnitPrice) - cart.DiscountAmount;
            return total > 0 ? total : 0;
        }

        public async Task<bool> IsProductAvailableInInventoryAsync(int productId)
        {
            var stock = await _catalogServiceClient.GetItemStockFromCatalogAsync(productId);
            return stock > 0;  // true if stock is greater than 0, otherwise false.
        }

        public bool IsCartExpired(Cart cart)
        {
            return DateTime.UtcNow > cart.ExpiryDateTime;
        }

        public async Task HandleExpiredCartsAsync()
        {
            var expiredCarts = _context.Carts.Where(c => DateTime.UtcNow > c.ExpiryDateTime);
            _context.Carts.RemoveRange(expiredCarts);
            await _context.SaveChangesAsync();
        }

        public async Task ApplyDiscountToCartAsync(int cartId, string discountCode)
        {
            // Fetch the discount from the database
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Code == discountCode && !d.IsUsed && DateTime.UtcNow <= d.ExpiryDateTime);
            if (discount == null)
            {
                throw new Exception("Invalid discount code or the discount has expired or been used.");
            }

            var cart = await GetCartAsync(cartId);
            var totalBeforeDiscount = await CalculateCartTotalAsync(cartId);

            // Apply the discount
            cart.DiscountAmount = totalBeforeDiscount * (discount.Percentage / 100);
            discount.IsUsed = true; // Mark the discount as used

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckoutAsync(int cartId)
        {
            var cart = await GetCartAsync(cartId);

            if (cart == null || IsCartExpired(cart))
            {
                throw new Exception($"Cart with id {cartId} not found or has expired.");
            }

            // Iterate over items in the cart to ensure sufficient stock for checkout
            if (cart.Items == null)
            {
                throw new Exception($"Cart with id {cartId} not found.");
            }
            
            foreach (var item in cart.Items)
            {
                var stock = await _catalogServiceClient.GetItemStockFromCatalogAsync(item.ProductId);
                if (stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product id {item.ProductId}.");
                }
            }

            // Logic to handle payment, order creation, etc. can be placed here

            // After processing the order, remove the cart
            return await DeleteCartAsync(cartId);
        }
    }
}