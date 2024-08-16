using Store.DTO.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DTO.DTOs.Category
{
    public class CategoryGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductGetDTO> Products { get; set; } = [];
    }
}
