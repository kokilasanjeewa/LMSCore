using Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace Core.Application.Features.DocType.Command
{
    public sealed class CreateDocTypeCommandValidator : AbstractValidator<CreateDocTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDocTypeCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.DocumentType)
                    .NotNull()
                    .MustAsync(IsUniqueDocType)
                    .WithMessage("Document type is required.");
        }
        private async Task<bool> IsUniqueDocType(string docType, CancellationToken cancellationToken)
        {
            bool isDocType = await _unitOfWork.Repository<Domain.Entities.DocType>().AnyAsync(x => x.DocumentType == docType);
            return !isDocType;
        }
    }
}
