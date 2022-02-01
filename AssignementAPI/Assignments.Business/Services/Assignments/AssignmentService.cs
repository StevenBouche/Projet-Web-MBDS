using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.Search.Assignments;
using Assignments.Business.Dto.WorkSubmits;
using Assignments.Business.Exceptions.Business;
using Assignments.Business.Extentions.ModelExtentions;
using Assignments.Business.Services.Base;
using Assignments.Business.Services.WorkSubmits;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Assignments;
using Microsoft.Extensions.Logging;

namespace Assignments.Business.Services.Assignments
{
    public class AssignmentService : BaseService<AssignmentEntity, IAssignmentRepository>, IAssignmentService
    {
        private readonly UserIdentity Identity;
        private readonly IWorkSubmitService WorkSubmitService;

        public AssignmentService(IAssignmentRepository repository, UserIdentity identity, IWorkSubmitService workSubmitService, ILogger<AssignmentService> logger) : base(repository, logger)
        {
            Identity = identity;
            WorkSubmitService = workSubmitService;
        }

        #region GET

        public async Task<Assignment> GetAssignmentByIdAsync(int? id)
        {
            var entity = await GetEntityAndVerifyOwner(id);
            return entity.ToAssignment();
        }

        public async Task<Assignment> GetAssignmentDetailsByIdAsync(int? id)
        {
            var entity = await VerifyAndGetEntity(id);
            return entity.ToAssignment();
        }

        public async Task<PaginationResult<Assignment>> GetAllAssignmentsAsync(PaginationForm form)
        {
            var pagination = await GetPaginationAsync(form);
            return MapPagination(pagination, entity => entity.ToAssignment());
        }

        public async Task<PaginationResult<Assignment>> GetMineAssignmentsAsync(PaginationForm form)
        {
            var pagination = Identity.Role switch
            {
                AuthorizationConstants.STUDENT => await GetPaginationAsync(form, entity => entity.WorkSubmits.Any(work => work.UserId == Identity.Id)),
                AuthorizationConstants.PROFESSOR => await GetPaginationAsync(form, entity => entity.Course != null && entity.Course.UserId == Identity.Id),
                _ => await GetPaginationAsync(form)
            };

            return MapPagination(pagination, entity => entity.ToAssignment());
        }

        public async Task<PaginationResult<Assignment>> GetAllAssignmentsOfCourseAsync(int course, PaginationForm form)
        {
            var pagination = await GetPaginationAsync(form, entity => entity.CourseId == course);

            return MapPagination(pagination, entity => entity.ToAssignment());
        }

        public IList<Assignment> GetAllAssignmentsOfCourse(int id)
        {
            return Repository.FilterByCriteria(entity => entity.CourseId == id).Select(entity => entity.ToAssignment()).ToList();
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllWorksAssignmentAsync(int id, PaginationForm form)
        {
            return await WorkSubmitService.GetAllWorksAssignmentAsync(id, form);
        }

        public AssignmentsSearchResult SearchAssignments(AssignmentsSearchForm form)
        {
            var result = Search(entity => entity.Label.Contains(form.Term));

            if (form.CourseId != null && form.CourseId > 0)
            {
                result = result.Where(entity => entity.CourseId == form.CourseId);
            }

            return new AssignmentsSearchResult()
            {
                Term = form.Term,
                CourseId = form.CourseId,
                Results = result.Select(entity => entity.ToAssignment()).OrderBy(entity => entity.Label).ToList()
            };
        }

        #endregion GET

        #region CREATE

        public async Task<Assignment> CreateAssignmentAsync(AssignmentForm form)
        {
            var entity = await Repository.AddAsync(new AssignmentEntity()
            {
                Label = form.Label,
                DelivryDate = form.DelivryDate,
                State = DAL.Enumerations.AssignmentState.OPEN,
                CourseId = form.CourseId
            });
            return entity.ToAssignment();
        }

        #endregion CREATE

        #region UPDATE

        public async Task<Assignment> OpenAssignmentAsync(int? id)
        {
            var entity = await GetEntityAndVerifyOwner(id);

            entity.State = DAL.Enumerations.AssignmentState.OPEN;

            await Repository.UpdateAsync(entity);

            return entity.ToAssignment();
        }

        public async Task<Assignment> CloseAssignmentAsync(int? id)
        {
            var entity = await GetEntityAndVerifyOwner(id);

            entity.State = DAL.Enumerations.AssignmentState.CLOSE;

            await Repository.UpdateAsync(entity);

            return entity.ToAssignment();
        }

        public async Task<Assignment> UpdateAssignmentAsync(AssignmentForm form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            VerifyUpdate(entity);

            entity.Label = form.Label;
            entity.DelivryDate = form.DelivryDate;

            await Repository.UpdateAsync(entity);

            return entity.ToAssignment();
        }

        #endregion UPDATE

        #region DELETE

        public Task DeleteAssignmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion DELETE

        #region VERIFICATION

        private async Task<AssignmentEntity> GetEntityAndVerifyOwner(int? id)
        {
            var entity = await VerifyAndGetEntity(id);
            VerifyOwner(entity);
            return entity;
        }

        private void VerifyUpdate(AssignmentEntity entity)
        {
            if (entity.State == DAL.Enumerations.AssignmentState.CLOSE)
                throw new AssignmentBusinessException(AssignmentBusinessExceptionTypes.ASSIGNMENT_UPDATE, "Cannot update a closed resource");
        }

        private void VerifyOwner(AssignmentEntity entity)
        {
            if (entity.Course == null || entity.Course.UserId != Identity.Id)
                throw new AssignmentBusinessException(AssignmentBusinessExceptionTypes.ASSIGNMENT_UNAUTHORIZE, "Not the owner of the resource");
        }

        #endregion VERIFICATION
    }
}