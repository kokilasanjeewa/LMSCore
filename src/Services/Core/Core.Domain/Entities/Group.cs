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
    [EntityTypeConfiguration(typeof(GroupConfigaration))]
    public class Group : BaseAuditableEntity
    {
        [Key]
        public int GrpSerialID { get; set; }
        [Required]
        public int GrpID { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters. ")]
        public string? GropName { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<GroupMenu>? GroupMenus { get; set; } 

    }
}
