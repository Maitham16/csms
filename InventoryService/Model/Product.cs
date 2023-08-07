// Product class for the InventoryService
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryService.Models;

namespace InventoryService.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string? Barcode { get; set; }

        public virtual Category? Category { get; set; }
    }
}