// Catalog Product DTO class
// By Maitham Al-rubaye
// 2023

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogService.DTOs
{
    public class CatalogProductDTO
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

        public virtual CatalogCategoryDTO? Category { get; set; }
    }
}