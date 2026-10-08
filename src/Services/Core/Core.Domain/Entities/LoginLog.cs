using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(LoginLogConfiguration))]
    public class LoginLog : BaseAuditableEntity
    {
        [Key]
        public Int64 LoginLogSerialID { get; set; }
        [Required]
        public int UserSerialID { get; set; }

        [ForeignKey(nameof(UserSerialID))]
        public virtual User? User { get; set; }
        [Required]
        public DateTime? LoginDateTime { get; set; }
        [StringLength(45, ErrorMessage = "The User Name cannot exceed 45 characters. ")]
        public string? IPAddress { get; set; }
        [StringLength(255, ErrorMessage = "The User Name cannot exceed 255 characters. ")]
        public string? MachineName { get; set; }
        public bool IsTimeout { get; set; }
        public bool IsSystemLogout { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public DateTime? TokenExpire { get; set; }
       
    }
}
