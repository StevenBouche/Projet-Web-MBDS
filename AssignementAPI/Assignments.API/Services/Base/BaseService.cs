using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;

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
    }
}
