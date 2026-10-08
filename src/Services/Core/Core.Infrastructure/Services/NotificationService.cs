using Core.Application.Interfaces;
using Core.Infrastructure.Hubs;
using Core.Infrastructure.Managers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly SessionManager _sessionManager;

        public NotificationService(IHubContext<NotificationHub> notificationHub, SessionManager sessionManager)
        {
            _notificationHub = notificationHub;
            _sessionManager = sessionManager;
        }

        public async Task SendNotificationAsync(string userId, string message)
        {
            // Retrieve the connection ID for the specified userId from SessionManager
            var sessionInfo = _sessionManager.GetSession(userId);

            if (sessionInfo.HasValue)
            {
                var connectionId = sessionInfo.Value.ConnectionId;
                // Send the notification to the specific connection
                await _notificationHub.Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
            }
        }
    }
}
