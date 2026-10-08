using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Group
{
    public class GroupDto : IMapFrom<Core.Domain.Entities.Group>
    {
        [Required]
        public int GrpSerialID { get; set; }
        public int GrpID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The group name cannot exceed 25 characters. ")]
        public string? GropName { get; set; }
    }
}
