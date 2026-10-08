using AutoMapper;
using Core.Application.DTOs.Approval;
using Core.Application.Extensions;
using Core.Application.Features.Approval.Queries.GetWorkFlowsWithPagination;
using Core.Application.Interfaces.Repositories;
using Core.Shared;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Features.Approval.Queries.GetWorkFlow
{
    public record GetWorkFlowWithQuery : IRequest<Result<GetWorkFlowWithDto>>
    {
        public Boolean status { get; set; }
        public int ApprovalWorkflowID { get; set; }
        public string? Filter { get; set; }
        public GetWorkFlowWithQuery() { }

        public GetWorkFlowWithQuery(int approvalWorkflowID)
        {
            ApprovalWorkflowID = approvalWorkflowID;
        }
    }

    internal class GetWorkFlowWithQueryHandler : IRequestHandler<GetWorkFlowWithQuery, Result<GetWorkFlowWithDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetWorkFlowWithDto> _sortHelper;

        public GetWorkFlowWithQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetWorkFlowWithDto> sortHelper)
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

        public async Task<Result<GetWorkFlowWithDto>> Handle(GetWorkFlowWithQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetWorkFlowWithValidator validator = new GetWorkFlowWithValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@approvalWorkflowID", query.ApprovalWorkflowID, DbType.Int64, ParameterDirection.Input, query.ApprovalWorkflowID);

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
                    WHERE 1 = 1 and apwf.ApprovalWorkflowID=@approvalWorkflowID";// Start with a true condition for dynamic filtering
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

                var data = _sqlDataAccess.SingleDataQuery<GetWorkFlowWithDto, dynamic>(sql, parameters).Result;

                if (!string.IsNullOrWhiteSpace(data.ApprovalStepsJson))
                {
                    data.ApprovalSteps =
                            JsonSerializer.Deserialize<List<ApprovalStepDto>>(data.ApprovalStepsJson)
                            ?? new List<ApprovalStepDto>();
                    data.ApprovalStepsJson = "";
                }


                return await Result<GetWorkFlowWithDto>.SuccessAsync(data, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<GetWorkFlowWithDto>.FailureAsync(errors);

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
