using Core.Application.Features.Users.Queries.GetUsersWithPagination;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Companies.Queries.GetUserCompanies
{
    public class GetUserCompaniesValidator : AbstractValidator<GetUserCompaniesQuery>
    {
        private readonly IUserRepository _userRepository;

        public GetUserCompaniesValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserID)
                    .NotEmpty()
                    .WithMessage("The user is required.");
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
