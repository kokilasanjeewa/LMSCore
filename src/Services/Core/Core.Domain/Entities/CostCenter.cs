using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{

    [EntityTypeConfiguration(typeof(CostCenterConfiguration))]

    public class CostCenter : BaseAuditableEntity
    {
        [Key]
        public byte CostCenterSerialID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The cost center cannot exceed 50 characters. ")]
        public string? CostCenterName { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
