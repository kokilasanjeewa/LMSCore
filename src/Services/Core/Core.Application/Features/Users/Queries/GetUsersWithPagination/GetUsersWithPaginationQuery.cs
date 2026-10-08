using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.DTOs.User;
using Core.Application.Extensions;
using Core.Application.Features.Users.Commands.CreateUser;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Core.Shared;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;

namespace Core.Application.Features.Users.Queries.GetUsersWithPagination
{
    public record GetUsersWithPaginationQuery : IRequest<Result<PaginatedResult<GetUsersWithPaginationDto>>>
    {
        public Boolean status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? Filter { get; set; }

        public GetUsersWithPaginationQuery() { }

        public GetUsersWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, Result<PaginatedResult<GetUsersWithPaginationDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ISortHelper<GetUsersWithPaginationDto> _sortHelper;

        public GetUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISqlDataAccess sqlDataAccess, ISortHelper<GetUsersWithPaginationDto> sortHelper)
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

        public async Task<Result<PaginatedResult<GetUsersWithPaginationDto>>> Handle(GetUsersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            // Correct way
            GetUsersWithPaginationValidator validator = new GetUsersWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(query);

            // _validator.ValidateAndThrow(command);
            if (validationResult.IsValid)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@pageNumber", query.PageNumber, DbType.Int64, ParameterDirection.Input, query.PageNumber);
                parameters.Add("@pageSize", query.PageSize, DbType.Int64, ParameterDirection.Input, query.PageSize);

                var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY u.UserSerialID) AS Id, u.UserSerialID, u.UserID, u.UserName, u.Active, u.CreatedDate, u.ModifiedDate, g.GropName as GrpName,
                                CASE u.PermissionType WHEN 1 THEN 'Individual' WHEN 2 THEN 'Group' END AS PermissionType, u.EESerialID, ee.Phone1 as Mobile, ee.CompanyEmail as Email,ee.Pi_Company_Code as ComCode,ee.CompanyName as CompanyName, ee.DeptName as DeptName, ee.SectName, ee.DesigName
                            FROM [CoreDB].[Core].[User] AS u
                            LEFT JOIN [CoreDB].[Core].[Group] AS g ON u.GrpSerialID = g.GrpSerialID
                            LEFT JOIN (SELECT e.EESerialID, e.CompanyEmail, e.Phone1, d.DeptName, s.SectName, dsg.DesigName,c.CompanyName,CASE c.ComSerialID
											WHEN 6 THEN 'PSK'     -- PSK
											WHEN 7 THEN 'DAS'     -- DAS
											WHEN 8 THEN 'DH'     -- DH
											WHEN 9 THEN 'VBTN'     -- VBTN
											WHEN 10 THEN 'IIIE'   -- IIIE
											WHEN 11 THEN 'IL'   -- IL
											WHEN 1  THEN 'ALL'
											ELSE 'N/A'            -- ALL (default case)
										END AS Pi_Company_Code
                                       FROM [HCMDB].[HCM].[Employee] AS e
                                       LEFT JOIN [HCMDB].[HCM].[Department] AS d ON e.DeptSerialID = d.DeptSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Section] AS s ON e.SectSerialID = s.SectSerialID
									   LEFT JOIN [CoreDB].[Core].[Company] AS c ON e.ComSerialID = c.ComSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Designation] AS dsg ON e.DesigSerialID = dsg.DesigSerialID) AS ee ON u.EESerialID = ee.EESerialID
                            Where 1 = 1";// Start with a true condition for dynamic filtering
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
                                if(columnName == "[Active]")
                                {
                                    columnName = "U.active";
                                }
                                // Sanitize the value
                                value = value.Replace("'", "''"); // Escape single quotes
                                                                  // Use LIKE clause
                                sql += $" AND {columnName} LIKE '%{value}%'"; // Use LIKE
                            }
                          
                        }
                    }
                }

                sql += " ORDER BY u.UserSerialID;";

                var data = _sqlDataAccess.LoadDataQuery<GetUsersWithPaginationDto, dynamic>(sql, parameters).Result.AsEnumerable();
                #region
                // var filterData = _sortHelper.FilterByName(data.AsQueryable(), query.Filter);
                // var result = await filterData.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);
                #endregion
                var result = await data.ToPaginatedCustomListAsync(query.PageNumber, query.PageSize, cancellationToken);

                return await Result<PaginatedResult<GetUsersWithPaginationDto>>.SuccessAsync(result, "Loaded Successfully.");
            }
            else
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await Result<PaginatedResult<GetUsersWithPaginationDto>>.FailureAsync(errors);

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

