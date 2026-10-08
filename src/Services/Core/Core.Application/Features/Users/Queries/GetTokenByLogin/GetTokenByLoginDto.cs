using Core.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Application.Features.Users.Queries.GetTokenByLogin
{
    public class GetTokenByLoginDto
    {
        public string? token { get; set; }
        public string? imageProfileUrl { get; set; }
        public bool isAuthorized { get; set; }
        public string? imageCompanyUrl { get; set; }
        public string? iconCompanyUrl { get; set; }
        public string? userName { get; set; }
        public string? userID { get; set; }
        public long? eESerialID { get; set; }
        public int? comSerialID { get; set; }
        public decimal Expires {  get; set; }  
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
