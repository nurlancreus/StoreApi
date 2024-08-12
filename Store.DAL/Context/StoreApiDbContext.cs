using Microsoft.EntityFrameworkCore;
using Store.DAL.Entities;
using Store.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
    public class StoreApiDbContext(DbContextOptions<StoreApiDbContext> contextOptions) : DbContext(contextOptions)
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = ChangeTracker
        .Entries<BaseEntity>()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entity in changedEntities)
            {
                var now = DateTime.UtcNow + TimeSpan.FromHours(4);

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Category> Categories { get; set; }
    }
}
