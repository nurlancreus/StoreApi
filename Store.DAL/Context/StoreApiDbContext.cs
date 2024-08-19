using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Context
{
    public class StoreApiDbContext(DbContextOptions<StoreApiDbContext> contextOptions) : DbContext(contextOptions)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppFile> AppFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedEntities = ChangeTracker
        .Entries<BaseEntity>()
        .Where(e => e.State != EntityState.Unchanged);

            foreach (var entity in changedEntities)
            {
                var now = DateTime.UtcNow + TimeSpan.FromHours(4);

                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = now;
                }
                else if (entity.State == EntityState.Modified && entity.Entity.IsDeleted == true)
                {
                    entity.Entity.DeletedAt = now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = now;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<AppFile>()
            .Property(p => p.Storage)
            .HasConversion<string>();

            // Apply global query filter for soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get the entity type's CLR type
                var clrType = entityType.ClrType;

                // Check if it's a root entity (not derived) and implements BaseEntity
                if (typeof(BaseEntity).IsAssignableFrom(clrType) &&
                    entityType.BaseType == null &&
                    clrType.GetProperty("IsDeleted") != null)
                {
                    // Apply the IsDeleted filter
                    modelBuilder.Entity(clrType).HasQueryFilter(GetIsDeletedFilter(clrType));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        private static LambdaExpression GetIsDeletedFilter(Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var prop = Expression.Property(param, "IsDeleted");
            var condition = Expression.Equal(prop, Expression.Constant(false));

            return Expression.Lambda(condition, param);
        }

    }
}
