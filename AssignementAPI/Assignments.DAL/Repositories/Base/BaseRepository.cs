using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;

namespace Assignments.DAL.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected AssignmentContext Context;
        internal DbSet<T> DbSet;
        protected readonly ILogger Logger;

        public DbSet<T> Set { get => DbSet; }

        public BaseRepository(AssignmentContext context, ILogger logger)
        {
            Context = context;
            DbSet = Context.Set<T>();
            Logger = logger;
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).CountAsync();
        }

        public Task<int> CountAllAsync()
        {
            return DbSet.CountAsync();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return DbSet.Where(model => model.Id == id).FirstOrDefaultAsync();
        }

        public Task<T?> GetFirstByCriteria(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error when deleting entity : {Message}", ex.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                return await DeleteAsync(entity);
            }
            return false;
        }

        public async Task<bool> UpsertAsync(T entity)
        {
            if (AnyById(entity.Id))
            {
                return await UpdateAsync(entity);
            }
            else
            {
                await AddAsync(entity);
                return true;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                DbSet.Update(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error when updating entity : {Message}", ex.Message);
                return false;
            }
            return true;
        }

        public bool AnyById(int id)
        {
            return DbSet.Any(entity => entity.Id == id);
        }

        public IEnumerable<T> GetPagination(int pageNumber, int pageSize)
        {
            return DbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> GetPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Task<bool> AnyByCriteria(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        public IEnumerable<T> FilterByCriteria(Func<T, bool> predicate)
        {
            return DbSet.AsEnumerable().Where(predicate);
        }

        public T? GetById(int id)
        {
            return DbSet.Where(model => model.Id == id).FirstOrDefault();
        }
    }
}