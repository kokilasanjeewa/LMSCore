
using Core.Domain.Common;
using Core.Domain.Entities;

namespace Core.Application.Features.Users.Commands.CreateUser
{
    public class UserLogoutEvent : BaseEvent
    {
        public InvalidateToken _invalidateToken { get; }

        public UserLogoutEvent(InvalidateToken invalidateToken)
        {
            _invalidateToken = invalidateToken;
        }
    }
}
