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
    public class WriteRepository<T> : Repository<T>, IWriteRepository<T> where T : BaseEntity
    {
        public WriteRepository(StoreApiDbContext context) : base(context)
        {
        }

        public async Task<bool> AddAsync(T entity)
        {
            var entityEntry = await Table.AddAsync(entity);

            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddBulkAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);

            // Check if all entities are in the Added state
            var allAdded = entities.All(entity =>
            {
                var entry = _context.Entry(entity);
                return entry.State == EntityState.Added;
            });

            return allAdded;
        }

        public bool Delete(T entity)
        {
            var entityEntry = Table.Remove(entity);

            return entityEntry.State == EntityState.Deleted;
        }

        public bool DeleteBulk(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);

            var allDeleted = entities.All(entity =>
            {
                var entry = _context.Entry(entity);
                return entry.State == EntityState.Deleted;
            });

            return allDeleted;
        }

        public bool DeleteSoft(T entity)
        {
            entity.IsDeleted = true;

            return _context.Entry(entity).State == EntityState.Modified;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync());
        }

        public bool Update(T entity)
        {
            var entityEntry = Table.Update(entity);

            return entityEntry.State == EntityState.Modified;
        }
    }
}
