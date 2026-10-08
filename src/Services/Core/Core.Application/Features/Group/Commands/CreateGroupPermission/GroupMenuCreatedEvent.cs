using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Group.Commands.CreateGroupPermission
{
    public class GroupMenuCreatedEvent : BaseEvent
    {
        public Domain.Entities.GroupMenu GroupMenu { get; }

        public GroupMenuCreatedEvent(Domain.Entities.GroupMenu groupMenu)
        {
            GroupMenu = groupMenu;
        }
    }
}
