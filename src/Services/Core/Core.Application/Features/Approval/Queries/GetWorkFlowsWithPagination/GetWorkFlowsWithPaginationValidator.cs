using FluentValidation;


namespace Core.Application.Features.Approval.Queries.GetWorkFlowsWithPagination
{
    public class GetWorkFlowsWithPaginationValidator : AbstractValidator<GetWorkFlowsWithPaginationQuery>
    {
        public GetWorkFlowsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
