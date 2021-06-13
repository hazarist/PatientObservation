using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Amatis.PatientObservation.DataAccess.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> DeleteAsync(long id);
        Task<int> CountAsync();
    }
}
