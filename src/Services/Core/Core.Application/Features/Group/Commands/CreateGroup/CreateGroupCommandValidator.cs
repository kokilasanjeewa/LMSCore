using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Commands.CreateGroup
{

    public sealed class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandValidator(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;

            RuleFor(x => x.GropName)
                    .NotNull()
                    .MustAsync(IsUniqueGroup)
                    .WithMessage("Group already exists.");
        }
        private async Task<bool> IsUniqueGroup(string groupName, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _groupRepository.IsUniqueGroup(groupName, cancellationToken);
            return !isExistingUsername;
        }
    }
}
