using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Features.Users.Queries.GetTokenByLogin;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Users.Queries.GetUser
{
    public record GetUserQuery : IRequest<Result<SearchUserDto>>
    {
        [Required]
        public long UserSerialID { get; set; }
        public GetUserQuery(long userSerialID)
        {
            UserSerialID = userSerialID;
        }
     
    }
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<SearchUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _userRepository = userRepository;
        }

        public async Task<Result<SearchUserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            GetUserCommandValidator validator = new GetUserCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(query,cancellationToken);
            if (validationResult.IsValid)
            {
                // Step 1: Retrieve the user along with GrpSerialID and necessary includes
                var user = await _unitOfWork.Repository<User>().GetEntityWithThenIncludesAsync(
                    u => u.UserSerialID == query.UserSerialID && u.Active == true,
                    cancellationToken, // Pass cancellationToken to the database query
                    q => q.Include(e => e.MenuPermissions).ThenInclude(np => np.Menu),
                    q => q.Include(e => e.Companies).Where(a => a.IsDeleted == false)
                    );

                // Get the GrpSerialID from the retrieved user
                int grpSerialID = user.GrpSerialID ?? 0; // Assuming Group might be null, handle accordingly

                // Step 2: Retrieve the user's MenuPermissions filtered by the GrpSerialID
                user.MenuPermissions =user.MenuPermissions.Where(a => a.GrpSerialID == grpSerialID).ToList();

                var userDto = _mapper.Map<SearchUserDto>(user);

                if (userDto == null)
                {
                    return await Result<SearchUserDto>.FailureAsync(data: new SearchUserDto(),message: "User not found");
                }

                return await Result<SearchUserDto>.SuccessAsync(data: userDto, message: "Found successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<SearchUserDto>.FailureAsync(data: new SearchUserDto(), messages: errors);
            }

        }
    }
}



#region
//q => q.Include(e => e.Group).ThenInclude(e => e.GroupMenus).ThenInclude(e => e.Menu),
#endregion