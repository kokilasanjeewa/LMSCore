using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalRequestStep : BaseAuditableEntity
    {
        [Key]
        public int ApprovalRequestStepID { get; set; }

        [Required]
        public int ApprovalRequestID { get; set; }

        [Required]
        public int ApprovalStepID { get; set; }

        public int StepOrder { get; set; }

        public ApprovalStepStatus StepStatus { get; set; }

        public int? AssignedUserSerialID { get; set; }

        public DateTime? ActionDate { get; set; }

        /* Navigation */
        public virtual ApprovalRequest ApprovalRequest { get; set; } = null!;
        public virtual ApprovalStep ApprovalStep { get; set; } = null!;
    }

}
