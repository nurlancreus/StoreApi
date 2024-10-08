﻿using Store.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock {  get; set; }
        public ICollection<Category> Categories { get; set; } = [];
        public ICollection<ProductImageFile> ProductImageFiles { get; set; } = [];
    }
}
