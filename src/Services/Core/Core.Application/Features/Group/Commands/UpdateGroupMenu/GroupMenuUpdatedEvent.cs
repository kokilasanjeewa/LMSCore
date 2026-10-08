using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Commands.CreateGroup
{
    public class GroupMenuUpdatedEvent : BaseEvent
    {
        public Domain.Entities.GroupMenu GroupMenu { get; }

        public GroupMenuUpdatedEvent(Domain.Entities.GroupMenu groupMenu)
        {
            GroupMenu = groupMenu;
        }
    }
}
