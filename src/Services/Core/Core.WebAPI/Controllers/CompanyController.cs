using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Request;
using MediatR;
using JwtTokenAuthentication.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Application.Features.Assets.Queries;
using Core.Application.DTOs.Asset;
using Asp.Versioning;
using JwtTokenAuthentication.Permission;
using Core.Application.DTOs.User;
using Core.Application.DTOs.Company;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Core.Application.Features.Companies.Queries.GetUserCompanies;
using Core.Application.Features.Group.Commands.CreateGroup;
using Core.Application.Features.Companies.Command;

namespace Core.WebAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/core/[controller]")]
    [Authorize]
    public class CompaniesController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;
        private readonly ICompanyRepository _companyRepository;
        private readonly IGenericRepository<Asset> _repositoryAsset;
        public CompaniesController(ILogger<AuthController> logger, ICompanyRepository companyRepository, ITokenService _jwtTokenService, IMediator mediator, IGenericRepository<Asset> repositoryAsset)
        {
            _logger = logger;
            _mediator = mediator;
            _companyRepository = companyRepository;
            _repositoryAsset = repositoryAsset;
        }

        /// <summary>
        /// Get User Companies.
        /// </summary>
        /// <param name="getusercompanies">The parameter for getting companies.</param>
        [MapToApiVersion(1)]
        [HttpPost("getusercompanies")]
        [AllowAnonymous()]
        public async Task<ActionResult<Result<List<UserCompanyDto>>>> GetUserCompanies([FromBody] GetUserCompaniesQuery userCompaniesQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(userCompaniesQuery, cancellationToken);

        }
        /// <summary>
        /// Get All Companies.
        /// </summary>
        /// <param name="getcompanies">The parameter for getting companies.</param>
        [MapToApiVersion(1)]
        [HttpPost("getcompanies")]
        [AllowAnonymous()]
        public async Task<ActionResult<List<CompanyDto>>> GetCompanies([FromBody] EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompaniesAsync(entityStatus, cancellationToken);
        }
        /// <summary>
        /// Get All Companies Asset.
        /// </summary>
        /// <param name="getAssets">The parameter for getting company assets.</param> 
        [MapToApiVersion(1)]
        [HttpGet("getAssets")]
        [AuthorizeMultiplePermissions(Permissions.Mnu_AsstMgt)]
        public async Task<ActionResult<List<Asset>>> GetAssets()
        {
            return await _repositoryAsset.Entities.AsQueryable().ToListAsync();   
        }
        /// <summary>
        /// Get All Companies Asset with pagination.
        /// </summary>
        /// <param name="getAssets">The parameter for getting company assets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<Result<PaginatedResult<GetAssetsWithPaginationDto>>>> GetAssetsWithPagination([FromQuery] GetAssetsWithPaginationQuery query,CancellationToken cancellationToken)
        {          
                return await _mediator.Send(query,cancellationToken);
        }

        /// <summary>
        /// Create a Company
        /// </summary> 
        /// <param name="create"></param> 
        [MapToApiVersion(1)]
        [HttpPost()]
        [AuthorizeMultiplePermissions(Permissions.Btn_UserSave)]
        public async Task<ActionResult<Result<int>>> CreateCompanyAsync([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}

