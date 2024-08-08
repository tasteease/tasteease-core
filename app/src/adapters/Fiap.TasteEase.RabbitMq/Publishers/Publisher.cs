using EasyNetQ;
using EasyNetQ.Topology;
using Fiap.TasteEase.Application.Ports;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fiap.TasteEase.RabbitMq.Publishers;

public class Publisher : IPublisher
{
    private readonly IBus _bus;
    
    public Publisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Pub<T>(T content, string exchangeName)
    {
        var message = new Message<T>(content);
        var exchange = await _bus.Advanced.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout);
        await _bus.Advanced.PublishAsync(exchange, "", false, message);
    }
}