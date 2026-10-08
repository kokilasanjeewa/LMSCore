
using Core.Domain.Common;
using Core.Domain.Entities;

namespace Core.Application.Features.Users.Commands.CreateUser
{
    public class UserCreatedEvent : BaseEvent
    {
        public User User { get; }

        public UserCreatedEvent(User user)
        {
            User = user;
        }
    }
}
