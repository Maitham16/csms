// CatalogController class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Services;
using CatalogService.DTOs;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly InventoryServiceClient _inventoryServiceClient;

    public CatalogController(InventoryServiceClient inventoryServiceClient)
    {
        _inventoryServiceClient = inventoryServiceClient;
    }

    [HttpGet("product")]
    public async Task<ActionResult<IEnumerable<CatalogProductDTO>>> GetProducts()
    {
        try
        {
            return Ok(await _inventoryServiceClient.GetProductsAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("category")]
    public async Task<ActionResult<IEnumerable<CatalogCategoryDTO>>> GetCategories()
    {
        try
        {
            return Ok(await _inventoryServiceClient.GetCategoriesAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<CatalogProductDTO>> GetProduct(int id)
    {
        try
        {
            return Ok(await _inventoryServiceClient.GetProductAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("category/{id}")]
    public async Task<ActionResult<CatalogCategoryDTO>> GetCategory(int id)
    {
        try
        {
            return Ok(await _inventoryServiceClient.GetCategoryAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("product/search/{search}")]
    public async Task<ActionResult<IEnumerable<CatalogProductDTO>>> SearchProducts(string search)
    {
        try
        {
            return Ok(await _inventoryServiceClient.SearchProductsAsync(search));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("product/sort/{sort}")]
    public async Task<ActionResult<IEnumerable<CatalogProductDTO>>> SortProducts(string sort)
    {
        try
        {
            return Ok(await _inventoryServiceClient.SortProductsAsync(sort));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}