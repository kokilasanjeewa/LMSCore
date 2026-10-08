using Core.Domain.Common;


namespace Core.Application.Features.Companies.Command
{
     public class CompanyCreatedEvent : BaseEvent
    {
        public Domain.Entities.Company Company { get; }

        public CompanyCreatedEvent(Domain.Entities.Company company)
        {
            Company = company;
        }
    }
}
