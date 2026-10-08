using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class BankBranch : BaseAuditableEntity
    {
        [Key]
        public int BankBrnchSerialID { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The bank code cannot exceed 15 characters. ")]
        public string? BankCode { get; set; }
        [Required]
        [StringLength(4, ErrorMessage = "The branch code cannot exceed 4 characters. ")]
        public string? BranchCode { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "The branch address cannot exceed 75 characters. ")]
        public string? BranchName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The branch address cannot exceed 100 characters. ")]
        public string? BranchAddress { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone number  cannot exceed 10 characters. ")]
        public string? Telephone1 { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone number  cannot exceed 10 characters. ")]
        public string? Telephone2 { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone number  cannot exceed 10 characters. ")]
        public string? Telephone3 { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "The telephone number  cannot exceed 10 characters. ")]
        public string? Telephone4 { get; set; }
        [StringLength(10, ErrorMessage = "The fax number  cannot exceed 10 characters. ")]
        public string? FaxNo { get; set; }
        public int? DistrictSerialID {  get; set; }
        [Required]
        public int BankSerialID { get; set; }

        [ForeignKey(nameof(BankSerialID))]
        public virtual Bank Bank { get; set; } = null!;

    }
}
//https://medium.com/@e.demir/asp-net-core-api-fluent-validation-2e9a5be058e9
//https://medium.com/@e.demir/naming-convention-9b9663d5fb8a
// Convert enum to number
/*BloodGroup myBloodGroup = BloodGroup.BPlus;
int numericValue = (int)myBloodGroup;
Console.WriteLine($"The numeric value of {myBloodGroup} is {numericValue}");*/

// Convert number to enum
/*int numericBloodGroup = 4;
BloodGroup bloodGroupFromNumber = (BloodGroup)numericBloodGroup;
Console.WriteLine($"The blood group for numeric value {numericBloodGroup} is {bloodGroupFromNumber}");*/