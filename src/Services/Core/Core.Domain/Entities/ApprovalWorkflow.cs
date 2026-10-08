using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalWorkflow : BaseAuditableEntity
    {
        [Key]
        public int ApprovalWorkflowID { get; set; }

        [Required]
        public int EntityTypeID { get; set; }     // Supplier, PO
        public int ComSerialID { get; set; }
        // Company scope
        [Required]
        [StringLength(50, ErrorMessage = "The code cannot exceed 50 characters. ")]
        public string? WorkflowCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The name cannot exceed 100 characters. ")]
        public string? WorkflowName { get; set; } 

        /* Navigation */
        public virtual EntityType? EntityType { get; set; }
        public virtual ICollection<ApprovalStep> ApprovalSteps { get; set; }
            = new HashSet<ApprovalStep>();
    }

}
