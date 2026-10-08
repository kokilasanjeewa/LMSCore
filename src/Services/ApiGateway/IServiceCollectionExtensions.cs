
using OcelotApiGateway.Decorator;

namespace OcelotApiGateway
{
    public static class IServiceCollectionExtensions
    {
    public static void DecorateClaimAuthoriser(this IServiceCollection services)
    {
        var serviceDescriptor = services.First(x => x.ServiceType == typeof(Ocelot.Authorization.IClaimsAuthorizer));
        services.Remove(serviceDescriptor);

        var newServiceDescriptor = new ServiceDescriptor(serviceDescriptor.ImplementationType, serviceDescriptor.ImplementationType, serviceDescriptor.Lifetime);
        services.Add(newServiceDescriptor);

        services.AddTransient<Ocelot.Authorization.IClaimsAuthorizer, ClaimAuthorizerDecorator>();

       // return services;
    }
}
}
