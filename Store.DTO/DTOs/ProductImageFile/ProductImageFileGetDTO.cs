using Store.DTO.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DTO.DTOs.ProductImageFile
{
    public record ProductImageFileGetDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public ProductGetDTO Product { get; set; }
    }
}
