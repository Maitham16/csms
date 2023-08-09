// IOrderRepository interface for OrderRepository
// By Maihtham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> DeleteOrder(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    }
}