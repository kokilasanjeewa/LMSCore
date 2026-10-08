using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Group
{
      public class GroupMenuDto : IMapFrom<Core.Domain.Entities.GroupMenu>
    {
        [Required]
        public int GrpMnuSerialID { get; set; }
        [Required]
        public int GrpMnuID { get; set; }

        [Required]
        public int MnuID { get; set; }
        [Required]
        public bool IsRptInc { get; set; }
        [Required]
        public bool IsBtnInc { get; set; }
        [StringLength(1, ErrorMessage = "The MnuMTBR cannot exceed 1 characters. ")]
        [Required]
        public string? MnuMTBR { get; set; }
        public Menu? Menu { get; set; }
    }
}
