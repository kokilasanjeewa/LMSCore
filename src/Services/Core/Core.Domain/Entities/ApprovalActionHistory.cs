using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalActionHistory : BaseAuditableEntity
    {
        [Key]
        public long ApprovalActionID { get; set; }

        public int ApprovalRequestID { get; set; }
        public int ApprovalStepID { get; set; }

        public ApprovalActionType Action { get; set; }

        public int ActionByUserSerialID { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(250)]
        public string? Remarks { get; set; }

        public ApprovalState PreviousState { get; set; }
        public ApprovalState NewState { get; set; }
    }
    // Optional improvement for clarity
    public enum ApprovalState : byte
    {
        Draft = 0,
        Submitted = 1,
        InProgress = 2,      // overall workflow started
        Approved = 3,        // all steps approved
        Rejected = 4,        // any step rejected
        Blocked = 5
    }

    // For steps that haven't started yet
    public enum ApprovalStepStatus : byte
    {
        NotStarted = 0,      // future step
        Pending = 1,         // current active step
        Approved = 2,
        Rejected = 3,
        Held = 4
    }

    // Action types match step actions
    public enum ApprovalActionType : byte
    {
        Approve = 1,
        Reject = 2,
        Hold = 3
    }

}
