using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Entities.Base;
using Store.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly StoreApiDbContext _context;

        public Repository(StoreApiDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
    }
}
