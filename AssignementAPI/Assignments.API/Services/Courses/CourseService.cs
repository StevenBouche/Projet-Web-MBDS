using Assignments.API.Models.Authentification;
using Assignments.API.Models.Courses;
using Assignments.API.Models.Search;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Courses;

namespace Assignments.API.Services.Courses
{
    public class CourseService : BaseService<CourseEntity, ICourseRepository>, ICourseService
    {
        public CourseService(ICourseRepository repository, ILogger<CourseService> logger) : base(repository, logger)
        {
        }

        public async Task<Course> CreateCourseAsync(CourseForm form, UserIdentity? identity)
        {

            return null;
            //return await Repository.AddAsync(new CourseEntity() { Name = form.Name, Description = form.Description });
        }

        public Task DeleteCourseAsync(int id, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<Course>> GetAllCoursesAsync(PaginationForm form)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetCourseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<Course>> GetMyCoursesAsync(PaginationForm form, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }

        public Task<Course> UpdateCourseAsync(CourseForm form, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }
    }
}
