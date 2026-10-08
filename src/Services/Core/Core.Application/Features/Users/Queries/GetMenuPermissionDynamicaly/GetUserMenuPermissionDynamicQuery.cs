using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Dapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Core.Application.Features.Users.Queries.GetMenuPermissionDynamicaly
{
  
    public record GetUserMenuPermissionDynamicQuery : IRequest<List<GetMenuPermissionDynamicDto>>
    {

        [Required]
        public long UserSerialID { get; set; }
        [Required]
        public int? Filter { get; set; }
        [Required]
        public int? Action { get; set; }

        public GetUserMenuPermissionDynamicQuery(long userSerialID,int? filter, int? action)
        {
            UserSerialID = userSerialID;
            Filter = filter;
            Action = action;
        }
    }

    internal class GetUserMenuPermissionDynamicQueryHandler : IRequestHandler<GetUserMenuPermissionDynamicQuery, List<GetMenuPermissionDynamicDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetUserMenuPermissionDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<List<GetMenuPermissionDynamicDto>> Handle(GetUserMenuPermissionDynamicQuery query, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@userSerialID", query.UserSerialID, DbType.Int64, ParameterDirection.Input);

            var result = await _sqlDataAccess.LoadDataQuery<GetMenuPermissionDynamicDto, dynamic>(
                MenuPermissionQuerySql.UserMenus,
                parameters);
            return result.ToList();
        }
    }

}


