using Core.Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Core.Domain.Entities
{
    public class ApprovalStepApprover : BaseAuditableEntity
    {
        [Key]
        public int ApprovalStepApproverSerialId { get; set; }

        [Required]
        public int ApprovalStepID { get; set; }

        [Required]
        public int UserSerialID { get; set; }

        public bool IsParallel { get; set; } = false;

        public bool IsActive { get; set; } = true;

        /* Navigation */
        public virtual ApprovalStep ApprovalStep { get; set; } = null!;
    }

}
#region
/*CREATE TABLE [Core].[ApprovalStepApprover]
(
    ApprovalStepApproverSerialId INT IDENTITY(1,1) PRIMARY KEY,
    ApprovalStepID INT NOT NULL,
    UserSerialID INT NOT NULL,
    IsParallel BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,

    -- BaseAuditableEntity columns
    CreatedBy INT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    ModifiedBy INT NULL,
    ModifiedDate DATETIME2 NULL,
    Active BIT NOT NULL DEFAULT 1,
    IsDeleted BIT NOT NULL DEFAULT 0,

    -- Foreign Key Constraint
    CONSTRAINT FK_ApprovalStepApprover_ApprovalStep
        FOREIGN KEY (ApprovalStepID)
        REFERENCES  [Core].[ApprovalStep](ApprovalStepID)
        ON DELETE CASCADE
);

INSERT INTO [CoreDB].[Core].[ApprovalStepApprover]
    ([ApprovalStepID], [UserSerialID], [IsParallel], [IsActive], [CreatedBy], [CreatedDate], [Active], [IsDeleted])
VALUES
-- Step 1: Finance Approval (sequential, 2 users)
(6, 14452, 0, 1, 0, SYSUTCDATETIME(), 1, 0),
(6, 16410, 0, 1, 0, SYSUTCDATETIME(), 1, 0),

-- Step 2: Compliance Approval (single user)
(7, 16410, 0, 1, 0, SYSUTCDATETIME(), 1, 0),

-- Step 3: Management Approval (parallel, 3 users)
(8, 16422, 1, 1, 0, SYSUTCDATETIME(), 1, 0),
(8, 16423, 1, 1, 0, SYSUTCDATETIME(), 1, 0),
(8, 16424, 1, 1, 0, SYSUTCDATETIME(), 1, 0)
*/
#endregion