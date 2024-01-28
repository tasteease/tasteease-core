using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Infra.Context;
using Fiap.TasteEase.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.TasteEase.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEfCoreConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                Console.WriteLine($"Using {configuration.GetConnectionString("DefaultConnection")} connection string");
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
                    );
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContextAdapter>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();

            return services;
        }
        
        public static WebApplication UseSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            appContext.Database.Migrate();
            return app;
        }
    }
}
