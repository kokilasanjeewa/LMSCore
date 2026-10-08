using Core.Application.DTOs.Approval;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using MediatR;


namespace Core.Application.Features.Approval.Command
{
    public class ExecuteApprovalActionCommand : IRequest<Result<List<ApproveActionResultDto>>>
    {
        public List<ApprovalActionItemDto> Items { get; set; } = new();

        public int ActionType { get; set; }
        public int UserID { get; set; }
        public string? Remarks { get; set; }
    }
    public class ExecuteApprovalActionCommandHandler
    : IRequestHandler<ExecuteApprovalActionCommand, Result<List<ApproveActionResultDto>>>
    {
        private readonly IApprovalService _service;

        public ExecuteApprovalActionCommandHandler(IApprovalService service)
        {
            _service = service;
        }

        public async Task<Result<List<ApproveActionResultDto>>> Handle(
            ExecuteApprovalActionCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Items == null || request.Items.Select(c => c.ApprovalRequestID).Count() != request.Items.Select(c => c.ApprovalStepID).Count())
            {
                return await Result<List<ApproveActionResultDto>>.FailureAsync(message: "ApprovalRequestIDs and StepID count must match.");
            }

            var results = new List<int>();
            var resultActions = new List<ApproveActionResultDto>();

            foreach (var item in request.Items)
            { try
                {
                    var result = await _service.ExecuteApprovalActionAsync(
                        item.ApprovalRequestID,
                        item.ApprovalStepID,
                        (Domain.Entities.ApprovalActionType)request.ActionType,
                        request.UserID,
                        request.Remarks
                    );
                    results.Add(result.Data);
                    resultActions.Add(new ApproveActionResultDto
                    {
                        SerialID = item.SerialID,
                        IsCompleted = result.Data == 1 ? true : false,
                        TypeCategory = item.TypeCategory

                    });
                }
                catch (UnauthorizedAccessException ex)
                {
                    return await Result<List<ApproveActionResultDto>>.FailureAsync(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    return await Result<List<ApproveActionResultDto>>.FailureAsync(ex.Message);
                }
                catch (Exception ex)
                {
                    return await Result<List<ApproveActionResultDto>>.FailureAsync(ex.InnerException?.Message ?? ex.Message);
                }
            }
            return await Result<List<ApproveActionResultDto>>.SuccessAsync(data: resultActions, message: "Approved successfully");
        }
    }

}
