using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.DocType.Command
{
    public class DocTypeUpdatedEvent : BaseEvent
    {
        public Domain.Entities.DocType DocType { get; }

        public DocTypeUpdatedEvent(Domain.Entities.DocType docType)
        {
            DocType = docType;
        }
    }
}
