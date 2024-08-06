using System.Text.Json;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.RabbitMq.Models;
using MediatR;
using IPublisher = Fiap.TasteEase.Application.Ports.IPublisher;

namespace Fiap.TasteEase.RabbitMq.Consumers;

public class Consumer : IConsumer
{
    private readonly IPublisher _publisher;
    private readonly IMediator _mediator;
    
    public Consumer(IPublisher publisher, IMediator mediator)
    {
        _publisher = publisher;
        _mediator = mediator;
    }
    
    public async Task<bool> Handle(string message, RabbitQueue type) =>
        type switch
        {
            RabbitQueue.StartOrder => await StartOrder(message),
            RabbitQueue.PaymentWebhook => await UpdatePayment(message),
            RabbitQueue.NotifyClient => await NotifyClient(message),
            _ => throw new NotImplementedException()
        };
    

    private async Task<bool> UpdatePayment(string message)
    {
        try
        {
            var model = JsonSerializer.Deserialize<PaymentModel>(message);

            if (model?.Paid ?? false)
            {
                await _mediator.Send(new UpdateOrderCommand { OrderId = model.OrderId, Status = OrderStatus.Paid });
                await _publisher.Pub(new OrderModel { OrderId = model.OrderId }, RabbitConstants.StartOrderExchange);
            }
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    private async Task<bool> StartOrder(string message)
    {
        try
        {
            var model = JsonSerializer.Deserialize<OrderModel>(message);
            await _mediator.Send(new UpdateOrderCommand { OrderId = model.OrderId, Status = OrderStatus.Preparing });
            
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    private async Task<bool> NotifyClient(string message)
    {
        try
        {
            var model = JsonSerializer.Deserialize<OrderModel>(message);
            await _mediator.Send(new UpdateOrderCommand { OrderId = model.OrderId, Status = OrderStatus.Prepared });
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}