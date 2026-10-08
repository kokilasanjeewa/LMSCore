using Core.Application.Features.Approval.Queries.GetWorkFlowsWithPagination;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Approval.Queries.GetWorkFlow
{
    public class GetWorkFlowWithValidator : AbstractValidator<GetWorkFlowWithQuery>
    {
        public GetWorkFlowWithValidator()
        {
            RuleFor(x => x.ApprovalWorkflowID)
                .GreaterThan(0)
                .WithMessage("Work flow missing.");
        }
    }
}
