// Order Model
// By Maitham Al-rubaye

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}