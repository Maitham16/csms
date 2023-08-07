// App Controller Class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryService.Models;
using InventoryService.Services;
using InventoryService.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class AppController : ControllerBase
    {
        private readonly IAppRepository _appRepository;

        public AppController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        // Product methods
        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _appRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _appRepository.GetProduct(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                var result = await _appRepository.AddProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var result = await _appRepository.UpdateProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _appRepository.DeleteProduct(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Category methods
        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _appRepository.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _appRepository.GetCategory(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("category")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                var result = await _appRepository.AddCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("category/{id}")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            try
            {
                var result = await _appRepository.UpdateCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _appRepository.DeleteCategory(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Inventory methods
        [HttpGet]
        [Route("inventory")]
        public async Task<IActionResult> GetInventories()
        {
            try
            {
                var inventories = await _appRepository.GetInventories();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("inventory/{id}")]
        public async Task<IActionResult> GetInventory(int id)
        {
            try
            {
                var inventory = await _appRepository.GetInventory(id);
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("inventory")]
        public async Task<IActionResult> AddInventory(Inventory inventory)
        {
            try
            {
                var result = await _appRepository.AddInventory(inventory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("inventory/{id}")]
        public async Task<IActionResult> UpdateInventory(Inventory inventory)
        {
            try
            {
                var result = await _appRepository.UpdateInventory(inventory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Route("inventory/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var result = await _appRepository.DeleteInventory(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}