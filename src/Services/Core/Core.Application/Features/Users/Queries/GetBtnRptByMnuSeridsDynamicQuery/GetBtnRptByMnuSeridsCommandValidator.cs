using Core.Application.Features.Users.Queries.GetUser;
using Core.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Users.Queries.GetBtnRptByMnuSeridsDynamicQuery
{
     public sealed class GetBtnRptByMnuSeridsCommandValidator : AbstractValidator<GetBtnRptByMnuSeridsDynamicQuery>
    {
        public GetBtnRptByMnuSeridsCommandValidator()
        {
            RuleFor(command => command.MnuSerialIDs)
                    .NotEmpty().WithMessage("Please ensure to select grop.")
                    .NotNull().WithMessage("Please ensure to select grop.")
                    .WithMessage("Please ensure to select grop.");
        }

    }
}
