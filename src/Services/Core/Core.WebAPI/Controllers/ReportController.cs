using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.Report;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using JwtTokenAuthentication.Constants;
using JwtTokenAuthentication.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing Report.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    public class ReportController : GenericController<Report, ApplicationDbContext, GetReportDto, CreateReportDto, UpdateReportDto>
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public ReportController(ApplicationDbContext context, IMapper mapper, ILogger<ReportController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all Report.
        /// </summary>
        [HttpGet(Name = "GetReport")]
        public override async Task<ActionResult<IEnumerable<GetReportDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves a Report by ID.
        /// </summary>
        [HttpGet("ignore/{id}")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        [Obsolete("This endpoint is disabled and should not be used.")]
        public override async Task<ActionResult<GetReportDto>> Get(int id)
        {
            return await base.Get(id);
        }
        /// <summary>
        /// Retrieves an Report by ID.
        /// </summary>
        /// <param name="id">The ID of the Report to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetReportDto>> Get(short id)
        {
            var entity = await _context.Set<Report>().FirstOrDefaultAsync(e => e.ReportSerialID == id); // Replace `Id` with the correct primary key property
            if (entity == null) return NotFound();

            var dto = _mapper.Map<GetReportDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new Report.
        /// </summary>
        [HttpPost(Name = "CreateReport")]
        public override async Task<ActionResult<GetReportDto>> Create([FromBody] CreateReportDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "ReportName" });
        }

        /// <summary>
        /// Updates an existing Report
        [HttpPut]
        public override async Task<ActionResult<GetReportDto>> Update([FromBody] UpdateReportDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "ReportName" });
        }


        /// <summary>
        /// Deletes an Report by ID.
        /// </summary>
        /// <param name="id">The ID of the Report to delete.</param>
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        //[AuthorizeMultiplePermissions(Permissions.Btn_SetReportDelete)]
        [Obsolete("This endpoint is disabled and should not be used.")]
        public override async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            return await base.Delete(id, hardDelete);
        }
    }
}
