using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Users.Queries.GetTokenByLogin
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private readonly IUserRepository _userRepository;

        public LoginModelValidator(IUserRepository userRepository)
        {
            _userRepository= userRepository;

            RuleFor(x => x.UserID)
                .NotNull().WithMessage("User is required.");

            RuleFor(x => x.ComSerialID)
                .GreaterThan(0).WithMessage("Company is required.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.");
            RuleFor(x => x.UserID)
                .NotNull()
                .MustAsync(IsUniqueUseID)
                .WithMessage("The user not found.");
        }
        private async Task<bool> IsUniqueUseID(string username, CancellationToken cancellationToken)
        {
            bool isExistingUsername = await _userRepository.UserIDExistsAsync(username, cancellationToken);
            return isExistingUsername;
        }
    }
}
