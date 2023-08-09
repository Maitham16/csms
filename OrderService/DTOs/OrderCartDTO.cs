// Order Cart DTO
// By Maitham Al-rubaye

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.DTOs
{
    public class OrderCartDTO
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItemDTO>? Items { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}