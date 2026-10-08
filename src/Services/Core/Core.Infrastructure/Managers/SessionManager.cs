namespace Core.Infrastructure.Managers
{
    using System.Collections.Concurrent;

    public class SessionManager
    {
        private readonly ConcurrentDictionary<string, (string SessionId, string ConnectionId)> _userSessions = new();

        // Adds or updates the session information for a user
        public void AddOrUpdateSession(string userId, string sessionId, string connectionId)
        {
            _userSessions[userId] = (sessionId, connectionId);
        }

        // Retrieves the session information for a specific user
        public (string SessionId, string ConnectionId)? GetSession(string userId)
        {
            return _userSessions.TryGetValue(userId, out var session) ? session : null;
        }

        // Removes a user session, which can be used when a session is invalidated
        public bool RemoveSession(string userId)
        {
            return _userSessions.TryRemove(userId, out _);
        }
    }

}
