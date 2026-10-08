using Core.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Request;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Core.Application.DTOs.Group;
using Asp.Versioning;
using Core.Shared;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using Core.Application.Features.Group.Queries;
using Core.Application.Features.Group.Commands.CreateGroup;
using Core.Application.Features.Group.Commands.CreateGroupPermission;
using Core.Application.Features.Group.Commands.UpdateGroupMenu;
using Core.Application.DTOs.User;
using Core.Application.Features.Users.Queries.GetUser;
using Core.Application.Features.Group.Queries.GetGroup;

namespace Core.WebAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IMediator _mediator;
        private readonly IGroupRepository _GroupRepository;
        public GroupsController(ILogger<GroupsController> logger, IGroupRepository GroupRepository, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _GroupRepository = GroupRepository;
        }

        /// <summary>
        /// Get All Groups
        /// </summary> 
        /// <param name=""></param> 
        [MapToApiVersion(1)]
        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GroupDto>>> GetGroupsAsync([FromBody] EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            return await _GroupRepository.GetGroupsAsync(entityStatus, cancellationToken);
        }
        
        /// <summary>
        /// Create a Group
        /// </summary> 
        /// <param name="create-group"></param> 
        [MapToApiVersion(1)]
        [HttpPost("create-group")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<ActionResult<Result<int>>> PostGroupAsync([FromBody] CreateGroupCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Create a Group Menu Permission
        /// </summary> 
        /// <param name="create-group-menu"></param> 
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [MapToApiVersion(1)]
        [HttpPost("create-group-menu")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<ActionResult<Result<int>>> PostGroupMenuAsync([FromBody] CreateGroupMenuCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        
        /// <summary>
        /// Update a Group Menu Permission
        /// </summary> 
        /// <param name="create-group-menu"></param> 
        [MapToApiVersion(1)]
        [HttpPut("update-group-menu")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserEdit)]
        public async Task<ActionResult<Result<int>>> PutGroupMenuAsync([FromBody] UpdateGroupMenuCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        /// <summary>
        /// Update a Group Menu Permission
        /// </summary> 
        /// <param name="search-group-menu"></param> 
        [MapToApiVersion(1)]
        [HttpPost("search-group-menu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSearch)]
        public async Task<ActionResult<Result<SearchGroupDto>>> PostSearchGroupMenuAsync([FromBody] GetGroupQuery command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        /// <summary>
        /// Get All Groups with pagination.
        /// </summary>
        /// <param name="getGroups">The parameter for getting groups.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<PaginatedResult<GetGroupsWithPaginationDto>>>> GetGroupsWithPagination([FromQuery] GetGroupsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }
    }
}

