using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.Module;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing Module.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [ApiController]
    public class ModuleController : GenericController<AppModule, ApplicationDbContext, GetModuleDto, CreateModuleDto, UpdateModuleDto>
    {
        private readonly ILogger<ModuleController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public ModuleController(ApplicationDbContext context, IMapper mapper, ILogger<ModuleController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all Module.
        /// </summary>
        [HttpGet(Name = "GetModule")]
        public override async Task<ActionResult<IEnumerable<GetModuleDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves a Module by ID.
        /// </summary>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetModuleDto>> Get(int id)
        {
            return await base.Get(id);
        }


        /// <summary>
        /// Creates a new Module.
        /// </summary>
        [HttpPost(Name = "CreateModule")]
        public override async Task<ActionResult<GetModuleDto>> Create([FromBody] CreateModuleDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "ModuleName" });
        }

        /// <summary>
        /// Updates an existing Module
        [HttpPut]
        public override async Task<ActionResult<GetModuleDto>> Update([FromBody] UpdateModuleDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "ModuleName" });
        }


        /// <summary>
        /// Deletes an Module by ID.
        /// </summary>
        /// <param name="id">The ID of the Module to delete.</param>
        [HttpDelete("{id}")]
        //  [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        //   [AuthorizeMultiplePermissions(Permissions.Btn_SetFpScannersDelete)]
        //   [Obsolete("This endpoint is disabled and should not be used.")]
        public override async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            return await base.Delete(id, hardDelete);
        }
    }
}