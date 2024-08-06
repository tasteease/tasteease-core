namespace Fiap.TasteEase.RabbitMq.Consumers;

public interface IConsumer
{
    Task<bool> Handle(string message, RabbitQueue type);
}