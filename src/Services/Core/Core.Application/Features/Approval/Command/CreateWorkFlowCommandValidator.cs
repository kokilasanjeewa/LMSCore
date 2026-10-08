using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using FluentValidation;


namespace Core.Application.Features.Approval.Command
{
    public sealed class CreateWorkFlowCommandValidator : AbstractValidator<CreateWorkFlowCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateWorkFlowCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.WorkflowName)
                    .NotNull()
                    .MustAsync(IsUniqueName)
                    .WithMessage("Workflow name already exists.");

            RuleFor(x => x.WorkflowCode)
                   .NotNull()
                   .MustAsync(IsUniqueCode)
                   .WithMessage("Workflow code already exists.");
                     

            RuleFor(x => x.Steps)
                    .Must(steps => steps.Count(s => s.IsFinalStep) == 1)
                    .WithMessage("Only one final approval step is allowed.");
            RuleFor(x => x.Steps)
                    .Must(steps => steps.Select(s => s.StepOrder).Distinct().Count() == steps.Count)
                    .WithMessage("Approval step order must be unique.");
            RuleFor(x => x.Steps)
                    .NotEmpty()
                    .Must(HaveUniqueStepCodes)
                    .WithMessage("Approval step codes must be unique.");

            RuleFor(x => x.Steps)
                .Must(HaveUniqueStepNames)
                .WithMessage("Approval step names must be unique.");

            // -------------------------
            // Per-step field validation
            // -------------------------
            RuleForEach(x => x.Steps).ChildRules(step =>
            {
                step.RuleFor(s => s.StepCode)
                    .NotEmpty()
                    .MaximumLength(20);

                step.RuleFor(s => s.StepName)
                    .NotEmpty()
                    .MaximumLength(100);

                step.RuleFor(s => s.ApprovalRole)
                    .NotEmpty()
                    .MaximumLength(100);

                step.RuleFor(s => s.StepOrder)
                    .GreaterThan(0);
            });

        }
        private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        {
            bool isCompanyName = await _unitOfWork.Repository<ApprovalWorkflow>().AnyAsync(x => x.WorkflowName == name);
            return !isCompanyName;
        }
        private async Task<bool> IsUniqueCode(string code, CancellationToken cancellationToken)
        {
            bool isCompanyName = await _unitOfWork.Repository<ApprovalWorkflow>().AnyAsync(x => x.WorkflowCode == code);
            return !isCompanyName;
        }
        // -----------------------
        // In-memory step checks
        // -----------------------
        private bool HaveUniqueStepCodes(List<CreateApprovalStepDto> steps)
        {
            return steps
                .Select(s => s.StepCode?.Trim().ToLower())
                .Distinct()
                .Count() == steps.Count;
        }

        private bool HaveUniqueStepNames(List<CreateApprovalStepDto> steps)
        {
            return steps
                .Select(s => s.StepName?.Trim().ToLower())
                .Distinct()
                .Count() == steps.Count;
        }
    }

}
