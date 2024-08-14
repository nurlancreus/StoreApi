using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Entities.Base;
using Store.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class ReadRepository<T> : Repository<T>, IReadRepository<T> where T : BaseEntity
    {
        public ReadRepository(StoreApiDbContext context) : base(context)
        {
        }

        public IQueryable<T> GetAll(bool isTracking = true)
        {
            var query = Table.AsQueryable();

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.Where(expression);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<T?> GetByIdAsync(string id, bool isTracking = true)
        {
            var query = Table.AsQueryable();

            if (!isTracking)
                query = query.AsNoTracking();

            T? entity = await query.FirstOrDefaultAsync(e => e.Id == Guid.Parse(id));

            return entity;
        }

        public async Task<T?> GetWhereAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.AsQueryable();

            if (!isTracking)
                query = query.AsNoTracking();

            T? entity = await query.FirstOrDefaultAsync(expression);

            return entity;
        }
    }
}
