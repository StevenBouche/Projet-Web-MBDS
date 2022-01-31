using Assignments.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Assignments.DAL.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        DbSet<T> Set { get; }

        bool AnyById(int id);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAllAsync();

        Task<T?> GetByIdAsync(int id);

        T? GetById(int id);

        Task<T?> GetFirstByCriteria(Expression<Func<T, bool>> predicate);

        Task<bool> AnyByCriteria(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetPagination(int pageNumber, int pageSize);

        IEnumerable<T> GetPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);

        Task<bool> DeleteByIdAsync(int id);

        Task<bool> DeleteAsync(T entity);

        Task<bool> UpsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        IEnumerable<T> FilterByCriteria(Func<T, bool> predicate);
    }
}