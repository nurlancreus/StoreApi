using Store.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Abstractions.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool DeleteSoft(T entity);
        bool DeleteBulk(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
