using Powdernaut.Application.Providers.Serializer.Objects;
using Microsoft.Extensions.DependencyInjection;

namespace Powdernaut.Application.Providers.Serializer;

public static class DependencyInjection
{
    public static IServiceCollection AddMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IObjectSerializer, MicrosoftSerializer>();

    public static IServiceCollection AddNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IObjectSerializer, NewtonSoftSerializer>();
}
