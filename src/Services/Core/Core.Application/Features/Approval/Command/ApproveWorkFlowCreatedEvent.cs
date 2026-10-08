using Core.Domain.Common;

namespace Core.Application.Features.Approval.Command
{
    public class ApproveWorkFlowCreatedEvent : BaseEvent
    {
        public Domain.Entities.ApprovalWorkflow ApprovalWorkflow { get; }

        public ApproveWorkFlowCreatedEvent(Domain.Entities.ApprovalWorkflow approvalWorkflow)
        {
            ApprovalWorkflow = approvalWorkflow;
        }
    }
    
}
