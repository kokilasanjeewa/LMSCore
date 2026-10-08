using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Asp.Versioning;
using Core.Shared;
using AutoMapper;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using Core.Domain.Entities;
using Core.Application.DTOS.CostCenter;
using Core.Application.Features.CostCenter.Command;
using JwtTokenAuthentication.Permission;
using JwtTokenAuthentication.Constants;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing Bank.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [ApiController]
    [Authorize]

    public class CostCenterController : GenericController<CostCenter, ApplicationDbContext, GetCostCenterDto, CreateCostCenterDto, UpdateCostCenterDto>
    {
        private readonly ILogger<CostCenterController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// 

        public CostCenterController(ApplicationDbContext context, IMapper mapper, ILogger<CostCenterController> logger, IMediator mediator, TheNumbersService theNumbersService)
         : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }

        /// <summary>
        /// Retrieves all Bank.
        /// </summary>
        [HttpGet(Name = "GetCostCenters")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCostCenter)]
        public override async Task<ActionResult<IEnumerable<GetCostCenterDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves a Bank by ID.
        /// </summary>
        [HttpGet("ignore/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCostCenter)]
        public override async Task<ActionResult<GetCostCenterDto>> Get(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Retrieves a Bank by ID.
        /// </summary>
        [HttpGet("{id}")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetViewCostCenter)]
        public async Task<ActionResult<GetCostCenterDto>> GetById(byte id)
        {
            var ethncty = await _context.CostCenter.FindAsync(id);

            return _mapper.Map<GetCostCenterDto>(ethncty);
        }

        /// <summary>
        /// Creates a new Bank.
        /// </summary>
        [HttpPost(Name = "CreateCostCenter")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveCostCenter)]
        public override async Task<ActionResult<GetCostCenterDto>> Create([FromBody] CreateCostCenterDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "CostCenterName" });
        }

        /// <summary>
        /// Updates an existing Bank
        [HttpPut]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetSaveCostCenter)]

        public override async Task<ActionResult<GetCostCenterDto>> Update([FromBody] UpdateCostCenterDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "CostCenterName" });
        }

        /// <summary>
        /// Example of a custom action that demonstrates using the injected dependencies.
        /// </summary>
        /// <param name="delete">The command to an asset item.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPatch("delete")]
        [AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteCostCenter)]
        public async Task<ActionResult<Result<int>>> SoftDelete([FromBody] DeleteCostCenterCommand delete, CancellationToken cancellationToken)
        {
            // Use the mediator to send the command (you may need to adjust this based on your actual command structure)
            return await _mediator.Send(delete, cancellationToken);
            // Return the result
        }
    }
}

