using AutoMapper;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Helper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Linq.Dynamic.Core.Tokenizer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Core.Application.Features.Users.Queries.GetTokenByLogin
{

    public record GetTokenByLoginQuery : IRequest<Result<GetTokenByLoginDto>>
    {
        public LoginModel LoginModel { get; }

        public GetTokenByLoginQuery(LoginModel loginModel)
        {
            LoginModel = loginModel;
        }
    }
    internal class GetTokenByLoginQueryHandler : IRequestHandler<GetTokenByLoginQuery, Result<GetTokenByLoginDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository userRepository;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly ITokenService jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;

        public GetTokenByLoginQueryHandler(IMapper mapper, IUserRepository _userRepository, ITokenService _jwtTokenService, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, INotificationService notificationService)
        {
            _mapper = mapper;
            userRepository = _userRepository;
            jwtTokenService = _jwtTokenService;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;

        }

        public async Task<Result<GetTokenByLoginDto>> Handle(GetTokenByLoginQuery query, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByUserName(loginName: query.LoginModel.UserID, cancellationToken);
            if (user == null)
            {
                return await Result<GetTokenByLoginDto>.FailureAsync("The username you entered is incorrect.");
            }
            var lastToken = user.LastToken;
            // Create an instance of the validator
            var validator = new LoginModelValidator(userRepository);

            // Validate the login model
            var validationResult = await validator.ValidateAsync(query.LoginModel);

            if (!validationResult.IsValid)
            {
                // Deny login or log out previous session
                // Convert validation errors to a List<string>
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return await Result<GetTokenByLoginDto>.FailureAsync(errorMessages);
            }

            // Access HttpContext like this
            var httpContext = _httpContextAccessor.HttpContext;
            // var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            var ipAddress = GetClientIpAddress(httpContext);

            // Get the server's machine name (local machine name)
             string machineName = System.Net.Dns.GetHostName();  // Local machine name
            // string machineName = Environment.MachineName;

            //string pc = System.Net.Dns.GetHostName();
            // Check if the device matches the one stored in the database
            if (user.IsLogOut==false && user.LastSessionId != null && (lastToken != null || user.LastIp != ipAddress))
            {
                user.LastSessionId = null;
                user.LastIp = null;
                user.LastToken = null;
                user.IsLogOut = true;
                DateTime? expirationTime = JwtTokenHelper.GetTokenExpiration(lastToken);
                var _invalidateToken = new InvalidateToken
                {
                    UserID = user.UserID,
                    Token = lastToken,
                    ExpirationTime = (DateTime)expirationTime,
                    Active = true,
                    IsDeleted = false
                };
                await _unitOfWork.Repository<InvalidateToken>().AddAsync(_invalidateToken);
                await _unitOfWork.Repository<User>().UpdateAsync(user, user.UserSerialID);
                await _unitOfWork.Save(cancellationToken);
            }
            if (user.LastSessionId != null && (user.LastToken != null || user.LastIp != ipAddress))
            {
                var errorMessage = $"There is new sign-in to your account from {ipAddress} + {machineName}.You have to sign in again to continue.";
                DateTime? expirationTime = JwtTokenHelper.GetTokenExpiration(lastToken);

                var _invalidateToken = new InvalidateToken
                {
                    UserID = user.UserID,
                    Token = lastToken,
                    ExpirationTime = (DateTime)expirationTime,
                    Active = true,
                    IsDeleted = false
                };
                await _unitOfWork.Repository<InvalidateToken>().AddAsync(_invalidateToken);
                await _unitOfWork.Save(cancellationToken);

                // Use NotificationService to send the error message
                await _notificationService.SendNotificationAsync(user.UserID.ToString(), errorMessage);

                // Deny login or log out previous session
                // return await Result<GetTokenByLoginDto>.FailureAsync(errorMessage);
            }
            // Generate a new session ID for this login
            user.LastSessionId = Guid.NewGuid().ToString();
            user.LastIp = ipAddress;
            user.IsLogOut = false;

            var userLoginCompany = await _unitOfWork.Repository<Company>().GetByIdAsync(query.LoginModel.ComSerialID);

            if (user == null || user.IsDeleted)
            {
                return await Result<GetTokenByLoginDto>.FailureAsync("The username you entered is incorrect.");
            }
            if (user.Companies.Count == 0)
            {
                return await Result<GetTokenByLoginDto>.FailureAsync("You are prohibited from accessing the company.");
            }

            #region
            /*if (!user.Companies.Any(c => c.ComSerialID==query.ComSerialID && c.IsDeleted==false))
            {
                return await Result<GetTokenByLoginDto>.FailureAsync("You are prohibited from accessing the company.");
            }*/
            #endregion

            var passwordHash = PasswordHasher.ComputeHash(query.LoginModel.Password, user.PasswdSalt, _pepper, _iteration);
            if (user.PasswdHash != passwordHash)
                return await Result<GetTokenByLoginDto>.FailureAsync("The password you entered is incorrect.");
            var refreshToken = new RefreshToken();
            user.LoginCompanySerialID = query.LoginModel.ComSerialID;
            var loginlog = new LoginLog
            {
                UserSerialID = user.UserSerialID,
                LoginDateTime = DateTime.Now,
                IPAddress = ipAddress,
                MachineName = Environment.MachineName,
                IsTimeout = false,
                IsSystemLogout = false,
                LogoutDateTime = null, // or DateTime.Now if applicable
                TokenExpire=null,
                Active = true,
                IsDeleted = false,
            };
            await _unitOfWork.Repository<LoginLog>().AddAsync(loginlog);
            await _unitOfWork.Save(cancellationToken);
            user.LoginLogSerialID = loginlog.LoginLogSerialID;
            var authToken = jwtTokenService.GenerateAuthToken(user);
            // invalidate expire token
            foreach (var item in user.RefreshTokens.Where(a => a.Active && a.Expires < DateTime.Now))
            {
                var invalidateRefreshToken = user.RefreshTokens.Where(a => a.RTSerialID == item.RTSerialID).FirstOrDefault();
                //Revoke Current Refresh Token
                invalidateRefreshToken.Revoked = DateTime.Now;
                invalidateRefreshToken.Active = false;
                invalidateRefreshToken.IsDeleted = true;
                await _unitOfWork.Repository<RefreshToken>().UpdateAsync(invalidateRefreshToken, invalidateRefreshToken.RTSerialID);
                await _unitOfWork.Save(cancellationToken);
            };
            // verify token validate
            if (user.RefreshTokens.Any(a => a.Active && a.Expires > DateTime.Now))
            {
                var activeRefreshToken = user.RefreshTokens.Where(a => a.Active == true).FirstOrDefault();
                refreshToken.ExistsRefreshToken = activeRefreshToken.Token;
                refreshToken.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else

            {
                // generate refresh token
                var refreshTokenCreate = jwtTokenService.CreateRefreshToken();
                refreshToken.ExistsRefreshToken = refreshTokenCreate.Token;
                refreshToken.RefreshTokenExpiration = refreshTokenCreate.Expires;
                refreshToken.Token = refreshTokenCreate.Token;
                refreshToken.Expires = refreshTokenCreate.Expires;
                refreshToken.Active = refreshToken.IsActive;
                
                try
                {
                    // save refresh token
                    user.LastToken = authToken?.Token;
                    user.IsLogOut = false;
                    user.RefreshTokens.Add(refreshToken);
                    await _unitOfWork.Repository<User>().UpdateAsync(user, user.UserSerialID);
                    user.AddDomainEvent(new UserCreatedEvent(user));
                    await _unitOfWork.Save(cancellationToken);

                    string messageSuccess = "Logged in successfully.";
                    return await Result<GetTokenByLoginDto>.SuccessAsync(new GetTokenByLoginDto { token = authToken?.Token, isAuthorized = true, imageCompanyUrl = userLoginCompany.CompanyLogoUrl, iconCompanyUrl = userLoginCompany.SmallComLogoUrl, imageProfileUrl = "", userName = authToken?.UserName, userID = authToken?.UserID,eESerialID=user.EESerialID, comSerialID= query.LoginModel.ComSerialID, RefreshToken = refreshToken.ExistsRefreshToken, RefreshTokenExpiration = refreshToken.RefreshTokenExpiration }, messageSuccess);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return await Result<GetTokenByLoginDto>.FailureAsync(new GetTokenByLoginDto { token = authToken?.Token, isAuthorized = false, imageCompanyUrl = userLoginCompany.CompanyLogoUrl, iconCompanyUrl = userLoginCompany.SmallComLogoUrl, imageProfileUrl = "", userName = authToken?.UserName, userID = authToken?.UserID, eESerialID = user.EESerialID }, ex.Message);

                }

            }
            string message = "Logged in successfully.";

            return await Result<GetTokenByLoginDto>.SuccessAsync(new GetTokenByLoginDto { token = authToken?.Token, isAuthorized = true, imageCompanyUrl = userLoginCompany.CompanyLogoUrl, iconCompanyUrl = userLoginCompany.SmallComLogoUrl, imageProfileUrl = "", userName = authToken?.UserName, userID = authToken?.UserID, eESerialID = user.EESerialID, comSerialID = query.LoginModel.ComSerialID, RefreshToken = refreshToken.ExistsRefreshToken, RefreshTokenExpiration = refreshToken.RefreshTokenExpiration }, message);
        }
        private string GetClientIpAddress(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;

            if (ipAddress == null)
            {
                return "IP not found";
            }

            // Check if it's an IPv4-mapped IPv6 address
            if (ipAddress.IsIPv4MappedToIPv6)
            {
                // Convert to IPv4 format
                return ipAddress.MapToIPv4().ToString();
            }

            // Otherwise, return the IP address as-is
            return ipAddress.ToString();
        }
    }

}
