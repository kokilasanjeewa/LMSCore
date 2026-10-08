using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(UserConfiguration))]
    public class User : BaseAuditableEntity
    {
        [Key]
        public int UserSerialID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 25 characters. ")]
        public string? UserID { get; set; }
        public long? EESerialID { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? UserName { get; set; }
        public PermissionType PermissionType { get; set; }
        [NotMapped]
        public int PermissionInt { get; set; }
        public int? GrpSerialID { get; set; }
        [ForeignKey(nameof(GrpSerialID))]
        public virtual Group? Group { get; set; }
        [NotMapped]
        [Required]
        public string? PassWd { get; set; }
        [Required]
        [Column(Order = 9)]
        [StringLength(30, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? PasswdSalt { get; set; }
        [Required]
        [Column(Order = 10)]
        [StringLength(50, ErrorMessage = "The User Name cannot exceed 50 characters. ")]
        public string? PasswdHash { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }= new List<RefreshToken>();
        public List<UserCompany>? Companies { get; set; } = new List<UserCompany>();
        public List<UserMenuPermission>? MenuPermissions { get; set; } = new List<UserMenuPermission>();
        [StringLength(45, ErrorMessage = "The User Name cannot exceed 45 characters. ")]
        public string? LastIp { get; set; }
        [StringLength(300, ErrorMessage = "The User Name cannot exceed 200 characters. ")]
        public string? LastSessionId { get; set; }
        public string? LastToken { get; set; }
        public bool? IsLogOut { get; set; }
        [NotMapped]
        public int? LoginCompanySerialID { get; set; }
        [NotMapped]
        public Int64? LoginLogSerialID { get; set; }
    }
    public enum PermissionType { Individual = 1, Group = 2 }
}
