using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using FluentValidation;


namespace Core.Application.Features.Approval.Command
{
    public sealed class UpdateWorkFlowCommandValidator : AbstractValidator<UpdateWorkFlowCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWorkFlowCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.WorkflowName)
                .NotEmpty()
                .MustAsync(async (command, name, cancellationToken) =>
                await IsUniqueName(command.approvalWorkflowID, name!, cancellationToken))
                .WithMessage("Workflow name already exists.");

            RuleFor(x => x.WorkflowCode)
                .NotEmpty()
                .MustAsync(async (command, code, cancellationToken) =>
                await IsUniqueCode(command.approvalWorkflowID, code!, cancellationToken))
                .WithMessage("Workflow code already exists.");

            RuleFor(x => x.Steps)
                    .Must(steps => steps.Count(s => s.IsFinalStep) == 1)
                    .WithMessage("Only one final approval step is allowed.");
            RuleFor(x => x.Steps)
                    .Must(steps => steps.Select(s => s.StepOrder).Distinct().Count() == steps.Count)
                    .WithMessage("Approval step order must be unique.");

            // -------------------------
            // Per-step field validation
            // -------------------------
            RuleForEach(x => x.Steps).ChildRules(step =>
            {
                step.RuleFor(s => s.StepCode)
                    .NotEmpty()
                    .WithMessage("Approval step codes must be unique.")
                    .MaximumLength(20);

                step.RuleFor(s => s.StepName)
                    .NotEmpty()
                    .WithMessage("Approval step names must be unique.")
                    .MaximumLength(100);

                step.RuleFor(s => s.ApprovalRole)
                    .NotEmpty()
                    .MaximumLength(100);

                step.RuleFor(s => s.StepOrder)
                    .GreaterThan(0);
            });

        }
        private async Task<bool> IsUniqueName(
           int workflowId,
           string name,
           CancellationToken cancellationToken)
        {
            name = name.Trim();

            return !await _unitOfWork
                .Repository<ApprovalWorkflow>()
                .AnyAsync(x =>
                    x.WorkflowName == name &&
                    x.ApprovalWorkflowID != workflowId);
        }

        private async Task<bool> IsUniqueCode(
            int workflowId,
            string code,
            CancellationToken cancellationToken)
        {
            code = code.Trim();

            return !await _unitOfWork
                .Repository<ApprovalWorkflow>()
                .AnyAsync(x =>
                    x.WorkflowCode == code &&
                    x.ApprovalWorkflowID != workflowId);
        }

        // -----------------------
        // In-memory step checks
        // -----------------------
        private bool HaveUniqueStepCodes(List<UpdateApprovalStepDto> steps)
        {
            return steps
                .Select(s => s.StepCode?.Trim().ToLower())
                .Distinct()
                .Count() == steps.Count;
        }

        private bool HaveUniqueStepNames(List<UpdateApprovalStepDto> steps)
        {
            return steps
                .Select(s => s.StepName?.Trim().ToLower())
                .Distinct()
                .Count() == steps.Count;
        }
    }
}
