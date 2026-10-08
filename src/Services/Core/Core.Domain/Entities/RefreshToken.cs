using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#region How to Use Refresh Tokens in ASP.NET Core APIs - JWT Authentication
/* https://codewithmukesh.com/blog/refresh-tokens-in-aspnet-core/ */
#endregion

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(RefreshTokenConfiguration))]

    public class RefreshToken : BaseAuditableEntity
    {
        [Key]
        public int RTSerialID { get; set; }
        [Required]
        public int RTID { get; set; }
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        [NotMapped]
        [JsonIgnore]
        public string? ExistsRefreshToken { get; set; }
        [JsonIgnore]
        [NotMapped]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
