
using System.ComponentModel.DataAnnotations;


namespace Core.Application.DTOs.Approval
{
    public class ApprovalStepDto
    {
        [Required]
        public int ApprovalStepID { get; set; }

        [Required]
        public int ApprovalWorkflowID { get; set; }

        [Required]
        public int StepOrder { get; set; }

        [Required, MaxLength(20)]
        public string? StepCode { get; set; }  // FIN, COMP, MGMT

        [Required, MaxLength(100)]
        public string? StepName { get; set; } 

        [Required, MaxLength(100)]
        public string? ApprovalRole { get; set; }     // Example: FINANCE_MANAGER, PLANT_MANAGER

        public int? PlantID { get; set; }
        // NULL = applies to ALL plants
        public bool IsMandatory { get; set; } 
        public bool CanReject { get; set; } 
        public bool IsFinalStep { get; set; }

    }

}
