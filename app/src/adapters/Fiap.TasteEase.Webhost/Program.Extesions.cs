using System.Reflection;
using Fiap.TasteEase.Infra;
using Mapster;

namespace Fiap.TasteEase.Api;

public static class Program
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        var mappersAssemblies = Array.Empty<Assembly>();

        mappersAssemblies = mappersAssemblies.Append(typeof(DependencyInjection).Assembly).ToArray();
        mappersAssemblies = mappersAssemblies.Append(typeof(Application.DependencyInjection).Assembly).ToArray();

        config.Scan(mappersAssemblies);
        config.Default.AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);
        config.Compile();

        services.AddSingleton(config);

        return services;
    }
}