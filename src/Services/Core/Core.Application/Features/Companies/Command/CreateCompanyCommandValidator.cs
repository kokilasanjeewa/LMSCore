using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using FluentValidation;

namespace Core.Application.Features.Companies.Command
{
    public sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.ComName)
                    .NotNull()
                    .MustAsync(IsUniqueCompany)
                    .WithMessage("Company is unique.");
        }
        private async Task<bool> IsUniqueCompany(string comName, CancellationToken cancellationToken)
        {
            bool isCompanyName = await _unitOfWork.Repository<Company>().AnyAsync(x => x.ComName == comName);
            return !isCompanyName;
        }
    }
}
