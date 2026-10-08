using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Approval
{
    public class ApprovalStepCreateDto
    {
        /// <summary>
        /// The workflow this step belongs to
        /// </summary>
        public int ApprovalWorkflowID { get; set; }

        /// <summary>
        /// Step sequence number
        /// </summary>
        public int StepOrder { get; set; }

        /// <summary>
        /// Short code for the step (FIN, COMP, MGMT)
        /// </summary>
        public string StepCode { get; set; } = null!;

        /// <summary>
        /// Step name
        /// </summary>
        public string StepName { get; set; } = null!;

        /// <summary>
        /// Role responsible for approval (e.g., FINANCE_MANAGER)
        /// </summary>
        public string ApprovalRole { get; set; } = null!;

        /// <summary>
        /// Optional plant ID
        /// </summary>
        public int? PlantID { get; set; }

        /// <summary>
        /// Is this step mandatory?
        /// </summary>
        public bool IsMandatory { get; set; } = true;

        /// <summary>
        /// Can this step reject the request?
        /// </summary>
        public bool CanReject { get; set; } = true;

        /// <summary>
        /// Is this the final step?
        /// </summary>
        public bool IsFinalStep { get; set; } = false;
    }

}
