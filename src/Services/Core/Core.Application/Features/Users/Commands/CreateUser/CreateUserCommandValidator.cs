using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserID)
                    .NotEmpty()
                    .WithMessage("UserID is required.");

            RuleFor(x => EnumConverter.StringToEnum<StatusType>(x.Status))
                     .NotEmpty()
                     .WithMessage("Status is required.");

            RuleFor(x => (PermissionType)EnumConverter.StringToEnum<PermissionType>(x.PermissionType))
                    .NotEmpty()
                    .WithMessage("Permission type is required.");

            RuleFor(x => x.UserID)
                    .NotNull()
                    .MustAsync(IsUniqueUseID)
                    .WithMessage("User already exists.");
        }
        private async Task<bool> IsUniqueUseID(string username, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _userRepository.UserIDExistsAsync(username, cancellationToken);
            return !isExistingUsername;
        }
    }
}
