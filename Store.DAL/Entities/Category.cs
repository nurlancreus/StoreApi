using Store.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}
