using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Courses;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.Search.Courses;
using Assignments.Business.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.Business.Services.Courses
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

        CoursesSearchResult SearchCourses(CoursesSearchForm form);
        IList<Assignment> GetAllAssignmentCourse(int id);
    }
}