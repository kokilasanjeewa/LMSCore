using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.DocType.Command
{
    public class DeleteDocTypeValidator : AbstractValidator<DeleteDocTypeCommand>
    {
        public DeleteDocTypeValidator()
        {
            RuleFor(x => x.DocTypeSerialID)
              .NotEmpty()
              .NotNull()
              .WithMessage("The polling division is required.");

            RuleFor(x => x.DeletedBy)
                .GreaterThan(0)
               .NotEmpty()
               .NotNull()
               .WithMessage("The user is required.");
        }
    }
}
