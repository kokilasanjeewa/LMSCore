using Core.Application.Features.Users.Queries.GetTokenByLogin;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Request;
using MediatR;
using Core.Application.Features.Users.Commands.CreateUser;
using JwtTokenAuthentication.Constants;
using Core.Application.Features.Users.Commands.LogoutUser;
using Core.Application.Features.Users.Queries.GetRefreshToken;
using Core.Application.DTOs.User;
using Core.Application.Features.Users.Queries.GetUsersWithPagination;
using Asp.Versioning;
using Core.Application.Features.Users.Queries.GetUser;
using JwtTokenAuthentication.Permission;
using Core.Application.Features.Users.Commands.UpdateUser;
using AutoMapper;
using Core.Application.Features.Users.Commands.ChangePassword;
using Core.Application.Features.Users.Commands.CloneUser;

namespace Core.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _jwtTokenService;
        private readonly IMapper _mapper;
        public AuthController(ILogger<AuthController> logger, IUserRepository userRepository, ITokenService jwtTokenService, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _mapper = mapper;
        }

        /// <summary>
        /// User Registration  a specific user .
        /// </summary> 
        /// <param name="register"></param> 
        ///  
        [MapToApiVersion(1)]
        [HttpPost("register")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]

        public async Task<ActionResult<Result<int>>> Register([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            //  if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return await _mediator.Send(command, cancellationToken);
        }
        /// <summary>
        /// Update User Registration  a specific user.
        /// </summary> 
        /// <param name="update-register"></param> 
        ///  
        [MapToApiVersion(1)]
        [HttpPut("update-register")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserEdit)]

        public async Task<ActionResult<Result<int>>> UpdateRegister([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);

        }
        /// <summary>
        /// User Registration  a specific username and password.
        /// </summary> 
        /// <param name="register"></param> 
        ///  
        [MapToApiVersion(1)]
        [HttpPost("search-user")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSearch)]
        public async Task<ActionResult<Result<SearchUserDto>>> GetSearchUser([FromBody] GetUserQuery getUserQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(getUserQuery, cancellationToken);
        }
        /// <summary>
        /// Change the user's password.
        /// </summary>
        /// <param name="changepassword">The change password command.</param> 
        [MapToApiVersion(1)]
        [HttpPost("changepassword")]
        public async Task<ActionResult<Result<int>>> ChangePassword([FromBody] ChangePasswordCommand changePassword, CancellationToken cancellationToken)
        {
            return await _mediator.Send(changePassword, cancellationToken);
        }

        /// <summary>
        /// User Login with a specific username and password.
        /// </summary>
        /// <param name="loginModel">The login model containing user credentials.</param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Result<GetTokenByLoginDto>>> Login([FromForm] LoginModel loginModel, CancellationToken cancellationToken)
        {
            // Pass the LoginModel directly to the GetTokenByLoginQuery
            var query = new GetTokenByLoginQuery(loginModel);
            return await _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Logout the user.
        /// </summary>
        /// <param name="command">The logout command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult<Result<GetTokenByLoginDto>>> Logout([FromForm] LogoutUserCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Refresh the user token.
        /// </summary>
        /// <param name="refreshTokenModel">The refresh token model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<Result<GetTokenByLoginDto>>> RefreshToken([FromForm] RefreshTokenModel refreshTokenModel, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetRefreshTokenQuery(refreshTokenModel.token), cancellationToken);
        }
        /// <summary>
        /// Get a user by search query.
        /// </summary>
        /// <param name="getUserQuery">The query to search for a user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<PaginatedResult<GetUsersWithPaginationDto>>>> GetUsersWithPagination([FromQuery] GetUsersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }
        /// <summary>
        /// Get a users by search query.
        /// </summary>
        /// <param name="getall">The query to search for a users.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet("getall")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetUsersAsync(cancellationToken);
                var usersDto = _mapper.Map<List<UserDto>>(users);

                if (users == null || !users.Any())
                {
                    return NotFound("Users were unavailable for the specified company.");
                }

                return Ok(usersDto);
            }
            catch (Exception)
            {
                // Log the exception as needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving users.");
            }
        }

        /// <summary>
        /// User Registration  a specific user .
        /// </summary> 
        /// <param name="register"></param> 
        ///  
        [MapToApiVersion(1)]
        [HttpPost("clone-user")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserClone)]

        public async Task<ActionResult<Result<int>>> CloneUser([FromBody] CloneUserCommand command, CancellationToken cancellationToken)
        {
            //  if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return await _mediator.Send(command, cancellationToken);
        }
    }
}

//https://blog.devgenius.io/applying-jwt-access-tokens-and-refresh-tokens-in-asp-net-core-web-api-fc757c9191b9