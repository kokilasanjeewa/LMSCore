using AutoMapper;
using Core.Application.DTOs.Approval;
using Core.Application.Extensions;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Dapper;
using MediatR;
using System.Data;
using System.Text.Json;

namespace Core.Application.Features.Approval.Queries.GetWorkFlowsWithPagination
{

    public record GetWorkFlowsWithPaginationQuery : IRequest<Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Filter { get; set; }

        public GetWorkFlowsWithPaginationQuery() { }

        public GetWorkFlowsWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetWorkFlowsWithPaginationQueryHandler : IRequestHandler<GetWorkFlowsWithPaginationQuery, Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetWorkFlowsWithPaginationDto> _sortHelper;

        public GetWorkFlowsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetWorkFlowsWithPaginationDto> sortHelper)
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

        public async Task<Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>> Handle(GetWorkFlowsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetWorkFlowsWithPaginationValidator validator = new GetWorkFlowsWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER (ORDER BY apwf.ApprovalWorkflowID) AS Id,apwf.ApprovalWorkflowID,apwf.EntityTypeID,
                        et.EntityName AS EntityType,apwf.ComSerialID,c.CompanyName,c.ComCode,apwf.WorkflowCode,apwf.WorkflowName,
                        (   SELECT aps.ApprovalStepID,aps.StepOrder,aps.StepCode,aps.StepName,aps.ApprovalRole,aps.PlantID,
                                aps.IsMandatory,aps.CanReject,aps.IsFinalStep
                            FROM [CoreDB].[Core].[ApprovalStep] aps
                            WHERE aps.ApprovalWorkflowID = apwf.ApprovalWorkflowID
                            ORDER BY aps.StepOrder
                            FOR JSON PATH
                        ) AS ApprovalStepsJson,
	                    cu.UserName as Created,mu.UserName as Modified,apwf.CreatedDate,apwf.ModifiedDate,apwf.Active
                    FROM [CoreDB].[Core].[ApprovalWorkFlow] apwf
                    LEFT JOIN [CoreDB].[Core].[Company] c ON apwf.ComSerialID = c.ComSerialID
                    LEFT JOIN [CoreDB].[Core].[EntityType] et ON apwf.EntityTypeID = et.EntityTypeID
                    LEFT JOIN [CoreDB].[Core].[User] AS cu ON apwf.CreatedBy = cu.UserSerialID
                    LEFT JOIN [CoreDB].[Core].[User] AS mu ON apwf.ModifiedBy = mu.UserSerialID
                    WHERE 1 = 1";// Start with a true condition for dynamic filtering
                #region
                /*    foreach (var term in terms)
                    {
                        // Split each term into key and value
                        var parts = term.Split('=');
                        if (parts.Length == 2)
                        {
                            // Get the column name and capitalize the first letter

                            string columnName = CapitalizeFirstLetter(parts[0].Trim());
                            string value = parts[1].Trim();

                            // Validate column name and prevent SQL injection
                            columnName = SqlHelper.QuoteIdentifier(columnName);

                            // Add to SQL and parameters
                            sql += $" AND {columnName} = @{columnName}";
                            parameters.Add($"@{columnName}", value);
                        }
                    }*/
                #endregion
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
                                if (columnName == "[Active]")
                                {
                                    columnName = "apwf.active";
                                }
                                // Sanitize the value
                                value = value.Replace("'", "''"); // Escape single quotes
                                                                  // Use LIKE clause
                                sql += $" AND {columnName} LIKE '%{value}%'"; // Use LIKE
                            }

                        }
                    }
                }

                sql += " ORDER BY apwf.ApprovalWorkflowID;";

                var data = _sqlDataAccess.LoadDataQuery<GetWorkFlowsWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                #region
                // var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                // var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                #endregion
                foreach (var item in data)
                {
                    if (!string.IsNullOrWhiteSpace(item.ApprovalStepsJson))
                    {
                        item.ApprovalSteps =
                            JsonSerializer.Deserialize<List<ApprovalStepDto>>(item.ApprovalStepsJson)
                            ?? new List<ApprovalStepDto>();
                        item.ApprovalStepsJson = "";
                    }
                }
                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetWorkFlowsWithPaginationDto>>.FailureAsync(errors);

            }

        }
    }
    // Helper class to safely quote identifiers
    public static class SqlHelper
    {
        public static string QuoteIdentifier(string identifier)
        {
            // Prevent SQL injection by enclosing the identifier in square brackets
            return "[" + identifier.Replace("]", "]]") + "]";
        }
    }

}
