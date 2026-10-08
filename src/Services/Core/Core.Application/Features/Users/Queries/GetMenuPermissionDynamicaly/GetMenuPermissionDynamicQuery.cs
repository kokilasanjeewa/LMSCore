using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Dapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Core.Application.Features.Users.Queries.GetMenuPermissionDynamicaly
{
  
    public record GetMenuPermissionDynamicQuery : IRequest<List<GetMenuPermissionDynamicDto>>
    {
        [Required]
        public int? Filter { get; set; }
        [Required]
        public int? Action { get; set; }

        public GetMenuPermissionDynamicQuery(int? filter,int? action)
        {
            Filter = filter;
            Action = action;    
        }
    }

    internal class GetMenuPermissionDynamicQueryHandler : IRequestHandler<GetMenuPermissionDynamicQuery, List<GetMenuPermissionDynamicDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetMenuPermissionDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<List<GetMenuPermissionDynamicDto>> Handle(GetMenuPermissionDynamicQuery query, CancellationToken cancellationToken)
        {
            var result = await _sqlDataAccess.LoadDataQuery<GetMenuPermissionDynamicDto, dynamic>(
                MenuPermissionQuerySql.AllMenus,
                new DynamicParameters());
            return result.ToList();
        }
    }

}


