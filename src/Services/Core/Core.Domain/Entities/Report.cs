using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(ReportConfiguration))]

    public class Report : BaseAuditableEntity
    {
        [Key]
        public short ReportSerialID { get; set; }
        [Required]
        public int ModSerialID { get; set; }
        [ForeignKey(nameof(ModSerialID))]
        public virtual AppModule? Module { get; set; }
        [Required]
        public int MnuID { get; set; }
        [ForeignKey(nameof(MnuID))]
        public virtual Menu? Menu { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The report name cannot exceed 60 characters. ")]
        public List<string>? DataSet { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The report name cannot exceed 60 characters. ")]
        public string? ReportName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The database name cannot exceed 25 characters. ")]
        public string? DataBaseName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The sp name cannot exceed 100 characters. ")]
        public string? DataBaseSPName { get; set; }
        public int? ReportTypeID { get; set; }
        [StringLength(20, ErrorMessage = "The orientation cannot exceed 15 characters. ")]
        public string? Orientation { get; set; }
        
    }
}
