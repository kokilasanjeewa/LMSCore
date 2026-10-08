using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalRequest : BaseAuditableEntity
    {
        [Key]
        public int ApprovalRequestID { get; set; }

        [Required]
        public int EntityTypeID { get; set; }

        [Required]
        public int EntityID { get; set; } // SupplierSerialID, CustomerID, POID
        public int ComSerialID { get; set; }
        public int? PlantID { get; set; }
        [Required]
        public int ApprovalWorkflowID { get; set; }

        public int CurrentStepOrder { get; set; }

        [Required]
        public ApprovalState CurrentState { get; set; }

        [Required]
        public int RequestedByUserSerialID { get; set; }

        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; }

        /* Navigation */
        public virtual ApprovalWorkflow ApprovalWorkflow { get; set; } = null!;
        public virtual ICollection<ApprovalRequestStep> RequestSteps { get; set; }
            = new HashSet<ApprovalRequestStep>();
    }

}
