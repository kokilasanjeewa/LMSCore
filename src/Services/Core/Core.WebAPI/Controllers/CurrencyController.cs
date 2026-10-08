using Asp.Versioning;
using AutoMapper;
using Core.Application.DTOs.Currency;
using Core.Application.DTOs.Currency;
using Core.Domain.Entities;
using Core.Infrastructure.Services;
using Core.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing Currency.
    /// Inherits from the <see cref="GenericController{TEntity, TContext, TGetDto, TCreateDto, TUpdateDto}"/>.
    /// </summary>
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2, Deprecated = true)]
    [ApiExplorerSettings(IgnoreApi = true)] // HIDDEN FROM SWAGGER
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [ApiController]
    public class CurrencyController : GenericController<Currency, ApplicationDbContext, GetCurrencyDto, CreateCurrencyDto, UpdateCurrencyDto>
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly TheNumbersService _theNumbersService;
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance for logging.</param>
        /// <param name="mediator">The mediator instance for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        ///

        public CurrencyController(ApplicationDbContext context, IMapper mapper, ILogger<CurrencyController> logger, IMediator mediator, TheNumbersService theNumbersService)
        : base(context, mapper, theNumbersService)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _theNumbersService = theNumbersService;

        }
        /// <summary>
        /// Retrieves all Currency.
        /// </summary>
        [HttpGet(Name = "GetCurrency")]
        public override async Task<ActionResult<IEnumerable<GetCurrencyDto>>> GetAll()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Retrieves a Currency by ID.
        /// </summary>
        [HttpGet("{id}")]
        public override async Task<ActionResult<GetCurrencyDto>> Get(int id)
        {
            return await base.Get(id);
        }


        /// <summary>
        /// Creates a new Currency.
        /// </summary>
        [HttpPost(Name = "CreateCurrency")]
        public override async Task<ActionResult<GetCurrencyDto>> Create([FromBody] CreateCurrencyDto createDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Create(createDto, new[] { "CurrencyName" });
        }

        /// <summary>
        /// Updates an existing Currency
        [HttpPut]
        public override async Task<ActionResult<GetCurrencyDto>> Update([FromBody] UpdateCurrencyDto updateDto, [FromQuery] string[]? uniquePropertyNames = null)
        {
            return await base.Update(updateDto, new[] { "CurrencyName" });
        }


        /// <summary>
        /// Deletes an Currency by ID.
        /// </summary>
        /// <param name="id">The ID of the Currency to delete.</param>
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
