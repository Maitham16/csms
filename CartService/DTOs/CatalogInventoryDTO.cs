// catalog inventory dto class
// by Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogService.DTOs
{
    public class CatalogInventoryDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? ProductId { get; set; }
        [Required]
        public int? Stock { get; set; }
    }
}