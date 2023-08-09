// order controller class
// By Maihtham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Services;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return Ok(await _orderRepository.GetOrders());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderRepository.GetOrder(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        await _orderRepository.AddOrder(order);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
    {
        if (id != order.Id) return BadRequest();
        var orderToUpdate = await _orderRepository.GetOrder(id);
        if (orderToUpdate == null) return NotFound();
        return Ok(await _orderRepository.UpdateOrder(order));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Order>> DeleteOrder(int id)
    {
        var orderToDelete = await _orderRepository.GetOrder(id);
        if (orderToDelete == null) return NotFound();
        return Ok(await _orderRepository.DeleteOrder(id));
    }

    [HttpGet("order/{userId}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(string userId)
    {
        return Ok(await _orderRepository.GetOrdersByUserId(userId));
    }
}


