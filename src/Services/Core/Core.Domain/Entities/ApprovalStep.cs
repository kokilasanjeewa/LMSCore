using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalStep : BaseAuditableEntity
    {
        [Key]
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
        public bool IsMandatory { get; set; } = true;
        public bool CanReject { get; set; } = true;
        public bool IsFinalStep { get; set; }

        /* Navigation */
        public virtual ApprovalWorkflow ApprovalWorkflow { get; set; } = null!;
        /* Navigation for actual approvers (user-based) */
        public virtual ICollection<ApprovalStepApprover> Approvers { get; set; }
            = new HashSet<ApprovalStepApprover>();
    }

}
