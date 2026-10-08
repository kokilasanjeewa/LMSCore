using Core.Domain.Common;
using Core.Domain.Configarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    [EntityTypeConfiguration(typeof(TheNumberConfiguration))]
    public class TheNumber: BaseAuditableEntity
    {
        [Key]
        public int TheNumberSerialID { get; set; }
        [Required]
        public int TheNumberID { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The number name cannot exceed 60 characters. ")]
        public string? TheNumberName { get; set; }
        public int? ComSerialID { get; set; }
        public int? LastNumber { get; set; }

    }
}


 		
