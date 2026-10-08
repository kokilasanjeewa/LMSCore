
using Core.Domain.Common;
using Core.Domain.Entities;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public class UserUpdatedEvent : BaseEvent
    {
        public User User { get; }

        public UserUpdatedEvent(User user)
        {
            User = user;
        }
    }
}
