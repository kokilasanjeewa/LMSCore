using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Features.Users.Commands.UpdateUser;
using Core.Application.Features.Users.Queries.GetTokenByLogin;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Application.Features.Users.Commands.ChangePassword
{
    
    public record ChangePasswordCommand : IRequest<Result<int>>, IMapFrom<User>
    {
        [Required]
        public string? UserID { get; set; }
        [Required]
        public string? CurrentPassWd { get; set; }
        [Required]
        public string? NewPassWd { get; set; }

    }

    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            // Instantiate the validator
            ChangePasswordCommandValidator validator = new ChangePasswordCommandValidator(_userRepository);

            // Perform validation
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                // Handle validation failures
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<int>.FailureAsync(errors);
            }

            try
            {
                // Retrieve existing user
                var existingUser = await _userRepository.GetUserByUserName(loginName: command.UserID, cancellationToken);
                if (existingUser == null || existingUser.IsDeleted)
                {
                    return await Result<int>.FailureAsync(new List<string> { "User is incorrect." });
                }
                var passwordHash = PasswordHasher.ComputeHash(command.CurrentPassWd, existingUser.PasswdSalt, _pepper, _iteration);
                if (existingUser.PasswdHash != passwordHash)
                {
                    return await Result<int>.FailureAsync(new List<string> { "Current Password is incorrect." });
                }
                if (existingUser == null)
                {
                    return await Result<int>.FailureAsync(new List<string> { "Could not be found." });
                }
                // Update properties
                  if (!string.IsNullOrWhiteSpace(command.NewPassWd))
                {
                    if (!IsPasswordComplex(command.NewPassWd))
                    {
                        throw new ArgumentException("Password does not meet complexity requirements.");
                    }
                    existingUser.PassWd = command.NewPassWd;
                    existingUser.PasswdSalt = PasswordHasher.GenerateSalt();
                    existingUser.PasswdHash = PasswordHasher.ComputeHash(command.NewPassWd, existingUser.PasswdSalt, _pepper, _iteration);
                }

                
                // Save changes
                await _unitOfWork.Repository<User>().UpdateAsync(existingUser, existingUser.UserSerialID);
                existingUser.AddDomainEvent(new UserUpdatedEvent(existingUser));

                await _unitOfWork.Save(cancellationToken);
                return await Result<int>.SuccessAsync(existingUser.UserSerialID, "Changed successfully.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<int>.FailureAsync(new List<string> { ex.Message + ex.InnerException });
            }
        }

        public bool IsPasswordComplex(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            // Password complexity rules:
            // At least 8 characters long
            // Contains at least one uppercase letter
            // Contains at least one lowercase letter
            // Contains at least one digit
            // Contains at least one special character
            const int minLength = 8;
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return password.Length >= minLength && hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }


    }
}
