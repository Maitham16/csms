// CatalogProductDTO class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CartService.DTOs
{
    public class CatalogProductDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal? Price { get; set; }
       
        [Required]
        public string? Barcode { get; set; }
    }
}