using Assignments.API.Models.Authentification;
using Assignments.API.Models.Courses;
using Assignments.API.Models.Search;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Courses;
using Assignments.API.Extentions.ModelExtentions;
using Assignments.API.Exceptions.Entities;
using Assignments.DAL.Enumerations;

namespace Assignments.API.Services.Courses
{
    public class CourseService : BaseService<CourseEntity, ICourseRepository>, ICourseService
    {
        public CourseService(ICourseRepository repository, ILogger<CourseService> logger) : base(repository, logger)
        {
        }

        public async Task<Course> CreateCourseAsync(CourseForm form, UserIdentity identity)
        {
            var entity = await Repository.AddAsync(new CourseEntity() { Name = form.Name, Description = form.Description, UserId = identity.Id });
            return entity.ToCourse();
        }

        public Task DeleteCourseAsync(int id, UserIdentity identity)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResult<Course>> GetAllCoursesAsync(PaginationForm form)
        {
            var pagination = await GetPaginationAsync(form);
            return MapPagination(pagination, entity => entity.ToCourse()); 
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var result = await Repository.GetByIdAsync(id);

            if (result == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            return result.ToCourse();
        }

        public async Task<PaginationResult<Course>> GetMyCoursesAsync(PaginationForm form, UserIdentity identity)
        {
            PaginationResult<CourseEntity> pagination;

            if (identity.Role == UserRoles.STUDENT.ToString())
            {
                var courses = Repository.GetStudentCoursesAsync(identity.Id);
                pagination = GetPaginationAsync(form, courses);
            }
            else
            {
                pagination = await GetPaginationAsync(form, entity => entity.UserId == identity.Id);
            }

            return MapPagination(pagination, entity => entity.ToCourse());
        }

        public async Task<Course> UpdateCourseAsync(CourseForm form, UserIdentity identity)
        {
            if (form.Id is null)
                throw new ArgumentException("id column missing"); //TODO change

            CourseEntity? entity = await Repository.GetByIdAsync(form.Id ?? 0);

            if (entity == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            if(entity.UserId != identity.Id)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND); //TODO change

            entity.Name = form.Name;
            entity.Description = form.Description;

            await Repository.UpdateAsync(entity);

            return entity.ToCourse();
        }
    }
}
