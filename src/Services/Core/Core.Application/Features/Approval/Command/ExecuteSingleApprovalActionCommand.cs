using Core.Domain.Entities;
using Core.Shared;
using MediatR;


namespace Core.Application.Features.Approval.Command
{
    public class ExecuteSingleApprovalActionCommand : IRequest<Result<int>>
    {
        public int ApprovalRequestID { get; set; }
        public int StepID { get; set; }
        public ApprovalActionType ActionType { get; set; }
        public int UserID { get; set; }
        public string? Remarks { get; set; }
    }

}
