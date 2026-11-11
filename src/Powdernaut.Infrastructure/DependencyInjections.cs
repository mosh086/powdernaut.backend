using Powdernaut.Application.Common.Repository;
using Powdernaut.Application.Common.Service;
using Powdernaut.Application.Interfaces;
using Powdernaut.Application.Providers.CacheSystem;
using Powdernaut.Application.UseCases.Identity.Account.Services;
using Powdernaut.Domain.UseCases.Security;
using Powdernaut.Infrastructure.Data;
using Powdernaut.Infrastructure.Data.Interceptors;
using Powdernaut.Infrastructure.Identity;
using Powdernaut.Infrastructure.Providers.CacheSystem.InMemory;
using Powdernaut.Infrastructure.Providers.DataDapper;
using Powdernaut.Infrastructure.Providers.Scrutor;
using Powdernaut.Infrastructure.Services;
using Powdernaut.Infrastructure.UseCases.Identity.Account.Services;

namespace Powdernaut.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureLibrary(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
    {
        return services
            .AddScrutor(configuration, assemblies, [typeof(IRepository<,>), typeof(IEntityService<,,>)])
            .AddDatabase(configuration)
            .AddDatabaseInterceptors()
            .AddInMemoryCaching()
            .AddIdentityConfigurations()
            .AddDataDapper(configuration);
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PowdernautDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                   .AddInterceptors(new AddAuditDataInterceptor());
        });

        // Register the password hasher
        services.AddScoped<IPasswordHasher<AppUserEntity>, PasswordHasher<AppUserEntity>>();

        services.AddTransient<IIdentityFactory, IdentityFactory>();
        services.AddScoped<IIdentityUserService, IdentityUserService>();
        services.AddScoped<IIdentityRoleService, IdentityRoleService>();
        services.AddScoped<ISignInService, SignInService>();
        return services;
    }

    private static IServiceCollection AddDatabaseInterceptors(this IServiceCollection services)
    {
        services.AddScoped<ISaveChangesInterceptor, AddAuditDataInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        return services;
    }

    private static IServiceCollection AddInMemoryCaching(this IServiceCollection services)
    {
        return services
            .AddMemoryCache()
            .AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
    }
}