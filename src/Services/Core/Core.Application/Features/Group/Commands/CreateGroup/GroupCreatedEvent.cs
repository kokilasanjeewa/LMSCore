using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Commands.CreateGroup
{
    public class GroupCreatedEvent : BaseEvent
    {
        public Domain.Entities.Group Group { get; }

        public GroupCreatedEvent(Domain.Entities.Group group)
        {
            Group = group;
        }
    }
}
