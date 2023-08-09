// Inventory model for eShop
// By Maitham Al-rubaye
// 2023

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryService.Models;

namespace InventoryService.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int? ProductId { get; set; }

        [Required]
        public int Stock { get; set; }

        public virtual Product? Product { get; set; }
    }
}