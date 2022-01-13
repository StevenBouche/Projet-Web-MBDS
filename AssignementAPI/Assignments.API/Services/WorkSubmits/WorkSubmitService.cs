using Assignments.API.Exceptions.Business;
using Assignments.API.Exceptions.Entities;
using Assignments.API.Extentions.ModelExtentions;
using Assignments.API.Models.Authentification;
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
        public WorkSubmitService(IWorkSubmitRepository repository, ILogger<WorkSubmitService> logger) : base(repository, logger)
        {

        }

        public async Task<WorkSubmit> CreateWorkSubmitAsync(WorkSubmitStudentForm form, UserIdentity identity)
        {
            var entity = await Repository.AddAsync(new WorkSubmitEntity() { 
                Label = form.Label, 
                Description = form.Description, 
                UserId = identity.Id,
                AssignmentId = form.AssignmentId,
                State = WorkSubmitState.CREATED
            });
            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> EvaluateWorkSubmitAsync(WorkSubmitActionForm form, UserIdentity identity)
        {
            var entity = await VerifyUpdateAngGetEntityStudent(form.Id, identity);

            entity.State = WorkSubmitState.EVALUATED;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> SubmitWorkSubmitAsync(WorkSubmitActionForm form, UserIdentity identity)
        {
            var entity = await VerifyUpdateAngGetEntityStudent(form.Id, identity);

            if(entity.AssignmentId == null)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_IS_NOT_LINK_ASSIGNMENT);

            entity.State = WorkSubmitState.SUBMITTED;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitStudentForm form, UserIdentity identity)
        {
            var entity = await VerifyUpdateAngGetEntityStudent(form.Id, identity);

            entity.Label = form.Label;
            entity.Description = form.Description;
            entity.AssignmentId = form.AssignmentId;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public async Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitProfessorForm form, UserIdentity identity)
        {
            var entity = await VerifyUpdateAngGetEntityProfessor(form.Id, identity);

            entity.Grade = form.Grade;
            entity.Comment = form.Comment;

            await Repository.UpdateAsync(entity);

            return entity.ToWorkSubmit();
        }

        public Task DeleteWorkSubmitAsync(int id, UserIdentity identity)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form)
        {
            var pagination = await GetPaginationAsync(form);
            return MapPagination(pagination, entity => entity.ToWorkSubmit());
        }

        public async Task<PaginationResult<WorkSubmit>> GetMyWorkSubmitsAsync(PaginationForm form, UserIdentity identity)
        {
            var pagination = await GetPaginationAsync(form, entity => entity.UserId == identity.Id);
            return MapPagination(pagination, entity => entity.ToWorkSubmit());
        }

        public async Task<WorkSubmit> GetWorkSubmitByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            return entity.ToWorkSubmit();
        }

        public async Task<PaginationResult<WorkSubmit>> GetAllSubmitsOfAssignmentAsync(PaginationIdForm form)
        {
            var pagination = await GetPaginationAsync(form, entity => entity.AssignmentId == form.Id);
            return MapPagination(pagination, entity => entity.ToWorkSubmit());
        }

        private async Task<WorkSubmitEntity> VerifyUpdateAngGetEntityStudent(int? id, UserIdentity identity)
        {
            if (id is null)
                throw new ArgumentException("id column missing"); //TODO change

            var entity = await Repository.GetByIdAsync((int)id);

            if (entity == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            if (entity.UserId != identity.Id)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE);

            if (entity.State != WorkSubmitState.CREATED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_IS_NOT_CREATED);

            return entity;
        }

        private async Task<WorkSubmitEntity> VerifyUpdateAngGetEntityProfessor(int? id, UserIdentity identity)
        {
            if (id is null)
                throw new ArgumentException("id column missing"); //TODO change

            var entity = await Repository.GetByIdAsync((int)id);

            if (entity == null)
                throw new EntityException(EntityExceptionTypes.NOT_FOUND);

            if (entity.Assignment.Course.UserId != identity.Id)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_UNAUTHORIZE);

            if (entity.State != WorkSubmitState.SUBMITTED)
                throw new WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_IS_NOT_SUBMITTED);

            return entity;
        }
    }
}
