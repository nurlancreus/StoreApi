using Store.DTO.DTOs.Category;
using Store.DTO.DTOs.ProductImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DTO.DTOs.Product
{
    public class ProductGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public ICollection<CategoryGetDTO> Categories { get; set; } = [];
        public ICollection<ProductImageFileGetDTO> ProductImageFiles { get; set; } = [];
    }
}
