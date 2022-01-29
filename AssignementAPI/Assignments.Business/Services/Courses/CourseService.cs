using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Courses;
using Assignments.Business.Services.Assignments;
using Assignments.Business.Services.Base;
using Assignments.Business.Extentions.ModelExtentions;
using Assignments.Business.Exceptions.Business;
using Assignments.Business.Dto.Authentification;
using Microsoft.Extensions.Logging;
using Assignments.Business.Dto.Courses;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Search.Courses;

namespace Assignments.Business.Services.Courses
{
    public class CourseService : BaseService<CourseEntity, ICourseRepository>, ICourseService
    {
        private readonly UserIdentity Identity;
        private readonly IAssignmentService AssignmentService;

        public CourseService(ICourseRepository repository, UserIdentity identity, IAssignmentService assignmentService, ILogger<CourseService> logger) : base(repository, logger)
        {
            Identity = identity;
            AssignmentService = assignmentService;
        }

        public async Task AddPictureId(int courseId, int id)
        {
            var entity = await VerifyAndGetEntity(courseId);

            entity.ImageId = id;

            await Repository.UpdateAsync(entity);
        }

        public async Task<Course> CreateCourseAsync(CourseFormCreate form)
        {
            var entity = await Repository.AddAsync(new CourseEntity()
            {
                Name = form.Name,
                Description = form.Description,
                UserId = Identity.Id
            });
            return entity.ToCourse();
        }

        public Task DeleteCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResult<Assignment>> GetAllAssignmentCourseAsync(int id, PaginationForm form)
        {
            return await AssignmentService.GetAllAssignmentsOfCourseAsync(id, form);
        }

        public async Task<PaginationResult<Course>> GetAllCoursesAsync(PaginationForm form)
        {
            var pagination = await GetPaginationAsync(form);
            return MapPagination(pagination, entity => entity.ToCourse());
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var result = await VerifyAndGetEntity(id);

            return result.ToCourse();
        }

        public async Task<PaginationResult<Course>> GetMineCoursesAsync(PaginationForm form)
        {
            var pagination = Identity.Role switch
            {
                AuthorizationConstants.STUDENT => GetPaginationAsync(form, Repository.GetStudentCoursesAsync(Identity.Id)),
                AuthorizationConstants.PROFESSOR => await GetPaginationAsync(form, entity => entity.UserId == Identity.Id),
                _ => await GetPaginationAsync(form)
            };
            return MapPagination(pagination, entity => entity.ToCourse());
        }

        public CoursesSearchResult SearchCourses(CoursesSearchForm form)
        {
            return new CoursesSearchResult()
            {
                Term = form.Term,
                Results = Search(entity => entity.Name.Contains(form.Term)).Select(entity => entity.ToCourse()).OrderBy(entity => entity.Name).ToList()
            };
        }

        public async Task<Course> UpdateCourseAsync(CourseFormUpdate form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            entity.Name = form.Name;
            entity.Description = form.Description;

            await Repository.UpdateAsync(entity);

            return entity.ToCourse();
        }

        private async Task<CourseEntity> GetEntityAndVerifyOwner(int? id)
        {
            var entity = await VerifyAndGetEntity(id);
            VerifyOwner(entity);
            return entity;
        }

        private void VerifyOwner(CourseEntity entity)
        {
            if (entity.UserId != Identity.Id)
                throw new CourseBusinessException(CourseBusinessExceptionTypes.COURSE_UNAUTHORIZE);
        }
    }
}