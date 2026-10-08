using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using FluentValidation;

namespace Core.Application.Features.Users.Commands.LogoutUser
{
    public sealed class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public LogoutUserCommandValidator(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
            RuleFor(command => command.UserID)
                    .NotEmpty()
                    .WithMessage("The userid can't be empty.");
            RuleFor(command => command.Token)
                     .NotEmpty()
                     .WithMessage("The token can't be empty.");
            RuleFor(command => command)
                     .Must(c => JwtTokenHelper.TryResolveExpirationTime(c.Token, c.ExpirationTime, out _))
                     .WithMessage("The expiration time can't be empty or invalid.");
           
            RuleFor(x => x.Token)
                    .NotNull()
                    .MustAsync(IsTokenInvalidated)
                    .WithMessage("Your token has expired.");
        }
        private async Task<bool> IsTokenInvalidated(string token ,CancellationToken cancellationToken)
        {
            bool isExistingToken = await _userRepository.UserTokenExistsAsync(token,cancellationToken);
            return !isExistingToken;
        }
    }
}
