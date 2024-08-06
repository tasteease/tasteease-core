using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyNetQ;
using EasyNetQ.Topology;
using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.RabbitMq.Consumers;
using Fiap.TasteEase.RabbitMq.Publishers;
using Microsoft.AspNetCore.Builder;

namespace Fiap.TasteEase.RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var bus = RabbitHutch.CreateBus(configuration.GetConnectionString("RabbitMq"));
        services.AddSingleton(bus);
        
        services.AddScoped<IPublisher, Publisher>();
        services.AddScoped<IConsumer, Consumer>();

        var config = bus.Advanced;
        var startOrderExchange = config.ExchangeDeclare(RabbitConstants.StartOrderExchange, ExchangeType.Fanout);
        var paymentWebhookExchange = config.ExchangeDeclare(RabbitConstants.PaymentWebhookExchange, ExchangeType.Fanout);
        var notifyClientExchange = config.ExchangeDeclare(RabbitConstants.NotifyClientExchange, ExchangeType.Fanout);
        var startOrderQueue = config.QueueDeclare(RabbitConstants.StartOrderQueue);
        var paymentWebhookQueue = config.QueueDeclare(RabbitConstants.PaymentWebhookQueue);
        var notifyClientQueue = config.QueueDeclare(RabbitConstants.NotifyClientQueue);
        config.Bind(startOrderExchange, startOrderQueue, "");
        config.Bind(paymentWebhookExchange, paymentWebhookQueue, "");
        config.Bind(notifyClientExchange, notifyClientQueue, "");
        
        return services;
    }
    
    public static WebApplication UseRabbitMq(this WebApplication app)
    {
        using var scoped = app.Services.CreateScope();
        var provider = scoped.ServiceProvider;
        var bus = provider.GetRequiredService<IBus>().Advanced;

        var startOrderQueue = bus.QueueDeclare(RabbitConstants.StartOrderQueue);
        var paymentWebhookQueue = bus.QueueDeclare(RabbitConstants.PaymentWebhookQueue);
        var notifyClientQueue = bus.QueueDeclare(RabbitConstants.NotifyClientQueue);
        
        bus.Consume(startOrderQueue, (body, _, _) => Task.Factory.StartNew(() =>
        {
            using var serviceScope = app.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var consumer = serviceProvider.GetRequiredService<IConsumer>();
            var message = Encoding.UTF8.GetString(body.ToArray());
            consumer.Handle(message, RabbitQueue.StartOrder);
        }));
        
        bus.Consume(paymentWebhookQueue, (body, _, _) => Task.Factory.StartNew(() =>
        {
            using var serviceScope = app.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var consumer = serviceProvider.GetRequiredService<IConsumer>();
            var message = Encoding.UTF8.GetString(body.ToArray());
            consumer.Handle(message, RabbitQueue.PaymentWebhook);
        }));
        
        bus.Consume(notifyClientQueue, (body, _, _) => Task.Factory.StartNew(() =>
        {
            using var serviceScope = app.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var consumer = serviceProvider.GetRequiredService<IConsumer>();
            var message = Encoding.UTF8.GetString(body.ToArray());
            consumer.Handle(message, RabbitQueue.NotifyClient);
        }));
        
        return app;
    }
}