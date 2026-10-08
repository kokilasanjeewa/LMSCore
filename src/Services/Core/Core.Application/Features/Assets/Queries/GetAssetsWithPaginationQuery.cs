using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.DTOs.Asset;
using Core.Application.DTOs.User;
using Core.Application.Extensions;
using Core.Application.Features.Users.Queries.GetUsersWithPagination;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using Dapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Assets.Queries
{
    public record GetAssetsWithPaginationQuery : IRequest<Result<PaginatedResult<GetAssetsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }

        public GetAssetsWithPaginationQuery() { }

        public GetAssetsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetAssetsWithPaginationQueryHandler : IRequestHandler<GetAssetsWithPaginationQuery, Result<PaginatedResult<GetAssetsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetAssetsWithPaginationDto> _sortHelper;


        public GetAssetsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetAssetsWithPaginationDto> sortHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }

        public async Task<Result<PaginatedResult<GetAssetsWithPaginationDto>>> Handle(GetAssetsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetAssetsWithPaginationValidator validator = new GetAssetsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);
                parameters.Add("@filteringCol", query.Filter, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@SortingCol", query.Filter, DbType.String, ParameterDirection.Input, 200);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY AssetId) AS Id,* FROM [CoreDB].[Core].[Asset] as a";


                var data = _sqlDataAccess.LoadDataQuery<GetAssetsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                return await Result<PaginatedResult<GetAssetsWithPaginationDto>>.SuccessAsync(result, "Loaded successfully.");

            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetAssetsWithPaginationDto>>.FailureAsync(errors);
            }

        }
    }
}
