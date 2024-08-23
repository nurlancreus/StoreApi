using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DTO.DTOs.ProductImageFile
{
    public record ProductImageFilePostDTO
    {
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
    }
}
