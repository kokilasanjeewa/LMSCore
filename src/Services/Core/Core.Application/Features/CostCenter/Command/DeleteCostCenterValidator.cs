
using FluentValidation;

namespace Core.Application.Features.CostCenter.Command
{
  
    public class DeleteCostCenterValidator : AbstractValidator<DeleteCostCenterCommand>
    {
        public DeleteCostCenterValidator()
        {
            RuleFor(x => x.CostCenterSerialID)
              .NotEmpty()
              .NotNull()
              .WithMessage("The Building is required.");

            RuleFor(x => x.DeletedBy)
                .GreaterThan(0)
               .NotEmpty()
               .NotNull()
               .WithMessage("The user is required.");
        }
    }
}
