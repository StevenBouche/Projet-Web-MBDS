using Assignment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Assignment.DAL.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        DbSet<T> Set { get; }
        bool AnyById(int id);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
