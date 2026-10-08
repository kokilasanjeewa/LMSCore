using Core.Application.DTOs.Approval;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories
{
    public class ApprovalAdminService : IApprovalAdminService
    {
        private readonly ApplicationDbContext _db;

        public ApprovalAdminService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApprovalWorkflow> CreateWorkflowAsync(ApprovalWorkflowCreateDto dto)
        {
            var workflow = new ApprovalWorkflow
            {
                WorkflowName = dto.WorkflowName,
                WorkflowCode = dto.WorkflowCode,
                EntityTypeID = dto.EntityTypeID,
                ComSerialID = dto.CompanyID
            };

            _db.ApprovalWorkflow.Add(workflow);
            await _db.SaveChangesAsync();
            return workflow;
        }

        public async Task<ApprovalStep> AddStepAsync(ApprovalStepCreateDto dto)
        {
            var step = new ApprovalStep
            {
                ApprovalWorkflowID = dto.ApprovalWorkflowID,
                StepOrder = dto.StepOrder,
                StepCode = dto.StepCode,
                StepName = dto.StepName,
                ApprovalRole = dto.ApprovalRole,
                PlantID = dto.PlantID,
                IsMandatory = dto.IsMandatory,
                CanReject = dto.CanReject,
                IsFinalStep = dto.IsFinalStep
            };

            _db.ApprovalStep.Add(step);
            await _db.SaveChangesAsync();
            return step;
        }

        public async Task<List<EntityTypeDto>> GetAllEntityAsync()
        {
            return await _db.EntityType.AsNoTracking().Select(e => new EntityTypeDto
                                         {
                                             EntityTypeID = e.EntityTypeID,
                                             EntityCode = e.EntityCode,
                                             EntityName = e.EntityName,
                                             Active = e.Active,
                                             IsDeleted = e.IsDeleted
                                         }).ToListAsync();
        }
    }

}
