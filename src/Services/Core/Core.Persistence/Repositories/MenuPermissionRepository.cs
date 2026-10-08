using Core.Application.DTOs.User;
using Core.Application.Interfaces.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Persistence.Repositories
{
    public class MenuPermissionRepository : IMenuPermissionRepository
    {
        private readonly string _connectionString;
        public MenuPermissionRepository()
        {
            //production
          //  _connectionString = "Server=coredb;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;";

            //development
            _connectionString = "Server=KOKILA-LAP\\MSSQLDAYARATHNE;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;";

          //  _connectionString = "Server=192.168.1.205;Database=CoreDB;User Id=sa;Password=Sqlserver2019;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        }

        public  IEnumerable<GetMenuPermissionDynamicDto> GetMenuPermissionsAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Deleted", 0, DbType.Int64, ParameterDirection.Input);
            var sql = @"SELECT ROW_NUMBER() OVER(ORDER BY MnuSerialID) AS Id,[MnuID],m.[ModSerialID],module.ModName,[MnuLevel],[IsShown],[ParentID],m.[Active],m.[IsDeleted],[MnuName],[MnuPosition],[MnuText],[PageName] 
                    FROM [CoreDB].[Core].[Menu] as m
                    LEFT JOIN [CoreDB].[Core].[Module] as module 
                    ON m.ModSerialID = module.ModSerialID 
                    WHERE m.[IsShown] = 1 and m.[IsDeleted] = @Deleted
                    ORDER BY m.MnuPosition"
            ;
            using var connection = new SqlConnection(_connectionString);
            return  connection.QueryAsync<GetMenuPermissionDynamicDto>(sql, parameters, commandTimeout: 120, commandType: CommandType.Text).Result.AsEnumerable();
        }
    }
}
