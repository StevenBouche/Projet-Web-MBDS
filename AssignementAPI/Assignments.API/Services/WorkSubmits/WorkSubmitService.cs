using Assignments.API.Exceptions.Business;
using Assignments.API.Extentions.ModelExtentions;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Authorization;
using Assignments.API.Models.Search;
using Assignments.API.Models.WorkSubmits;
using Assignments.API.Services.Base;
using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.WorkSubmits;

namespace Assignments.API.Services.WorkSubmits
{
    public class WorkSubmitService : BaseService<WorkSubmitEntity, IWorkSubmitRepository>, IWorkSubmitService
    {

        private readonly UserIdentity Identity;

        public WorkSubmitService(IWorkSubmitRepository repository, UserIdentity identity, ILogger<WorkSubmitService> logger) : base(repository, logger)
        {
            Identity = identity;
        }

        #region GET

        public async Task<WorkSubmit> GetWorkSubmitByIdAsync(int id)
        {
            var entity = await VerifyAndGetEntity(id);

            return entity.ToWorkSubmit();
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form)
        {
            var pagination = Identity.Role switch
            {
                AuthorizationConstants.STUDENT => await GetPaginationAsync(form, entity => entity.UserId == Identity.Id),
                AuthorizationConstants.PROFESSOR => await GetPaginationAsync(form, entity =>
                        entity.Assignment != null &&
                        entity.Assignment.Course != null &&
                        entity.Assignment.Course.UserId == Identity.Id
                    ),
                _ => await GetPaginationAsync(form)
            };

            return MapPagination(pagination, entity => entity.ToWorkSubmit());
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllWorksAssignmentAsync(int id, PaginationForm form)
        {
            return await GetAllSubmitsOfAssignmentAsync(new PaginationIdForm() { Id = id, Page = form.Page, PageSize = form.PageSize });
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllSubmitsOfAssignmentAsync(PaginationIdForm form)
        {
            var pagination = await GetPaginationAsync(form, entity => entity.AssignmentId == form.Id);
            return MapPagination(pagination, entity => entity.ToWorkSubmit());
        }

        #endregion GET

        #region CREATE

        public async Task<WorkSubmit> CreateWorkSubmitAsync(WorkSubmitStudentForm form)
        {
            var entity = await Repository.AddAsync(new WorkSubmitEntity()
            {
                Label = form.Label,
                Description = form.Description,
                UserId = Identity.Id,
                AssignmentId = form.AssignmentId,
                State = WorkSubmitState.CREATED
            });
            return entity.ToWorkSubmit();
        }

        #endregion CREATE

        #region UPDATE

        public async Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitStudentForm form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            VerifyUpdate(entity);

            if (entity.AssignmentId != form.AssignmentId)
            {
                var any = await Repository.AnyByCriteria(entity => entity.UserId == Identity.Id && entity.AssignmentId == form.AssignmentId);
                if (!any)
                {
                    entity.AssignmentId = form.AssignmentId;
                }
                else throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UPDATE, "Only one work per user is authorized for an assignment");
            }

            entity.Label = form.Label;
            entity.Description = form.Description;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitProfessorForm form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            VerifyUpdate(entity);

            entity.Grade = form.Grade;
            entity.Comment = form.Comment;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> EvaluateWorkSubmitAsync(WorkSubmitActionForm form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            VerifyUpdate(entity);

            entity.State = WorkSubmitState.EVALUATED;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> SubmitWorkSubmitAsync(WorkSubmitActionForm form)
        {
            var entity = await GetEntityAndVerifyOwner(form.Id);

            VerifyUpdate(entity);

            if (entity.AssignmentId == null)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UPDATE, "Cannot submit a work which is not assigned to an assignments");

            entity.State = WorkSubmitState.SUBMITTED;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        #endregion UPDATE

        #region DELETE

        public Task DeleteWorkSubmitAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion DELETE

        #region VERIFICATION

        private async Task<WorkSubmitEntity> GetEntityAndVerifyOwner(int? id)
        {
            var entity = await VerifyAndGetEntity(id);
            VerifyOwner(entity);
            return entity;
        }

        private void VerifyOwner(WorkSubmitEntity entity)
        {
            if (Identity.Role == AuthorizationConstants.STUDENT) VerifyOwnerStudent(entity);
            else if (Identity.Role == AuthorizationConstants.PROFESSOR) VerifyOwnerProfessor(entity);
        }

        private void VerifyOwnerStudent(WorkSubmitEntity entity)
        {
            if (entity.UserId != Identity.Id)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE, "Not the owner of the resource");
        }

        private void VerifyOwnerProfessor(WorkSubmitEntity entity)
        {
            if (entity.Assignment == null || entity.Assignment.Course == null || entity.Assignment.Course.UserId != Identity.Id)
            {
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_UNAUTHORIZE, "Not the owner of the resource");
            }
        }

        private void VerifyUpdate(WorkSubmitEntity entity)
        {
            if (Identity.Role == AuthorizationConstants.STUDENT) VerifyUpdateStudent(entity);
            else if (Identity.Role == AuthorizationConstants.PROFESSOR) VerifyUpdateProfessor(entity);
        }

        private void VerifyUpdateStudent(WorkSubmitEntity entity)
        {
            if (entity.State == WorkSubmitState.SUBMITTED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UPDATE, "Cannot update work when is submitted");
            else if (entity.State == WorkSubmitState.EVALUATED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UPDATE, "Cannot update work when is evaluate");
        }

        private void VerifyUpdateProfessor(WorkSubmitEntity entity)
        {
            if (entity.State == WorkSubmitState.CREATED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_UPDATE, "Cannot update evaluation when work is not submitted");
            else if (entity.State == WorkSubmitState.EVALUATED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_UPDATE, "Cannot update evaluation when work is already evaluated");
        }

        #endregion VERIFICATION
    }
}
