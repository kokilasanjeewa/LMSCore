using AutoMapper;
using Core.Application.DTOs.Group;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Queries.GetGroup
{

    public record GetGroupQuery : IRequest<Result<SearchGroupDto>>
    {
        [Required]
        public long GroupSerialID { get; set; }
        public GetGroupQuery(long groupSerialID)
        {
            GroupSerialID = groupSerialID;
        }

    }
    internal class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, Result<SearchGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly IGroupRepository _groupRepository;

        public GetGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _groupRepository = groupRepository;
        }

        public async Task<Result<SearchGroupDto>> Handle(GetGroupQuery query, CancellationToken cancellationToken)
        {
            GetGroupCommandValidator validator = new GetGroupCommandValidator(_groupRepository);
            var validationResult = await validator.ValidateAsync(query, cancellationToken);
            if (validationResult.IsValid)
            {
                // Step 1: Retrieve the user along with GrpSerialID and necessary includes
                var group = await _unitOfWork.Repository<Core.Domain.Entities.Group>().GetEntityWithThenIncludesAsync(
                    u => u.GrpSerialID == query.GroupSerialID,
                    cancellationToken,
                    q => q.Include(e => e.GroupMenus.Where(predicate => predicate.Active == true)).ThenInclude(np => np.Menu));

                var groupDto = _mapper.Map<SearchGroupDto>(group);

                if (groupDto == null)
                {
                    return await Result<SearchGroupDto>.FailureAsync(data: new SearchGroupDto(), message: "Group not found");
                }

                return await Result<SearchGroupDto>.SuccessAsync(data: groupDto, message: "Found successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<SearchGroupDto>.FailureAsync(data: new SearchGroupDto(), messages: errors);
            }

        }
    }
}
