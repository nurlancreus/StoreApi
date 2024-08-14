using Store.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Abstractions.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool isTracking = true);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T?> GetByIdAsync(string id, bool isTracking = true);
        Task<T?> GetWhereAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
    }
}
