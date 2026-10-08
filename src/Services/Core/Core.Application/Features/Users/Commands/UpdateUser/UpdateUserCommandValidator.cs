using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using FluentValidation;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(command => command.UserSerialID)
            .NotEmpty().WithMessage("UserSerialID is required.");

            RuleFor(command => command.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");

            RuleFor(command => command.PassWd)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(command => command.PermissionType)
                    .NotEmpty()
                    .WithMessage("The permission type can't be empty.");

            RuleFor(command => command.UserSerialID)
                    .NotNull()
                    .MustAsync(IsUserID)
                    .WithMessage("Could not be found.");
        }
        private async Task<bool> IsUserID(int userSerialID, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _userRepository.IsValidUserID(userSerialID, cancellationToken);
            return isExistingUsername;
        }
    }
}
