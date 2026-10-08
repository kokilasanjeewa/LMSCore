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
    [EntityTypeConfiguration(typeof(UserCompanyConfigaration))]
    public class UserCompany : BaseAuditableEntity
    {
        [Key]
        public int UserComSerialID { get; set; }
        [Required]
        public int UserComID { get; set; }

        [Required]
        public int UserSerialID { get; set; }

        [ForeignKey(nameof(UserSerialID))]
        public virtual User? User { get; set; }

        [Required]
        public int ComSerialID { get; set; }

        [ForeignKey(nameof(ComSerialID))]
        public virtual Company? Company { get; set; }
    }
}

 