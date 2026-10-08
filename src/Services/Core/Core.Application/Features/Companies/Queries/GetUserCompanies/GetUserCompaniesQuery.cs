using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Companies.Queries.GetUserCompanies
{
    public class GetUserCompaniesQuery : IRequest<Result<List<UserCompanyDto>>>
    {
        public bool Active { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public long? SerialID { get; set; }
        public string? UserID { get; set; }
    }
    internal class GetUserCompaniesQueryHandler : IRequestHandler<GetUserCompaniesQuery, Result<List<UserCompanyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserCompaniesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserCompanyDto>>> Handle(GetUserCompaniesQuery query, CancellationToken cancellationToken)
        {
            GetUserCompaniesValidator validator = new GetUserCompaniesValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<List<UserCompanyDto>>.FailureAsync(data: new List<UserCompanyDto>(), messages: errors);
            }

            // Step 1: Retrieve the user along with companies 
            var user = await _unitOfWork.Repository<User>().GetEntityWithThenIncludesAsync(
                    u => u.UserID == query.UserID && u.Active == true,
                    cancellationToken,
                    q => q.Include(e => e.Companies.Where(c => c.Active == true)).ThenInclude(e => e.Company).Where(a => a.IsDeleted == false));
            var userDto = _mapper.Map<List<UserCompanyDto>>(user.Companies);

            if (userDto == null)
            {
                return await Result<List<UserCompanyDto>>.FailureAsync(data: new List<UserCompanyDto>(), message: "Company not found");
            }

            return await Result<List<UserCompanyDto>>.SuccessAsync(data: userDto, message: "Loading allowed company list...");


        }
    }

}
