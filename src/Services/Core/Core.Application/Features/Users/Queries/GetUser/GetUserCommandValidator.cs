using Core.Application.Features.Users.Queries.GetUser;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Users.Commands.CreateUser
{
    public sealed class GetUserCommandValidator : AbstractValidator<GetUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserCommandValidator(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
            RuleFor(command => command.UserSerialID)
                    .NotEmpty()
                    .WithMessage("The user can't be empty.");
            RuleFor(x => x.UserSerialID)
                    .NotNull()
                    .MustAsync(IsValidUserID)
                    .WithMessage("The user is incorrect.");
        }
        private async Task<bool> IsValidUserID(long userSerialID, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _userRepository.IsValidUserID(userSerialID, cancellationToken);
            return isExistingUsername;
        }
    }
}
