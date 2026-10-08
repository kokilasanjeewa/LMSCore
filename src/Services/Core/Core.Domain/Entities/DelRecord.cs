using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(DelRecordConfiguration))]

    public class DelRecord : BaseAuditableEntity
    {
        [Key]
        public int DelRecSerialID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The doc table cannot exceed 50 characters. ")]
        public string? DocTable { get; set; }
        [Required]
        public int? DocSerialID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The remarks cannot exceed 100 characters. ")]
        public string? Remarks { get; set; }
    }
}
