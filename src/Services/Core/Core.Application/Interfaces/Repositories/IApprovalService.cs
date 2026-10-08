using Core.Domain.Entities;
using Core.Shared;


namespace Core.Application.Interfaces.Repositories
{
     public interface IApprovalService
    {
        Task<ApprovalRequest> CreateApprovalRequestAsync(
            int entityTypeID,
            int entityID,
            int workflowID,
            int requestedByUserID,
            int companyID,
            int? plantID = null);

        Task<Result<int>> ExecuteApprovalActionAsync(
            int approvalRequestID,
            int stepID,
            ApprovalActionType actionType,
            int userSerialID,
            string? remarks = null);

        Task<ApprovalRequest?> GetCurrentApprovalAsync(int entityTypeID, int entityID);

        Task<IEnumerable<ApprovalActionHistory>> GetApprovalHistoryAsync(int entityTypeID, int entityID);
        Task<ApprovalWorkflow?> GetWorkflowAsync(int entityTypeID,int companyID,int? plantID = null,int? entityID = null);
    }
    public interface IUserRoleService
    {
        Task<bool> HasRoleAsync(int userId, string role);
    }
    public interface IApproverResolver
    {
        Task<int> ResolveApproverAsync(string role, int companyId, int? plantId);
    }
}
