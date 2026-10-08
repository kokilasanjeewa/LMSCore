using JwtTokenAuthentication.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.ComponentModel.DataAnnotations;
using JwtTokenAuthentication.Dto;
using System.Globalization;
namespace JwtTokenAuthentication.Services
{
    public class JTokenService : IJTokenService
    {
        private readonly string _connectionString;

        public JTokenService(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultTokenConnection"]; ;
        }
        public bool IsTokenInvalidated(string token)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $"SELECT COUNT(1) FROM Core.InvalidateToken WHERE Token = @Token";
            return connection.ExecuteScalar<bool>(sql, new { Token = token });

        }
    }

}
