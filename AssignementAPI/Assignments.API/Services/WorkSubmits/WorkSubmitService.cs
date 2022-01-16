using Assignments.API.Exceptions.Business;
using Assignments.API.Exceptions.Entities;
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
            var entity = await VerifyAndGetEntity(id, Identity);

            return entity.ToWorkSubmit();
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form)
        {
            PaginationResult<WorkSubmitEntity> pagination;

            if (Identity.Role == AuthorizationConstants.STUDENT)
            {
                pagination = await GetPaginationAsync(form, entity => entity.UserId == Identity.Id);
            }
            else if (Identity.Role == AuthorizationConstants.PROFESSOR)
            {
                pagination = await GetPaginationAsync(form, entity => 
                        entity.Assignment != null && 
                        entity.Assignment.Course.UserId == Identity.Id
                    );
            }
            else
            {
                pagination = await GetPaginationAsync(form);
            }

            return MapPagination(pagination, entity => entity.ToWorkSubmit());
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
            var entity = await VerifyUpdateAngGetEntityStudent(form.Id, Identity);

            entity.Label = form.Label;
            entity.Description = form.Description;
            entity.AssignmentId = form.AssignmentId;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitProfessorForm form)
        {
            var entity = await VerifyUpdateAngGetEntityProfessor(form.Id, Identity);

            entity.Grade = form.Grade;
            entity.Comment = form.Comment;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> EvaluateWorkSubmitAsync(WorkSubmitActionForm form)
        {
            var entity = await VerifyUpdateAngGetEntityProfessor(form.Id, Identity);

            entity.State = WorkSubmitState.EVALUATED;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> SubmitWorkSubmitAsync(WorkSubmitActionForm form)
        {
            var entity = await VerifyUpdateAngGetEntityStudent(form.Id, Identity);

            if (entity.AssignmentId == null)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_IS_NOT_LINK_ASSIGNMENT);

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

        private async Task<WorkSubmitEntity> VerifyAndGetEntity(int? id, UserIdentity identity)
        {
            if (id is null)
                throw new ArgumentException("id column missing"); //TODO change

            var entity = await Repository.GetByIdAsync((int)id);

            if (entity == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            if (identity.Role == AuthorizationConstants.STUDENT)
            {
                VerifyAuthorizationStudent(entity, identity);
            }
            else if (identity.Role == AuthorizationConstants.PROFESSOR)
            {
                VerifyAuthorizationProfessor(entity, identity);
            }

            return entity;
        }

        private async Task<WorkSubmitEntity> VerifyUpdateAngGetEntityStudent(int? id, UserIdentity identity)
        {
            var entity = await VerifyAndGetEntity(id, identity);

            if (entity.State != WorkSubmitState.CREATED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_IS_NOT_CREATED);

            return entity;
        }

        private async Task<WorkSubmitEntity> VerifyUpdateAngGetEntityProfessor(int? id, UserIdentity identity)
        {
            var entity = await VerifyAndGetEntity(id, identity);

            if (entity.State != WorkSubmitState.SUBMITTED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_IS_NOT_SUBMITTED);

            return entity;
        }

        private void VerifyAuthorizationStudent(WorkSubmitEntity entity, UserIdentity identity)
        {
            if (entity.UserId != identity.Id)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE);
        }

        private void VerifyAuthorizationProfessor(WorkSubmitEntity entity, UserIdentity identity)
        {
            if (entity.Assignment == null)
            {
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE);
            }

            if (entity.Assignment.Course.UserId != identity.Id)
            {
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE);
            }
        }

        #endregion VERIFICATION
  
    }
}
