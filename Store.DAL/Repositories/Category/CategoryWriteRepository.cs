using Store.Core.Abstractions.Repositories;
using Store.Core.Entities;
using Store.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(StoreApiDbContext context) : base(context)
        {
        }
    }
}
