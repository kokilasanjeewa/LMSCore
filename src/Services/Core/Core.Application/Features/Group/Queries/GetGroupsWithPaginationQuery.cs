using AutoMapper;
using Core.Application.DTOs.Group;
using Core.Application.Extensions;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Queries
{
      public record GetGroupsWithPaginationQuery : IRequest<Result<PaginatedResult<GetGroupsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }

        public GetGroupsWithPaginationQuery() { }

        public GetGroupsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetGroupsWithPaginationQueryHandler : IRequestHandler<GetGroupsWithPaginationQuery, Result<PaginatedResult<GetGroupsWithPaginationDto>>>
    {
          private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetGroupsWithPaginationDto> _sortHelper;


        public GetGroupsWithPaginationQueryHandler(ISqlDataAccess sqlDataAccess, ISortHelper<GetGroupsWithPaginationDto> sortHelper)
        {
             _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetGroupsWithPaginationDto>>> Handle(GetGroupsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetGroupsWithPaginationValidator validator = new GetGroupsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);
                parameters.Add("@filteringCol", query.Filter, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@SortingCol", query.Filter, DbType.String, ParameterDirection.Input, 200);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY GrpSerialID) AS Id,* FROM [CoreDB].[Core].[Group] as grp";


                var data = _sqlDataAccess.LoadDataQuery<GetGroupsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                return await Result<PaginatedResult<GetGroupsWithPaginationDto>>.SuccessAsync(result, "Loaded successfully.");

            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetGroupsWithPaginationDto>>.FailureAsync(errors);
            }

        }
    }
}
