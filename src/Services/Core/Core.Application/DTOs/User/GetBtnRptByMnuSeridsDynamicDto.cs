using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.User
{
    public class GetBtnRptByMnuSeridsDynamicDto
    {
        public int Id { get; set; }
        public int MnuID { get; set; }
        public int ModSerialID { get; set; }
        public string? ModName { get; set; }
        public int MnuLevel { get; set; }
        public bool IsShown { get; set; }
        public int ParentID { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public string? MnuName { get; set; }
        public string? MnuPosition { get; set; }
        public string? MnuText { get; set; }
        public string? MnuMTBR { get; set; }
        public bool IsBtnInc { get; set; }
        public string? PageName { get; set; }
    }
}
