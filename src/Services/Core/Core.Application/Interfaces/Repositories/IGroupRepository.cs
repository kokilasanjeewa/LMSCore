using Core.Application.DTOs.Company;
using Core.Application.DTOs.Group;
using Core.Application.Request;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<List<GroupDto>> GetGroupsAsync(EntityStatus entityStatus, CancellationToken cancellationToken);
        Task<bool> IsUniqueGroup(string groupName, CancellationToken cancellationToken);
        Task<bool> IsExactlyUniqueGroup(string groupName, int groupSerialID, CancellationToken cancellationToken);
        int GetNextGrpID();
        Task<bool> IsValidGroupID(long groupSerialID, CancellationToken cancellationToken);
    }
}
