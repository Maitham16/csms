// OrderSevice class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Data;

namespace OrderService.Services
{
    public class OrderServices : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly CartServiceClient _cartServiceClient;

        public OrderServices(OrderDbContext context, CartServiceClient cartServiceClient)
        {
            _context = context;
            _cartServiceClient = cartServiceClient;
        }

        public async Task<IEnumerable<Order>> PlaceOrderFromCart(string cartId)
        {
            var cart = await _cartServiceClient.GetCartByIdAsync(cartId);

            if (cart == null || cart.Items == null || !cart.Items.Any())
            {
                throw new Exception("No items in the cart to order.");
            }

            var orders = new List<Order>();
            foreach (var cartItem in cart.Items)
            {
                var order = new Order
                {
                    UserId = cart.UserId, // assuming the cart DTO contains a UserId
                    ProductId = cartItem.ProductId,
                    UnitPrice = (double)cartItem.UnitPrice,
                    Quantity = cartItem.Quantity,
                    TotalPrice = (double)cartItem.TotalPrice,
                    OrderDate = DateTime.Now,
                    Status = Order.OrderStatus.Placed
                };
                _context.Orders.Add(order);
                orders.Add(order);
            }
            await _context.SaveChangesAsync();

            // Optionally, after placing the orders, empty the user's cart
            await _cartServiceClient.EmptyCartAsync(cartId);

            return orders;
        }

        public async Task<Order> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrder(int id)
        {
            var result = await _context.Orders.FindAsync(id);
            if (result == null)
            {
                throw new Exception("Order not found");
            }
            return result;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> ReturnOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != Order.OrderStatus.Delivered)
            {
                throw new Exception("Only delivered orders can be returned");
            }

            order.Status = Order.OrderStatus.Returned;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Potentially notify an inventory service to add back items, or process a refund here.

            return order;
        }

        public async Task<Order> ShipOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != Order.OrderStatus.Placed)
            {
                throw new Exception("Only placed orders can be shipped");
            }

            order.Status = Order.OrderStatus.Shipped;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Potentially notify a shipping service here.

            return order;
        }

        public async Task<Order> DeliverOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != Order.OrderStatus.Shipped)
            {
                throw new Exception("Only shipped orders can be delivered");
            }

            order.Status = Order.OrderStatus.Delivered;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Potentially notify a shipping service here.

            return order;
        }

        public async Task<Order> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Status != Order.OrderStatus.Placed)
            {
                throw new Exception("Only placed orders can be cancelled");
            }

            order.Status = Order.OrderStatus.Cancelled;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Potentially notify an inventory service to add back items, or process a refund here.

            return order;
        }
    }
}
