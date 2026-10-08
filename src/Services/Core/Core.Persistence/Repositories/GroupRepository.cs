using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.DTOs.Company;
using Core.Application.DTOs.Group;
using Core.Application.Interfaces.Repositories;
using Core.Application.Request;
using Core.Domain.Entities;
using Core.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Core.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IGenericRepository<Group> _repository;
        private readonly IMapper _mapper;

        public GroupRepository(IGenericRepository<Group> repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
 
        public async Task<List<GroupDto>> GetGroupsAsync(EntityStatus entityStatus, CancellationToken cancellationToken)
        {
            return await _repository.Entities.Where(x => x.Active == entityStatus.Active).ProjectTo<GroupDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
        public async Task<bool> IsUniqueGroup(string groupName, CancellationToken cancellationToken)
        {
            return await _repository.Entities.AnyAsync(x => x.GropName == groupName, cancellationToken);

        }
        public async Task<bool> IsExactlyUniqueGroup(string groupName, int groupSerialID, CancellationToken cancellationToken)
        {
            return await _repository.Entities.AnyAsync(x => x.GropName == groupName && x.GrpSerialID != groupSerialID, cancellationToken);

        }
        public int GetNextGrpID()
        {
            // Retrieve the next sequence value using FromSqlRaw and map it to Group entity
            return _repository.ExecuteScalar<int>("SELECT NEXT VALUE FOR dbo.GrpID");
        }
        public async Task<bool> IsValidGroupID(long groupSerialID, CancellationToken cancellationToken)
        {
            return await _repository.Entities.AnyAsync(x => x.GrpSerialID == groupSerialID, cancellationToken);
        }

    }
}
