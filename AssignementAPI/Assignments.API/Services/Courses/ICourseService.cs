using Assignments.API.Models.Authentification;
using Assignments.API.Models.Courses;
using Assignments.API.Models.Search;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.Courses
{
    public interface ICourseService : IBaseService<CourseEntity>
    {
        Task<Course> GetCourseByIdAsync(int id);
        Task<PaginationResult<Course>> GetAllCoursesAsync(PaginationForm form);
        Task<PaginationResult<Course>> GetMyCoursesAsync(PaginationForm form, UserIdentity identity);
        Task<Course> CreateCourseAsync(CourseForm form, UserIdentity identity);
        Task<Course> UpdateCourseAsync(CourseForm form, UserIdentity identity);
        Task DeleteCourseAsync(int id, UserIdentity identity);
    }
}
