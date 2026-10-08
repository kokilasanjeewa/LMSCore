using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class Bank : BaseAuditableEntity
    {
        [Key]
        public int BankSerialID { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The bank code cannot exceed 15 characters. ")]
        public string? BankCode { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The bank name cannot exceed 30 characters. ")]
        public string? BankName { get; set; }

        // Navigation property for the branches
        public virtual ICollection<BankBranch> BankBranches { get; set; } = new List<BankBranch>();
    }
}




