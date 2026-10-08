using Core.Application.Interfaces;
using Core.Domain.Common;
using Core.Domain.Common.interfaces;
using Core.Infrastructure.Managers;
using Core.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddTransient<IEmailService, EmailService>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<TheNumbersService>()
                .AddSingleton<SessionManager>()
                .AddHttpContextAccessor();
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, QueryStringUserIdProvider>();
        }
    }
     public class HeaderUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Access the "userID" from headers
            if (connection.GetHttpContext().Request.Headers.TryGetValue("userID", out var userId))
            {
                return userId.ToString();
            }

            // Return null or handle cases where "userID" is missing
            return null;
        }
    }
    public class QueryStringUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var result = connection.GetHttpContext().Request.Query["userId"];
            // Access the "userId" query string parameter
            return connection.GetHttpContext().Request.Query["userId"].ToString();
        }
    }
}
// .AddScoped<TenantProvider>()
