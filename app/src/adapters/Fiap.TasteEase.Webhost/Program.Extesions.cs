using System.Reflection;
using Fiap.TasteEase.Domain.DTOs;
using Mapster;

namespace Fiap.TasteEase.Webhost;

public static class Program
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        var mappersAssemblies = Array.Empty<Assembly>();

        mappersAssemblies = mappersAssemblies.Append(typeof(Api.DependencyInjection).Assembly).ToArray();
        mappersAssemblies = mappersAssemblies.Append(typeof(Application.DependencyInjection).Assembly).ToArray();
        mappersAssemblies = mappersAssemblies.Append(typeof(Infra.DependencyInjection).Assembly).ToArray();

        config.Scan(mappersAssemblies);
        config.Default.AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);
        config.Compile();

        services.AddSingleton(config);

        return services;
    }

    public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder builder)
    {
        var secretPath = Environment.GetEnvironmentVariable("SECRETS_PATH") ?? "";
        builder.Configuration.AddJsonFile($"{secretPath}appsettings.json", true);
        builder.Services.Configure<AwsSettings>(builder.Configuration.GetSection("AWS"));
        return builder;
    }
}