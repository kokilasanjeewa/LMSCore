using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.Users.Queries.GetMenuPermissionDynamicaly;
using Core.Application.DTOs.User;
using Asp.Versioning;
using Core.Application.Features.Users.Queries.GetBtnRptByMnuSeridsDynamicQuery;
namespace Core.WebAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [Authorize]
    public class PermController : ControllerBase
    {

        private readonly ILogger<PermController> _logger;
        private readonly IMediator _mediator;

        public PermController(ILogger<PermController> logger, IUserRepository _userRepository, ITokenService _jwtTokenService, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        ///// <summary>
        ///// Menu Permission - Get all menu permissions.
        ///// </summary> 
        ///// <param name=""></param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("")]
        public async Task<List<GetMenuPermissionDynamicDto>> GetMenuPermisionList([FromBody] GetMenuPermissionDynamicQuery permissionDynamicQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(permissionDynamicQuery, cancellationToken);
        }

        /// <summary>
        /// Menu Permission - Get all menu permissions.
        /// </summary> 
        /// <param name=""></param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("get-usermenu-list")]
        public async Task<List<GetMenuPermissionDynamicDto>> GetUserMenuPermisionList([FromBody] GetUserMenuPermissionDynamicQuery permissionDynamicQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(permissionDynamicQuery, cancellationToken);
        }
        /// <summary>
        /// Menu Permission - Get all menu permissions.
        /// </summary> 
        /// <param name=""></param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("get-groupmenu-list")]
        public async Task<List<GetMenuPermissionDynamicDto>> GetGroupMenuPermisionList([FromBody] GetGroupMenuPermissionDynamicQuery permissionDynamicQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(permissionDynamicQuery, cancellationToken);
        }
        /// <summary>
        /// Button and Report  - Get all button and report.
        /// </summary> 
        /// <param name=""></param>
        [MapToApiVersion(1)]
        [AllowAnonymous]
        [HttpPost("getbtnrpt-by-mnuserids")]
        public async Task<Result<List<GetBtnRptByMnuSeridsDynamicDto>>> GetBtnRptByMnuSerids([FromBody] GetBtnRptByMnuSeridsDynamicQuery permissionDynamicQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(permissionDynamicQuery, cancellationToken);
        }
    }
}



