using HCM.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApplicationService.Models
{
    public class GetEmployeeWithEmployeeIDDto
    {
        [Required]
        public long EESerialID { get; set; }
        [Required]
        public long EEID { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? CallName { get; set; }
        [Required]
        public int? ComSerialID { get; set; }
        [Required]
        public int? DesigSerialID { get; set; }
        public int? DeptSerialID { get; set; }
        public int? SectSerialID { get; set; }
        public CompanyCode? CompanyCode { get; set; }
        public int? EmpNo { get; set; }
        public string? bindName_NameEmpNo { get; set; }

    }
}
