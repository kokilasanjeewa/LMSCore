using Core.Application.DTOs.Company;
using Core.Application.DTOs.User;
using Core.Application.Request;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<UserCompanyDto>> GetUserCompaniesAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<List<CompanyDto>> GetCompaniesAsync(EntityStatus entityStatus, CancellationToken cancellationToken);

    }
}
