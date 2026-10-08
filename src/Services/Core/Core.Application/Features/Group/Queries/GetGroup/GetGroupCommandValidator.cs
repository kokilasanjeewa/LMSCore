using Core.Application.Features.Users.Queries.GetUser;
using Core.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Queries.GetGroup
{
      public sealed class GetGroupCommandValidator : AbstractValidator<GetGroupQuery>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupCommandValidator(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            RuleFor(command => command.GroupSerialID)
                    .NotEmpty()
                    .WithMessage("The Group can't be empty.");
            RuleFor(x => x.GroupSerialID)
                    .NotNull()
                    .MustAsync(IsValidGroupID)
                    .WithMessage("The Group is incorrect.");
        }
        private async Task<bool> IsValidGroupID(long groupSerialID, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _groupRepository.IsValidGroupID(groupSerialID, cancellationToken);
            return isExistingUsername;
        }
    }
}
