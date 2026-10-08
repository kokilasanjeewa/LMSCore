using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;


namespace Core.Application.Features.Users.Commands.CloneUser
{
    
    public sealed class CloneUserCommandValidator : AbstractValidator<CloneUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CloneUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.CloneToUserID)
                    .NotEmpty()
                    .WithMessage("UserID is required.");

            RuleFor(x => x.UserSerialID)
                    .NotNull()
                    .MustAsync(IsValidUseID)
                    .WithMessage("User doesn't exists.");
        }
        private async Task<bool> IsValidUseID(int UserSerialID, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _userRepository.IsValidUserID(UserSerialID, cancellationToken);
            return isExistingUsername;
        }
    }
}
