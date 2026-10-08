using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.State;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{

    /// <summary>
    /// Controller for managing State.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    public class StateController : GenericController<State, ApplicationDbContext, GetStateDto, CreateStateDto, UpdateStateDto>
    {
        private readonly ILogger<StateController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="StateController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public StateController(ApplicationDbContext context, IMapper mapper, ILogger<StateController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all State.
        /// </summary>
        [HttpGet(Name = "GetState")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger

        public override async Task<ActionResult<IEnumerable<GetStateDto>>> GetAll()
        {
            return await base.GetAll();
        }
        /// <summary>
        /// Retrieves all States by CountrySerialId.
        /// </summary>
        [HttpGet("by-country/{countrySerialId:long}", Name = "GetStateByCountry")]
        public async Task<ActionResult<IEnumerable<GetStateDto>>> GetStatesByCountry(long countrySerialId)
        {
            var states = await _context.State.Where(x => x.CntrySerialID == countrySerialId).AsNoTracking().ToListAsync();

            var stateDtos = _mapper.Map<List<GetStateDto>>(states);

            return Ok(stateDtos);
        }
        /// <summary>
        /// Retrieves a State by ID.
        /// </summary>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetStateDto>> Get(int id)
        {
            return await base.Get(id);
        }


        /// <summary>
        /// Creates a new State.
        /// </summary>
        [HttpPost(Name = "CreateState")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetStateDto>> Create([FromBody] CreateStateDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "StateName" });
        }

        /// <summary>
        /// Updates an existing State
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetStateDto>> Update([FromBody] UpdateStateDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "StateName" });
        }


        /// <summary>
        /// Deletes an State by ID.
        /// </summary>
        /// <param name="id">The ID of the State to delete.</param>
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        //   [AuthorizeMultiplePermissions(Permissions.Btn_SetFpScannersDelete)]
        //   [Obsolete("This endpoint is disabled and should not be used.")]
        public override async Task<IActionResult> Delete(int id, [FromQuery] bool hardDelete = false)
        {
            return await base.Delete(id, hardDelete);
        }
    }
}