/*SELECT ROW_NUMBER() OVER(ORDER BY u.UserSerialID) AS Id, u.UserSerialID, u.UserID, u.UserName, u.Active, u.CreatedDate, u.ModifiedDate, g.GropName as GrpName,
                                CASE u.PermissionType WHEN 1 THEN 'Individual' WHEN 2 THEN 'Group' END AS PermissionType, u.EESerialID, ee.Pi_Mobile as Mobile, ee.Pi_Email as Email,ee.Pi_Company_Code as ComCode,ee.CompanyName as CompanyName, ee.DeptName as DeptName, ee.SectName, ee.DesigName
                            FROM [CoreDB].[Core].[User] AS u
                            LEFT JOIN [CoreDB].[Core].[Group] AS g ON u.GrpSerialID = g.GrpSerialID
                            LEFT JOIN (SELECT e.EESerialID, e.Pi_Email, e.Pi_Mobile, d.DeptName, s.SectName, dsg.DesigName,c.CompanyName,e.Pi_Company_Code
                                       FROM [HCMDB].[HCM].[Employee] AS e
                                       LEFT JOIN [HCMDB].[HCM].[Department] AS d ON e.DeptSerialID = d.DeptSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Section] AS s ON e.SectSerialID = s.SectSerialID
									   LEFT JOIN [CoreDB].[Core].[Company] AS c ON e.ComSerialID = c.ComSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Designation] AS dsg ON e.DesigSerialID = dsg.DesigSerialID) AS ee ON u.EESerialID = ee.EESerialID
                            Where 1 = 1SELECT ROW_NUMBER() OVER(ORDER BY u.UserSerialID) AS Id, u.UserSerialID, u.UserID, u.UserName, u.Active, u.CreatedDate, u.ModifiedDate, g.GropName as GrpName,
                                CASE u.PermissionType WHEN 1 THEN 'Individual' WHEN 2 THEN 'Group' END AS PermissionType, u.EESerialID, ee.Pi_Mobile as Mobile, ee.Pi_Email as Email,ee.Pi_Company_Code as ComCode,ee.CompanyName as CompanyName, ee.DeptName as DeptName, ee.SectName, ee.DesigName
                            FROM [CoreDB].[Core].[User] AS u
                            LEFT JOIN [CoreDB].[Core].[Group] AS g ON u.GrpSerialID = g.GrpSerialID
                            LEFT JOIN (SELECT e.EESerialID, e.Pi_Email, e.Pi_Mobile, d.DeptName, s.SectName, dsg.DesigName,c.CompanyName,e.Pi_Company_Code
                                       FROM [HCMDB].[HCM].[Employee] AS e
                                       LEFT JOIN [HCMDB].[HCM].[Department] AS d ON e.DeptSerialID = d.DeptSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Section] AS s ON e.SectSerialID = s.SectSerialID
									   LEFT JOIN [CoreDB].[Core].[Company] AS c ON e.ComSerialID = c.ComSerialID
                                       LEFT JOIN [HCMDB].[HCM].[Designation] AS dsg ON e.DesigSerialID = dsg.DesigSerialID) AS ee ON u.EESerialID = ee.EESerialID
                            Where 1 = 1*/