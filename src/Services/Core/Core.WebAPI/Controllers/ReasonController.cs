using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Core.Application.DTOs.Reason;
using Core.Infrastructure.Services;
using Core.Application.DTOs.DocType;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing Reasons.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [ApiController]
    public class ReasonController : GenericController<Reason, ApplicationDbContext, GetReasonsDto, CreateReasonDto, UpdateReasonDto>
    {
        private readonly ILogger<ReasonController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;

        public ReasonController(ApplicationDbContext context, ILogger<ReasonController> logger, IMediator mediator, IMapper mapper, TheNumbersService theNumbersService)
            : base(context, mapper, theNumbersService)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _theNumbersService = theNumbersService;
        }

        /// <summary>
        /// Retrieves all reasons.
        /// </summary>
        [HttpGet(Name = "GetReasons")]
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetViewReason)]
        public override async Task<ActionResult<IEnumerable<GetReasonsDto>>> GetAll()
        {
            var entity = await _context.Set<Reason>()
                                .Include(e => e.Module)
                                .ToListAsync(); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<IEnumerable<GetReasonsDto>>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Retrieves an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetReasonsDto>> Get(int id)
        {
            var entity = await _context.Set<Reason>()
                .Include(e => e.Module)
                .FirstOrDefaultAsync(e => e.ReasonSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetReasonsDto>(entity);
            return Ok(dto);
        }
        /// <summary>
        /// Creates a new reason.
        /// </summary>
        [HttpPost(Name = "CreateReason")]
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetSaveReason)]
        public override async Task<ActionResult<GetReasonsDto>> Create([FromBody] CreateReasonDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto);
        }

        /// <summary>
        /// Updates an existing reason.
        /// </summary>
        [HttpPut]
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetEditReason)]
        public override async Task<ActionResult<GetReasonsDto>> Update([FromBody] UpdateReasonDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "ReasonText" });
        }

        /// <summary>
        /// Deletes a reason by ID.
        /// </summary>
        [HttpDelete("{id}")]
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetDeleteReason)]
        public override async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            return await base.Delete(id, hardDelete);
        }
    }
}
