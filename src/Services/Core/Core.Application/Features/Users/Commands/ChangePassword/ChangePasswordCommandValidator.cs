using Core.Application.Features.Users.Commands.UpdateUser;
using Core.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Application.Features.Users.Commands.ChangePassword
{
   
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(command => command.UserID)
            .NotEmpty().WithMessage("UserID is required.");
            RuleFor(command => command.CurrentPassWd)
                .NotNull().WithMessage("Old Password is required.")
                .NotEmpty().WithMessage("Old Password is required.");
            RuleFor(command => command.NewPassWd)
                .NotNull().WithMessage("New Password is required.")
                .NotEmpty().WithMessage("New Password is required.");

            RuleFor(command => command.UserID)
                    .NotNull()
                    .MustAsync(IsUserID)
                    .WithMessage("Could not be found.");
        }
        private async Task<bool> IsUserID(string userID, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByUserName(loginName: userID, cancellationToken);
            if (existingUser == null) {
                return false;
            } 
            return true;
        }
    }
}
