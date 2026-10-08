using AutoMapper;
using Core.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Core.Application.DTOs.Approval
{
    public class GetWorkFlowsWithPaginationDto 
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public int ApprovalWorkflowID { get; set; }
        [Required]

        public int EntityTypeID { get; set; }
        [Required]
        public string? EntityType  { get; set; }
        [Required]
        public int ComSerialID { get; set; }
        public string? CompanyName { get; set; }
        public string? ComCode { get; set; }
        [Required]
        public string WorkflowCode { get; set; } = null!;
        [Required]
        public string WorkflowName { get; set; } = null!;

        // 🔴 This maps to SQL JSON
        [JsonIgnore]
        public string? ApprovalStepsJson { get; set; }
        public List<ApprovalStepDto> ApprovalSteps { get; set; } = new();
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Created { get; set; }
        public string? Modified { get; set; }
        public bool Active { get; set; }

    }

}
