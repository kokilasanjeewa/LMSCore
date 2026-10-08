using AutoMapper;
using MediatR;
using FluentValidation;
using Core.Domain.Entities;
using Core.Application.Common.Mappings;
using Core.Shared;
using Core.Application.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;
using Core.Application.Helper;
using Core.Application.Features.Users.Commands.CreateUser;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Application.Features.Users.Queries.GetTokenByLogin;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Data.Entity;

namespace Core.Application.Features.Users.Commands.LogoutUser
{
    public record LogoutUserCommand : IRequest<Result<GetTokenByLoginDto>>, IMapFrom<User>
    {
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]*$", ErrorMessage = "The userid must begin with a capital letter and contain no spaces.")]
        public string? UserID { get; set; }
        [Required]
        public string? Token { get; set; }

        /// <summary>
        /// Token expiration from the client (e.g. "05/18/2026, 06:00:00 PM") or derived from the JWT when omitted.
        /// </summary>
        public string? ExpirationTime { get; set; }

        public bool IsTimeout { get; set; }=false;
        public bool IsSystemLogout { get; set; } = false;
       
    }

    internal class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Result<GetTokenByLoginDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;

        public LogoutUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _userRepository = userRepository;
        }

        public async Task<Result<GetTokenByLoginDto>> Handle(LogoutUserCommand command, CancellationToken cancellationToken)
        {
            if (!JwtTokenHelper.TryResolveExpirationTime(command.Token, command.ExpirationTime, out var expirationTime))
            {
                return await Result<GetTokenByLoginDto>.FailureAsync(
                    new GetTokenByLoginDto
                    {
                        token = command.Token,
                        isAuthorized = false,
                        userID = command?.UserID
                    },
                    "The expiration time is missing or invalid.");
            }

            LogoutUserCommandValidator validator = new LogoutUserCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(command);

            var user = await _userRepository.GetUserByUserName(loginName: command.UserID, cancellationToken);
                user.LastSessionId = null;
                user.LastIp = null;
                user.LastToken = null;
                user.IsLogOut = true;
            // _validator.ValidateAndThrow(command);
            Int64 loginLogSerialID = 0;
            var token = command.Token;
            if (token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.ToString().Trim('"'));
                loginLogSerialID = Convert.ToInt32(jwt.Claims.First(c => c.Type == "loginLogSerialID").Value);
            }
            var loginLog = await _unitOfWork.Repository<LoginLog>().GetByIdAsync(loginLogSerialID);
            if (validationResult.IsValid)
            {
                try
                {
                    var _invalidateToken = new InvalidateToken { 
                        UserID = command.UserID,
                        Token = command.Token,
                        ExpirationTime = expirationTime,
                        Active=true,
                        IsDeleted=false
                    };

                    await _unitOfWork.Repository<InvalidateToken>().AddAsync(_invalidateToken);
                    _invalidateToken.AddDomainEvent(new UserLogoutEvent(_invalidateToken));
                    await _unitOfWork.Repository<User>().UpdateAsync(user, user.UserSerialID);
                    user.AddDomainEvent(new UserCreatedEvent(user));
                    loginLog.IsSystemLogout = command.IsSystemLogout;
                    if (command.IsSystemLogout)
                    {
                        loginLog.LogoutDateTime = DateTime.Now;
                    }
                    loginLog.IsTimeout = command.IsTimeout;
                    if(command.IsTimeout)
                    {
                        loginLog.TokenExpire = DateTime.Now;
                    }
                    await _unitOfWork.Repository<LoginLog>().UpdateAsync(loginLog, loginLogSerialID);
                    await _unitOfWork.Save(cancellationToken);
                    string messageSuccess = "Logout successfully.";
                    return await Result<GetTokenByLoginDto>.SuccessAsync(new GetTokenByLoginDto { token = command.Token, isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = "", userID = command?.UserID, RefreshToken = "", RefreshTokenExpiration = DateTime.MinValue, Expires= Convert.ToDecimal(_invalidateToken.ExpirationTime.Subtract(DateTime.Now).TotalMilliseconds) }, messageSuccess);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = command.Token, isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl = "", imageProfileUrl = "", userName = "", userID = command?.UserID, RefreshToken = "", RefreshTokenExpiration = DateTime.MinValue, Expires = Convert.ToDecimal(expirationTime.Subtract(DateTime.Now).TotalMilliseconds) }, ex.Message);

                }
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = command.Token, isAuthorized = false, imageCompanyUrl = "", iconCompanyUrl="", imageProfileUrl = "", userName = "", userID = command?.UserID, RefreshToken = "", RefreshTokenExpiration = DateTime.MinValue, Expires = Convert.ToDecimal(expirationTime.Subtract(DateTime.Now).TotalMilliseconds) }, errors);
            }

        }
    }
}
