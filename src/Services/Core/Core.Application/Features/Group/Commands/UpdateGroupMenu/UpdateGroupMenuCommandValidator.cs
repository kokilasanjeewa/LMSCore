using Core.Application.Features.Group.Commands.UpdateGroupMenu;
using Core.Application.Interfaces.Repositories;
using FluentValidation;


namespace Core.Application.Features.Group.Commands.CreateGroup
{

    public sealed class UpdateGroupMenuCommandValidator : AbstractValidator<UpdateGroupMenuCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupMenuCommandValidator(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            RuleFor(command => command.UserSerialID)
                    .NotEmpty().WithMessage("User was not found.");
            RuleFor(command => command.GroupName)
                .NotEmpty().WithMessage("Group was not found.")
                .NotNull().WithMessage("Group cannot be null.")
                .MustAsync(IsUniqueGroup).WithMessage("The group name must be unique.")
                .WithMessage("The group name must be unique.");
            // You can also add a separate check for the GroupSerialID if necessary
            RuleFor(command => command.GrpSerialID)
                .NotEmpty().WithMessage("Group was not found.");
        }
        private async Task<bool> IsUniqueGroup(UpdateGroupMenuCommand command, string groupName, CancellationToken cancellationToken)
        {
            // Check if the group name is unique, excluding the group with the provided GroupSerialID
            bool isExistingGroup = await _groupRepository.IsExactlyUniqueGroup(groupName, command.GrpSerialID, cancellationToken);
            return !isExistingGroup;  // Returns true if the group name is unique
        }
    }
}
