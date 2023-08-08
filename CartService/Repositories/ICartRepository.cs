// ICartRepository interface for CartRepository class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartService.Data;
using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Repositories
{
    public interface ICartRepository
    {
        // CRUD Operations for Cart
        Task<Cart> GetCartAsync(int cartId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int cartId);
        // return all carts
        Task<IEnumerable<Cart>> GetCartsAsync();

        // Operations for Cart Items
        Task<IEnumerable<CartItem>> GetItemsInCartAsync(int cartId);
        Task<bool> AddItemToCartAsync(int cartId, CartItem item);
        Task<bool> RemoveItemFromCartAsync(int cartId, int itemId); 
        Task<bool> UpdateItemInCartAsync(int cartId, CartItem item);
        Task<CartItem> GetCartItemByIdAsync(int itemId);
        Task<decimal> CalculateCartTotalAsync(int cartId);
        Task<bool> CheckItemAvailabilityAsync(int itemId);
    }
}