using AutoMapper;
using Core.Application.DTOs.Asset;
using Core.Application.DTOs.DocType;
using Core.Application.Extensions;
using Core.Application.Features.Assets.Queries;
using Core.Application.Features.Users.Queries.GetUsersWithPagination;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.DocType.Queries
{
    public record GetDocTypesWithPaginationQuery : IRequest<Result<PaginatedResult<GetDocTypePaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? SortColumn { get; set; } // Column to sort by
        public string? SortDirection { get; set; } // Sort direction: ASC or DESC
        public GetDocTypesWithPaginationQuery() { }

        public GetDocTypesWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetDocTypesWithPaginationQueryHandler : IRequestHandler<GetDocTypesWithPaginationQuery, Result<PaginatedResult<GetDocTypePaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetDocTypePaginationDto> _sortHelper;
        public GetDocTypesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetDocTypePaginationDto> sortHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sqlDataAccess = sqlDataAccess;
            _sortHelper = sortHelper;
        }
        // Method to capitalize the first letter of a string
        private static string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsUpper(str[0]))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }
        public async Task<Result<PaginatedResult<GetDocTypePaginationDto>>> Handle(GetDocTypesWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetDocTypesWithPaginationValidator validator = new GetDocTypesWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"  SELECT ROW_NUMBER() OVER(ORDER BY a.DocTypeSerialID) AS Id,a.ModSerialID,m.ModName,a.DocTypeSerialID,DocumentType,FilePathSerialID,FilePath,a.[CreatedBy],a.[CreatedDate],a.[ModifiedBy],a.[ModifiedDate],a.Active,a.IsDeleted  FROM [CoreDB].[Core].[DocType] as a
                              left join [CoreDB].[Core].[FilePaths] as pa on a.DocTypeSerialID=pa.DocTypeSerialID
                              left join [CoreDB].[Core].[Module] as m on a.ModSerialID=m.ModSerialID where 1=1";
                // Split the search terms by comma
                if (!string.IsNullOrWhiteSpace(query.Filter))
                {
                    string[] terms = query.Filter.Split(',');
                    foreach (var term in terms)
                    {
                        // Split each term into key and value
                        var parts = term.Split('=');
                        if (parts.Length == 2)
                        {
                            // Get the column name and capitalize the first letter
                            string columnName = CapitalizeFirstLetter(parts[0].Trim());
                            string value = parts[1].Trim();
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                // Validate column name and prevent SQL injection
                                columnName = SqlHelper.QuoteIdentifier(columnName);
                                switch (columnName)
                                {
                                    case "[Active]":
                                        columnName = "a.[Active]";
                                        break;
                                    case "[IsDeleted]":
                                        columnName = "a.[IsDeleted]";
                                        break;
                                    case "[DocTypeSerialID]":
                                        columnName = "a.DocTypeSerialID";
                                        break;
                                    case "[DocumentType]":
                                        columnName = "a.DocumentType";
                                        break;
                                    case "[ModSerialID]":
                                        columnName = "a.ModSerialID";
                                        break;
                                    case "[FilePath]":
                                        columnName = "pa.FilePath";
                                        break;
                                    case "[ModName]":
                                        columnName = "m.ModName";
                                        break;
                                    case "[DeletedBy]":
                                        columnName = "a.[DeletedBy]";
                                        break;
                                    case "[CreatedBy]":
                                        columnName = "a.[CreatedBy]";
                                        break;
                                    case "[ModifiedBy]":
                                        columnName = "a.[ModifiedBy]";
                                        break;
                           
                                }
                                // Sanitize the value
                                value = value.Replace("'", "''"); // Escape single quotes
                                                                  // Use LIKE clause
                                sql += $" AND {columnName} LIKE '%{value}%'"; // Use LIKE
                            }

                        }
                    }
                }

                // Apply Sorting
                string sortColumn = query.SortColumn ?? "DocTypeSerialID"; // Default column
                string sortDirection = query.SortDirection?.ToUpper() == "DESC" ? "DESC" : "ASC"; // Default to ASC
                sortColumn = SqlHelper.QuoteIdentifier(sortColumn); // Prevent SQL injection
                switch (sortColumn)
                {
                    case "[DocTypeSerialID]":
                        sortColumn = "a.DocTypeSerialID";
                        break;
                }
                sql += $" ORDER BY {sortColumn} {sortDirection};";
                var data = _sqlDataAccess.LoadDataQuery<GetDocTypePaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                return await Result<PaginatedResult<GetDocTypePaginationDto>>.SuccessAsync(result, "Loaded successfully.");

            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetDocTypePaginationDto>>.FailureAsync(errors);
            }

        }
    }
}
