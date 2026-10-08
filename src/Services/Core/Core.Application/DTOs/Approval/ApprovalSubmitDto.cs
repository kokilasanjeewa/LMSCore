
using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.Approval
{
    public class ApprovalSubmitDto
    {
        /// <summary>
        /// Entity type (Supplier, Customer, PO, etc.)
        /// Example: 1 = SUPPLIER
        /// </summary>
        public int EntityTypeID { get; set; }
        [Required]
        public string? EntityType { get; set; }

        /// <summary>
        /// Primary key of the entity
        /// Example: SupplierID = 100245
        /// </summary>
        public int EntityID { get; set; }

        /// <summary>
        /// Approval workflow to be applied
        /// Example: Supplier Approval Workflow ID
        /// </summary>
        public int WorkflowID { get; set; }

        /// <summary>
        /// Company / Legal entity
        /// Example: 1
        /// </summary>
        [Required]
        public int CompanyID { get; set; }

        /// <summary>
        /// Optional plant / location
        /// Example: 10 (Katunayake Plant)
        /// </summary>
        public int? PlantID { get; set; }

        /// <summary>
        /// Logged-in user submitting the request
        /// Example: 5 (Procurement Officer)
        /// </summary>
        [Required]
        public int UserID { get; set; }
    }

}
