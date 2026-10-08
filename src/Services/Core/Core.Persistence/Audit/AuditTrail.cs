using Audit.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Core.Persistence.Audit
{
    [AuditIgnore]
    public class AuditTrail
    {
        [Key]
        public long AudtTralID { get; set; }
        public DateTime AuditDateTimeUtc { get; set; }
        [Required]
        public int? UserSerialID { get; set; }
        [Required]
        public int? MnuSerialID { get; set; }
        [Required]
        public long? LoginLogSerialID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? TableName { get; set; }
        [Required]
        public string? AuditData { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "The User Name cannot exceed 255 characters. ")]
        public string? MachineName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The User Name cannot exceed 10 characters. ")]
        public string? Action { get; set; }


    }
}
