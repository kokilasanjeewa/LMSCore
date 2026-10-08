using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class GetMenuPermissionDynamicDto : IMapFrom<Core.Domain.Entities.Menu>
    {
        [Required]
        public long Id { get; set; }
        public int MnuID { get; set; }
        [Required]
        public int? ModSerialID { get; set; }
        public string? ModName { get; set; }
        [Required]
        public int? MnuLevel { get; set; }
        [Required]
        public decimal MnuPosition { get; set; }

        [StringLength(20, ErrorMessage = "The menu name cannot exceed 20 characters. ")]
        public string? MnuName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The menu text cannot exceed 30 characters. ")]
        public string? MnuText { get; set; }
        [Required]
        [StringLength(35, ErrorMessage = "The page name cannot exceed 35 characters. ")]
        public string? PageName { get; set; }
        public bool IsShown { get; set; }
        public int? ParentID { get; set; }
        public bool IsDeleted { get; set; } 
        public bool Active { get; set; }
        public bool IsChecked { get; set; }
        public string? MnuMTBR { get; set; }

    }
}
