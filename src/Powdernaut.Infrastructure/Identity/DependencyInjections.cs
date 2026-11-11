using Powdernaut.Infrastructure.Identity.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace Powdernaut.Infrastructure.Identity;

public static class DependencyInjections
{
    public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
    {

        services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        return services;
    }
}