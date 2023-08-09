// App Repository interface
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryService.Models;

namespace InventoryService.Repositories
{
    public interface IAppRepository 
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int id);

        Task<List<Inventory>> GetInventories();
        Task<Inventory> GetInventory(int id);
        Task<Inventory> AddInventory(Inventory inventory);
        Task<Inventory> UpdateInventory(Inventory inventory);
        Task<Inventory> DeleteInventory(int id);
        
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int id);  

        Task<int> GetStock(int id);
    }
}