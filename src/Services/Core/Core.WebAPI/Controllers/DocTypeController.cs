using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.DocType;
using Core.Application.Features.DocType.Command;
using Core.Application.Features.DocType.Queries;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing DocType.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [ApiController]
    public class DocTypeController : GenericController<DocType, ApplicationDbContext, GetDocTypeDto, CreateDocTypeDto, UpdateDocTypeDto>
    {
        private readonly ILogger<DocTypeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="DocTypeController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public DocTypeController(ApplicationDbContext context, IMapper mapper, ILogger<DocTypeController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }


        /// <summary>
        /// Retrieves all DocType.
        /// </summary>
        [HttpGet(Name = "GetDocType")]
        public override async Task<ActionResult<IEnumerable<GetDocTypeDto>>> GetAll()
        {
            return await base.GetAll();
        }

         /// <summary>
        /// Retrieves an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetDocTypeDto>> Get(int id)
        {
            var entity = await _context.Set<FilePaths>()
                .Include(e => e.DocType)
                .FirstOrDefaultAsync(e => e.DocTypeSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetDocTypeDto>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Creates a new DocType.
        /// </summary>
        [Obsolete("This endpoint is disabled and should not be used.")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [HttpPost(Name = "CreateDocType")]
        public override async Task<ActionResult<GetDocTypeDto>> Create([FromBody] CreateDocTypeDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "DocumentType" });
        }

        /// <summary>
        /// Updates an existing DocType
        [Obsolete("This endpoint is disabled and should not be used.")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [HttpPut]
        public override async Task<ActionResult<GetDocTypeDto>> Update([FromBody] UpdateDocTypeDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "DocumentType" });
        }


        /// <summary>
        /// Deletes an DocType by ID.
        /// </summary>
        /// <param name="id">The ID of the DocType to delete.</param>
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            return await base.Delete(id, hardDelete);
        }
        /// <summary>
        /// Create a DocType
        /// </summary> 
        /// <param name="create"></param> 
        [MapToApiVersion(1)]
        [HttpPost("create")]
        public async Task<ActionResult<Result<int>>> CreateDocTypeAsync([FromBody] CreateDocTypeCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        /// <summary>
        /// Create a DocType update
        /// </summary> 
        /// <param name="update"></param> 
        [MapToApiVersion(1)]
        [HttpPut("update")]
        public async Task<ActionResult<Result<int>>> UpdateDocTypeAsync([FromBody] UpdateDocTypeCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Get All Doctype with pagination.
        /// </summary>
        /// <param name="getAssets">The parameter for getting Doctypes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<Result<PaginatedResult<GetDocTypePaginationDto>>>> GetAssetsWithPagination([FromQuery] GetDocTypesWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Example of a custom action that demonstrates using the injected dependencies.
        /// </summary>
        /// <param name="delete">The command to a doc type item.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPatch("delete")]
        public async Task<ActionResult<Result<int>>> SoftDelete([FromBody] DeleteDocTypeCommand delete, CancellationToken cancellationToken)
        {
            // Use the mediator to send the command (you may need to adjust this based on your actual command structure)
            return await _mediator.Send(delete, cancellationToken);
            // Return the result
        }
    }
}
