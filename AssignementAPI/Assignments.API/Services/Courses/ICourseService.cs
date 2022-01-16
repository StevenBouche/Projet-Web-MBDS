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
        Task<Course> CreateCourseAsync(CourseForm form);
        Task<Course> UpdateCourseAsync(CourseForm form);
        Task DeleteCourseAsync(int id);
    }
}