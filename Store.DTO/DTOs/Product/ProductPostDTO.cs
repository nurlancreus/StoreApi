using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Store.DTO.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DTO.DTOs.Product
{
    public record ProductPostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public ICollection<CategoryPostDTO> Categories { get; set; } = [];

        public IFormFileCollection? FormFiles { get; set; }
}
}
