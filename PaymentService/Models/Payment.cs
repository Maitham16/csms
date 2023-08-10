// Payment class
// By Maitham Al-rubaye

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public enum PaymentStatuses
        {
            Pending,
            Paid,
            Failed
        }

        [Required]
        public PaymentStatuses PaymentStatus { get; set; }


        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}