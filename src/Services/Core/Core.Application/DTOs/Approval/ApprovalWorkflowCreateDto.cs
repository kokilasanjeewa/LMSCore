

using AutoMapper;
using Core.Application.Common.Mappings;
using Core.Application.Features.Companies.Command;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.DTOs.Approval
{
    public class ApprovalWorkflowCreateDto:IMapFrom<Domain.Entities.ApprovalWorkflow>
    {
        /// <summary>
        /// Name of the workflow (e.g., Supplier Approval)
        /// </summary>
        public string WorkflowName { get; set; } = null!;

        /// <summary>
        /// Unique code (e.g., SUPPLIER_APPROVAL)
        /// </summary>
        public string WorkflowCode { get; set; } = null!;

        /// <summary>
        /// Entity type this workflow applies to (Supplier, Customer, PO)
        /// </summary>
        public int EntityTypeID { get; set; }

        /// <summary>
        /// Optional company scope
        /// </summary>
        public int CompanyID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompanyCommand, Domain.Entities.ApprovalWorkflow>();
        }
    }
    
}
