using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.City;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Core.WebAPI.Controllers
{

    /// <summary>
    /// Controller for managing City.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    public class CityController : GenericController<City, ApplicationDbContext, GetCityDto, CreateCityDto, UpdateCityDto>
    {
        private readonly ILogger<CityController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public CityController(ApplicationDbContext context, IMapper mapper, ILogger<CityController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all City.
        /// </summary>
        [HttpGet(Name = "GetCity")]
        public override async Task<ActionResult<IEnumerable<GetCityDto>>> GetAll()
        {
            return await base.GetAll();
        }
                  /// <summary>
        /// Retrieves a City by CountrySerialId and StateSerialID.
        /// </summary>
        [HttpGet("by-country/{countrySerialId:long}/state/{stateSerialId:long}", Name = "GetCityByCountryAndState")]
        public async Task<ActionResult<List<GetCityDto>>> GetCityByCountryAndState(long countrySerialId,long stateSerialId)
        {
            var state = await _context.City.AsNoTracking().Where(x => x.CntrySerialID == countrySerialId && x.StateSerialID == stateSerialId).ToListAsync();

            if (state == null)  return NotFound();

            var stateDto = _mapper.Map<List<GetCityDto>>(state);

            return Ok(stateDto);
        }
        /// <summary>
        /// Retrieves a City by ID.
        /// </summary>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetCityDto>> Get(int id)
        {
            return await base.Get(id);
        }


        /// <summary>
        /// Creates a new City.
        /// </summary>
        [HttpPost(Name = "CreateCity")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetCityDto>> Create([FromBody] CreateCityDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "CityName" });
        }

        /// <summary>
        /// Updates an existing City
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetCityDto>> Update([FromBody] UpdateCityDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "CityName" });
        }


        /// <summary>
        /// Deletes an City by ID.
        /// </summary>
        /// <param name="id">The ID of the City to delete.</param>
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
