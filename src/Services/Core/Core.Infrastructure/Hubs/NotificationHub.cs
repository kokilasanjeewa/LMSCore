namespace Core.Infrastructure.Hubs
{
    using Core.Infrastructure.Managers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider serviceProvider;
        private readonly SessionManager _sessionManager;

        public NotificationHub(IHttpContextAccessor httpContextAccessor, IServiceProvider _serviceProvider, SessionManager sessionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            serviceProvider = _serviceProvider;
            _sessionManager = sessionManager;
        }
         // Handling sending messages between users
        public async Task SendMessage(string userId,string messageRequest)
        {
            try
            {
                // Send message to all users in the receiver list
                await Clients.User(userId).SendAsync("ReceiveNotification", messageRequest);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error sending message: {ex.Message}");
            }

        }
        // This method is automatically called when a new client connects to the hub.
        public override async Task OnConnectedAsync()
        {

            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"].ToString();
            var sessionId = httpContext.Request.Query["sessionId"].ToString();
            var existingSession = _sessionManager.GetSession(userId);
            // Check if there is a proxy and get the IP from the X-Forwarded-For header (if available)
            // var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            var ipAddress = GetClientIpAddress(httpContext);

            // Get the server's machine name (local machine name)
              // string machineName = Environment.MachineName;
             string machineName = System.Net.Dns.GetHostName();  // Local machine name

            if (existingSession != null && existingSession.Value.SessionId != sessionId)
            {
                // Notify previous session if user is logging in with a different session ID
                await Clients.Client(existingSession.Value.ConnectionId)
                    .SendAsync("ReceiveNotification", $"There is new sign-in to your account from {ipAddress} + {machineName}.You have to sign in again to continue.");
                // Remove old session
                _sessionManager.RemoveSession(userId);
            }

            // Add or update the current session
            _sessionManager.AddOrUpdateSession(userId, sessionId, Context.ConnectionId);

        
            await base.OnConnectedAsync();
        }
        // This method is automatically called when a client disconnects
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"].ToString();
            var sessionId = httpContext.Request.Query["sessionId"].ToString();
            if (userId != null)
            {
                // Log the disconnection or perform other actions when the user disconnects
                Console.WriteLine($"User {userId} disconnected.");
            }

            await base.OnDisconnectedAsync(exception);
        }
        private string GetClientIpAddress(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;

            if (ipAddress == null)
            {
                return "IP not found";
            }

            // Check if it's an IPv4-mapped IPv6 address
            if (ipAddress.IsIPv4MappedToIPv6)
            {
                // Convert to IPv4 format
                return ipAddress.MapToIPv4().ToString();
            }

            // Otherwise, return the IP address as-is
            return ipAddress.ToString();
        }
    }


}
