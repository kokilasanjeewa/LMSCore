using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(UserMenuPermissionConfigaration))]

    public class UserMenuPermission : BaseAuditableEntity
    {
        [Key]
        public int UserMnuPermsSerialID { get; set; }
        [Required]
        public int UserMnuPermsID { get; set; }

        [Required]
        public int UserSerialID { get; set; }

        [ForeignKey(nameof(UserSerialID))]
        public virtual User? User { get; set; }
        public int? GrpSerialID { get; set; } = 0;

        [ForeignKey(nameof(GrpSerialID))]
        public virtual Group? Group { get; set; }
        [Required]
        public int MnuID { get; set; }

        [ForeignKey(nameof(MnuID))]
        public virtual Menu? Menu { get; set; }
        [NotMapped]
        public bool IsRptInc { get; set; }
        [NotMapped]
        public bool IsBtnInc { get; set; }
        [NotMapped]
        public string? MnuMTBR { get; set; }
    }
} 
