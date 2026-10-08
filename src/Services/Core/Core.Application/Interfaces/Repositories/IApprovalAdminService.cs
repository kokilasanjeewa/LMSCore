using Core.Application.DTOs.Approval;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IApprovalAdminService
    {
        Task<ApprovalWorkflow> CreateWorkflowAsync(ApprovalWorkflowCreateDto dto);
        Task<ApprovalStep> AddStepAsync(ApprovalStepCreateDto dto);
        Task<List<EntityTypeDto>> GetAllEntityAsync();

    }

}
