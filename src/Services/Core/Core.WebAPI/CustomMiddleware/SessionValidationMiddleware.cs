using Core.Application.Interfaces.Repositories;
using Core.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;



namespace Core.WebAPI.CustomMiddleware
{
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider serviceProvider;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public SessionValidationMiddleware(RequestDelegate next,IServiceProvider serviceProvider, IHubContext<NotificationHub> notificationHub)
        {
            _next = next;
            this.serviceProvider = serviceProvider;
            _notificationHub = notificationHub;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Request.Headers["userID"].ToString();
            var sessionId = context.Request.Headers["Session-Id"].FirstOrDefault();
    
            if (userId != null && sessionId != null)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                    // Use userRepository within this scope
                    var user = await userRepository.GetUserByUserName(loginName: userId, cancellationToken: CancellationToken.None);
                    if (user != null && user.LastSessionId != sessionId)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Session is invalid due to a different login.");
                        // Send real-time notification to user via SignalR
                        await _notificationHub.Clients.User(userId).SendAsync("ReceiveNotification", "You have been logged out due to a new login from another device.");
                        return;
                    }
                }


            }

            await _next(context);
        }
    }

}
