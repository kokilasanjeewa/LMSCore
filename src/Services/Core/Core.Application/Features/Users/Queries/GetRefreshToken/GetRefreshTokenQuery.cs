using AutoMapper;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Features.Users.Queries.GetTokenByLogin;
using Core.Application.Helper;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Threading;

namespace Core.Application.Features.Users.Queries.GetRefreshToken
{

    public record GetRefreshTokenQuery : IRequest<Result<GetTokenByLoginDto>>
    {
        public string? refreshToken { get; set; }
        public GetRefreshTokenQuery() { }
        public GetRefreshTokenQuery(string token)
        {
            refreshToken = token;
        }
    }
    internal class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, Result<GetTokenByLoginDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenService jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;

        public GetRefreshTokenQueryHandler(IMapper mapper, IUserRepository _userRepository, ITokenService _jwtTokenService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            userRepository = _userRepository;
            jwtTokenService = _jwtTokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetTokenByLoginDto>> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
        {
            var authenticationModel = new GetTokenByLoginDto();
            var user = await userRepository.GetUserByRefreshToken(query.refreshToken, cancellationToken); 
            if (user == null)
            {
                return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = "", isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = "", userID = "", RefreshToken = "", RefreshTokenExpiration = DateTime.MinValue }, $"Token did not match any users.");
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == query.refreshToken);

            if (!refreshToken.IsActive)
            {
                return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = "", isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = user?.UserName, userID = user?.UserID, RefreshToken = refreshToken.Token, RefreshTokenExpiration = refreshToken.Expires }, $"Token Not Active.");
            }

            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.Now;

            //Generate new Refresh Token and save to Database
            var newRefreshToken = jwtTokenService.CreateRefreshToken();
            newRefreshToken.Active = newRefreshToken.IsActive;
            try
            {
                user.RefreshTokens.Add(newRefreshToken);
                await _unitOfWork.Repository<User>().UpdateAsync(user, user.UserSerialID);
                user.AddDomainEvent(new UserCreatedEvent(user));
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = "", isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = user?.UserName, userID = user?.UserID, RefreshToken = "", RefreshTokenExpiration = DateTime.MinValue }, ex.Message);

            }
            //Generates new jwt
            authenticationModel.isAuthorized = true;
            var authToken = jwtTokenService.GenerateAuthToken(user);
            authenticationModel.token = authToken.Token;
            authenticationModel.userName = authToken.UserName;
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            string messageSuccess = "Logged in successfully.";
            return await Result<GetTokenByLoginDto>.SuccessAsync(new GetTokenByLoginDto { token = authToken?.Token, isAuthorized = true, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = authToken?.UserName, userID = authToken?.UserID, RefreshToken = newRefreshToken.Token, RefreshTokenExpiration = newRefreshToken.Expires }, messageSuccess);

        }

    }
}
