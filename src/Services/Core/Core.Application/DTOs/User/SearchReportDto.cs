using Core.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class SearchReportDto : IMapFrom<Core.Domain.Entities.UserMenuPermission>
    {
        [Required]
        public int UserMnuPermsSerialID { get; set; }
        [Required]
        public int UserMnuPermsID { get; set; }

        [Required]
        public int UserSerialID { get; set; }

        [Required]
        public int MnuID { get; set; }
        [Required]
        public bool IsRptInc { get; set; }
        [Required]
        public bool IsBtnInc { get; set; }
        [StringLength(1, ErrorMessage = "The MnuMTBR cannot exceed 1 characters. ")]
        [Required]
        public string? MnuMTBR { get; set; }
    }
}
