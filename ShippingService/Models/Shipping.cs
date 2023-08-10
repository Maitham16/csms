// Shipping model
// By Maitham Al-rubaye

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShippingService.Models
{
    public class Shipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? ShippingAddress { get; set; }
        public enum ShippingStatus
        {
            Pending,
            Shipped,
            Delivered
        }
        public ShippingStatus Status { get; set; }
        public DateTime ShippmentDeliveredDate { get; set; }
    }
}