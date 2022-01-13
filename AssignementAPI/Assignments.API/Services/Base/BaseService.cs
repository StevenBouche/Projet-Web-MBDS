using Assignments.API.Models.Search;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using System.Linq.Expressions;

namespace Assignments.API.Services.Base
{
    public abstract class BaseService<T, K> : IBaseService<T>
        where T : BaseModel
        where K : IBaseRepository<T>
    {
        protected K Repository { get; }
        protected readonly ILogger Logger;

        public BaseService(K repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }

        public async Task<PaginationResult<T>> GetPaginationAsync(PaginationForm form)
        {
            PaginationResult<T> result = BuildPagination(form, await Repository.CountAllAsync());

            result.Results = Repository.GetPagination(result.Page, result.PageSize).ToList();
            result.TotalPage = result.Results.Count;

            return result;
        }

        public async Task<PaginationResult<T>> GetPaginationAsync(PaginationForm form, Expression<Func<T, bool>> predicate)
        {
            PaginationResult<T> result = BuildPagination(form, await Repository.CountAllAsync());

            result.Results = Repository.GetPagination(result.Page, result.PageSize, predicate).ToList();
            result.TotalPage = result.Results.Count;

            return result;
        }

        public PaginationResult<T> GetPaginationAsync(PaginationForm form, IEnumerable<T> results)
        {

            var pageEntity = results.Skip((form.Page - 1) * form.PageSize).Take(form.PageSize);

            PaginationResult<T> result = BuildPagination(form, results.Count());

            result.Results = pageEntity.ToList();
            result.TotalPage = result.Results.Count;

            return result;
        }

        public PaginationResult<TResult> MapPagination<TResult>(PaginationResult<T> pagination, Func<T, TResult> selector)
        {
            return new PaginationResult<TResult>()
            {
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                Total = pagination.Total,
                TotalPage = pagination.Total,
                Results = pagination.Results.Select(selector).ToList(),
            };
        }

        private PaginationResult<T> BuildPagination(PaginationForm form, int count)
        {
            return new PaginationResult<T>()
            {
                PageSize = form.PageSize < 1 ? 1 : form.PageSize,
                Page = form.Page < 1 ? 1 : form.Page,
                Total = count
            };
        }
    }
}
