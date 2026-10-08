using Core.Application.Features.Approval.Command;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;

namespace Core.Application.DTOs.Approval
{
    public class ApprovalActionDto : IRequest<Result<int>>
    {
        /// <summary>
        /// Approval request identifier
        /// Example: 5001
        /// </summary>
        public int ApprovalRequestID { get; set; }

        /// <summary>
        /// Step being acted upon
        /// Example: Finance Step ID
        /// </summary>
        public int StepID { get; set; }

        /// <summary>
        /// Approval action (Approve / Reject)
        /// </summary>
        public ApprovalActionType ActionType { get; set; }

        /// <summary>
        /// Logged-in approver
        /// Example: 10 (Finance Manager)
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Optional remarks
        /// Example: Credit verified
        /// </summary>
        public string? Remarks { get; set; }
    }
    public class ExecuteSingleApprovalActionCommandHandler
    : IRequestHandler<ApprovalActionDto, Result<int>>
    {
        private readonly IApprovalService _service;

        public ExecuteSingleApprovalActionCommandHandler(IApprovalService service)
        {
            _service = service;
        }

        public async Task<Result<int>> Handle(
            ApprovalActionDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _service.ExecuteApprovalActionAsync(
                    request.ApprovalRequestID,
                    request.StepID,
                    request.ActionType,
                    request.UserID,
                    request.Remarks
                );
            }
            catch (UnauthorizedAccessException ex)
            {
                return await Result<int>.FailureAsync(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return await Result<int>.FailureAsync(ex.Message);
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync(
                    ex.InnerException?.Message ?? ex.Message
                );
            }
        }
    }


}
