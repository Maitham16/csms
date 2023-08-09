// App Service class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryService.Models;
using InventoryService.Repositories;
using InventoryService.Data;


namespace InventoryService.Services
{
    public class AppService : IAppRepository
    {
        private readonly InventoryDbContext _context;

        public AppService(InventoryDbContext context)
        {
            _context = context;
        }

        // Product methods
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            
            var result = await _context.Products.FindAsync(id);
            if (result == null)
            {
                throw new Exception("Product not found");
            }
            return result;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Inventory methods
        public async Task<List<Inventory>> GetInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetInventory(int id)
        {
            var result = await _context.Inventories.FindAsync(id);
            if (result == null)
            {
                throw new Exception("Inventory not found");
            }
            return result;
        }

        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> UpdateInventory(Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                throw new Exception("Inventory not found");
            }
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        // Category methods
        public async Task<List<Category>> GetCategories()
        {
            var result = await _context.Categories.ToListAsync();
            if (result == null)
            {
                throw new Exception("Category not found");
            }
            return result;
        }

        public async Task<Category> GetCategory(int id)
        {
            var result = await _context.Categories.FindAsync(id);
            if (result == null)
            {
                throw new Exception("Category not found");
            }
            return result;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // get int stock by product id from inventory
        public async Task<int> GetStock(int id)
        {
            var result = await _context.Inventories.FindAsync(id);
            if (result == null)
            {
                throw new Exception("Inventory not found");
            }
            return result.Stock;
        }
    }
}