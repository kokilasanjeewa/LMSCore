using Core.Domain.Common;

namespace Core.Application.Features.DocType.Command
{

    public class DocTypeCreatedEvent : BaseEvent
    {
        public Domain.Entities.DocType DocType { get; }

        public DocTypeCreatedEvent(Domain.Entities.DocType docType)
        {
            DocType = docType;
        }
    }
}
