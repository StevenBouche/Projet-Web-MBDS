using Assignments.API.Models.Assignments;
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
        Task<PaginationResult<Course>> GetMineCoursesAsync(PaginationForm form);
        Task<Course> CreateCourseAsync(CourseFormCreate form);
        Task<Course> UpdateCourseAsync(CourseFormUpdate form);
        Task DeleteCourseAsync(int id);
        Task<PaginationResult<Assignment>> GetAllAssignmentCourseAsync(int id, PaginationForm form);
        Task AddPictureId(int courseId, int id);
    }
}