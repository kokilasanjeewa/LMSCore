using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Approval
{
    public class ApproveActionResultDto
    {
        [Required]
        public Int64 SerialID { get; set; }
        public bool IsCompleted { get; set; }
        public string? TypeCategory { get; set; }
    }
 
}
