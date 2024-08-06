namespace Fiap.TasteEase.Application.Ports;

public interface IPublisher
{
    Task Pub<T>(T content, string exchangeName);
}