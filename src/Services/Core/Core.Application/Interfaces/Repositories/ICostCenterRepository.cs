

using Core.Application.Features.CostCenter.Command;
using Core.Shared;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICostCenterRepository
    {
        Task<Result<int>> DeleteCostCenterAsync(DeleteCostCenterCommand delete, CancellationToken cancellationToken);

    }
}
