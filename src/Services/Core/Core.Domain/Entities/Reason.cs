using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ReasonConfiguration))]
    public class Reason : BaseAuditableEntity
    {
        [Key]
        public int ReasonSerialID { get; set; }             // Reason ID
        [Required]
        public int ModSerialID { get; set; }      // Module ID
        [ForeignKey(nameof(ModSerialID))]
        public virtual AppModule? Module { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The document name cannot exceed 15 characters. ")]
        public string? Document { get; set; }          // Document (nvarchar(15))
        [Required]
        [StringLength(100, ErrorMessage = "The reason text name cannot exceed 100 characters. ")]
        public string? ReasonText { get; set; }        // Reason (nvarchar(50))
    }

}
