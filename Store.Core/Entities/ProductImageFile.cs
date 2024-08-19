using Store.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class ProductImageFile : AppFile
    {
        public bool IsMain { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
