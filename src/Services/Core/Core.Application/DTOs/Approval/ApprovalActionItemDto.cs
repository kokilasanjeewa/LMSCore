using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Approval
{
    public class ApprovalActionItemDto
    {
        public int SerialID { get; set; }
        public int ApprovalStepID { get; set; }
        public int ApprovalRequestID { get; set; }
        public string? TypeCategory { get; set; }
    }
}
