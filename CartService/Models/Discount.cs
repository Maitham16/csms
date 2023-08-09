// Disscount model
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public bool IsUsed { get; set; }
    }
}