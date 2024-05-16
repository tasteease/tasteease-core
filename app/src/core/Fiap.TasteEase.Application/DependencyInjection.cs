using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.TasteEase.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        return services;
    }
}