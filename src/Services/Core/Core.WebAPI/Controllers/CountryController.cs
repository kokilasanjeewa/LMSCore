using Asp.Versioning;
using AutoMapper;
using Core.Application.Common.Dtos;
using Core.Application.DTOs.City;
using Core.Application.DTOs.Country;
using Core.Application.DTOs.Country;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.WebAPI.Controllers
{

    /// <summary>
    /// Controller for managing Country.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1)]
    [ApiVersion(2, Deprecated = true)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    public class CountryController : GenericController<Country, ApplicationDbContext, GetCountryDto, CreateCountryDto, UpdateCountryDto>
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public CountryController(ApplicationDbContext context, IMapper mapper, ILogger<CountryController> logger, IMediator mediator, TheNumbersService theNumbersService, IUnitOfWork unitOfWork)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;
            _unitOfWork = unitOfWork;

        }
        /// <summary>
        /// Retrieves all Country.
        /// </summary>
        [HttpGet(Name = "GetCountry")]
        public override async Task<ActionResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            return await base.GetAll();
        }
        [HttpGet("form-options")]
        //[AuthorizeMultiplePermissions(Permissions.Btn_ViewPersonalDetails,Permissions.Btn_ViewOfficialDetails)]
        [AllowAnonymous]
        public async Task<ActionResult<FormOptionsDto>> GetFormOptions()
        {
            var countries = await _unitOfWork.Repository<Country>()
                .Entities
                .Select(x => new FormOptionDto
                {
                    Id = x.CntrySerialID,
                    Label = x.Name,
                    Value = x.CntrySerialID

                })
            .ToListAsync();
            var Currencies = await _unitOfWork.Repository<Country>()
                .Entities
                .Select(x => new FormOptionDto
                {
                    Id = x.CntrySerialID,
                    Label = x.Currency,
                    Value = x.CntrySerialID

                })
            .ToListAsync();
            var OfficeLocations = await _unitOfWork.Repository<Country>()
                .Entities
                .Select(x => new FormOptionDto
                {
                    Id = x.CntrySerialID,
                    Label = x.Name,
                    Value = x.CntrySerialID

                })
            .ToListAsync();

            var result = new FormOptionsDto
            {
                Countries = countries,
                OfficeLocations = OfficeLocations,
                Currencies = Currencies,
            };

            return Ok(result);
        }
        /// <summary>
        /// Retrieves a Country by ID.
        /// </summary>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetCountryDto>> Get(int id)
        {
            return await base.Get(id);
        }


        /// <summary>
        /// Creates a new Country.
        /// </summary>
        [HttpPost(Name = "CreateCountry")]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetCountryDto>> Create([FromBody] CreateCountryDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "CountryName" });
        }

        /// <summary>
        /// Updates an existing Country
        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = true)] // Optional: hides this from Swagger
        public override async Task<ActionResult<GetCountryDto>> Update([FromBody] UpdateCountryDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "CountryName" });
        }


        /// <summary>
        /// Deletes an Country by ID.
        /// </summary>
        /// <param name="id">The ID of the Country to delete.</param>
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
