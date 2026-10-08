using Core.Domain.Common;

namespace Core.Application.Features.Approval.Command
{
    public class ApproveWorkFlowUpdatedEvent : BaseEvent
    {
        public Domain.Entities.ApprovalWorkflow ApprovalWorkflow { get; }

        public ApproveWorkFlowUpdatedEvent(Domain.Entities.ApprovalWorkflow approvalWorkflow)
        {
            ApprovalWorkflow = approvalWorkflow;
        }
    }
}
