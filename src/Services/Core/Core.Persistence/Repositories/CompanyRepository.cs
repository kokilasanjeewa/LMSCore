using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.DTOs.Company;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Repositories
{
    /// <summary>
    /// Represents a repository for managing companies.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IGenericRepository<UserCompany> _repository;
        private readonly IGenericRepository<Company> _repositoryCompany;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyRepository"/> class.
        /// </summary>
        /// <param name="repository">The repository for managing user companies.</param>
        /// <param name="mapper">The mapper for converting between entity and DTO objects.</param>
        /// <param name="unitOfWork">The unit of work for managing database transactions.</param>
        public CompanyRepository(IGenericRepository<UserCompany> repository, IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Company> repositoryCompany)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repositoryCompany = repositoryCompany;
        }

        /// <summary>
        /// Retrieves the list of companies for a specific user.
        /// </summary>
        /// <param name="entityStatus">The entity status of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of company DTOs.</returns>
        public async Task<List<UserCompanyDto>> GetUserCompaniesAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetEntityWithThenIncludesAsync(
                             u => u.UserID == entityStatus.UserID && u.Active == true,
                             cancellationToken,
                             q => q.Include(e => e.Companies.Where(c => c.Active == true)).ThenInclude(e => e.Company).Where(a => a.IsDeleted == false));
            if (user == null) return Enumerable.Empty<UserCompanyDto>().ToList(); ;
            var userDto = _mapper.Map<List<UserCompanyDto>>(user.Companies);
            return userDto;
        }

        /// <summary>
        /// Retrieves the list of all companies.
        /// </summary>
        /// <param name="entityStatus">The entity status of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of company DTOs.</returns>
        public async Task<List<CompanyDto>> GetCompaniesAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            //You are ignoring global query filters in this case (ignore query filters).

            return await _repositoryCompany.Entities.IgnoreQueryFilters().Where(x => x.Active == entityStatus.Active).ProjectTo<CompanyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        }
    }
}
