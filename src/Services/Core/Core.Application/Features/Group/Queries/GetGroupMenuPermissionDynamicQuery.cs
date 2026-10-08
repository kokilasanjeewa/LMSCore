using AutoMapper;
using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Dapper;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Core.Application.Features.Users.Queries.GetMenuPermissionDynamicaly
{
  
    public record GetGroupMenuPermissionDynamicQuery : IRequest<List<GetMenuPermissionDynamicDto>>
    {
        [Required]
        public long GrpSerialID { get; set; }
        [Required]
        public int? Filter { get; set; }
        [Required]
        public int? Action { get; set; }

        public GetGroupMenuPermissionDynamicQuery(long grpSerialID, int? filter, int? action)
        {
            GrpSerialID = grpSerialID;
            Filter = filter;
            Action = action;
        }
    }

    internal class GetGroupMenuPermissionDynamicQueryHandler : IRequestHandler<GetGroupMenuPermissionDynamicQuery, List<GetMenuPermissionDynamicDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;

        public GetGroupMenuPermissionDynamicQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<List<GetMenuPermissionDynamicDto>> Handle(GetGroupMenuPermissionDynamicQuery query, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@grpSerialID", query.GrpSerialID, DbType.Int64, ParameterDirection.Input);

            var result = await _sqlDataAccess.LoadDataQuery<GetMenuPermissionDynamicDto, dynamic>(
                MenuPermissionQuerySql.GroupMenus,
                parameters);
            return result.ToList();
        }
    }

}


