using Core.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.DocType.Command
{
    public sealed class UpdateDocTypeCommandValidator : AbstractValidator<UpdateDocTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDocTypeCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.DocumentType)
                    .NotNull()
                    .WithMessage("Document type is required.");
            RuleFor(x => x.DocumentType)
                    .NotNull()
                   // .MustAsync(IsUniqueDocType)
                    .WithMessage("The document type already exists.\r\n");
        }
        private async Task<bool> IsUniqueDocType(string docType, CancellationToken cancellationToken)
        {
            bool isDocType = await _unitOfWork.Repository<Domain.Entities.DocType>().AnyAsync(x => x.DocumentType == docType);
            return !isDocType;
        }
    }
}
