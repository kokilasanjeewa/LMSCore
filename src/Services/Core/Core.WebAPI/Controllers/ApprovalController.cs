using Asp.Versioning;
using Core.Application.DTOs.Approval;
using Core.Application.Features.Approval.Command;
using Core.Application.Features.Approval.Queries.GetWorkFlow;
using Core.Application.Features.Approval.Queries.GetWorkFlowsWithPagination;
using Core.Application.Interfaces.Repositories;
using Core.Persistence.Repositories;
using Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [Authorize]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalService _service;
        private readonly IApprovalAdminService _workflowservice;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovalController(IApprovalService service, IApprovalAdminService workflowservice, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _service = service;
            _workflowservice = workflowservice;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("submit")]
        [AllowAnonymous]
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<IActionResult> Submit([FromBody] ApprovalSubmitDto dto)
        {
            // 2️⃣ Lookup EntityType ID
            var entityType = await _unitOfWork.Repository<EntityType>().Entities
                .Where(e => e.EntityName == dto.EntityType)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var workflow = await _service.GetWorkflowAsync(entityType.EntityTypeID,dto.CompanyID,dto.PlantID,dto.EntityID); // optional for amount-based logic

            if (workflow == null)
                return BadRequest("No approval workflow configured.");
            var result = await _service.CreateApprovalRequestAsync(
                entityType.EntityTypeID,
                dto.EntityID,
                workflow.ApprovalWorkflowID,
                dto.UserID,
                dto.CompanyID,
                dto.PlantID);

            return Ok(result);
        }
        [HttpPost("multi-action")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<List<ApproveActionResultDto>>>> MultiAction([FromBody] ExecuteApprovalActionCommand command,CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        [HttpPost("action")]
        [AllowAnonymous]
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<ActionResult<Result<int>>> Action([FromBody] ApprovalActionDto command,CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
  
        /// <summary>
        /// Create a new approval workflow.
        /// </summary>
        /// <param name="flowCommand">The query to save for a work flows.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpPost("workflow")]
        [AllowAnonymous]

        //[AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<int>>> CreateWorkflow([FromBody] CreateWorkFlowCommand flowCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(flowCommand, cancellationToken);
        }

        /// <summary>
        /// Update a new approval workflow.
        /// </summary>
        /// <param name="flowCommand">The query to save for a work flows.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpPut("workflow")]
        [AllowAnonymous]

        //[AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<int>>> UpdateWorkflow([FromBody] UpdateWorkFlowCommand flowCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(flowCommand, cancellationToken);
        }

        /// <summary>
        /// Create a new approval workflow
        /// </summary>
        [HttpPost("createworkflow")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<IActionResult> CreateWorkflow([FromBody] ApprovalWorkflowCreateDto dto)
        {
            var workflow = await _workflowservice.CreateWorkflowAsync(dto);
            return Ok(workflow);
        }

        /// <summary>
        /// Add a step to an existing workflow
        /// </summary>
        [HttpPost("step")]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<IActionResult> AddStep([FromBody] ApprovalStepCreateDto dto)
        {
            var step = await _workflowservice.AddStepAsync(dto);
            return Ok(step);
        }
        /// <summary>
        /// Get all entities
        /// </summary>
        [HttpGet("entity")]
        [AllowAnonymous]
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _workflowservice.GetAllEntityAsync();
            return Ok(data);
        }
        /// <summary>
        /// Get work flows query.
        /// </summary>
        /// <param name="getWorkFlowsQuery">The query to search for a work flows.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        [AllowAnonymous]
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>>> GetWorkFlowsWithPagination([FromQuery] GetWorkFlowsWithPaginationQuery getWorkFlowsQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(getWorkFlowsQuery, cancellationToken);
        }
        /// <summary>
        /// Get work flows query.
        /// </summary>
        /// <param name="getWorkFlowQuery">The query to search for a work flows.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("workflow")]
        [AllowAnonymous]
        //[AuthorizeMultiplePermissions(Permissions.Btn_UserView)]
        public async Task<ActionResult<Result<GetWorkFlowWithDto>>> GetWorkFlowWithPagination([FromQuery] GetWorkFlowWithQuery getWorkFlowQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(getWorkFlowQuery, cancellationToken);
        }
    }

}
