using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result<OrderResponseCommand>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponseCommand>> Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var validStatus = new List<OrderStatus> { OrderStatus.Finished, OrderStatus.Delivered, OrderStatus.Prepared, OrderStatus.Preparing };
        if (!validStatus.Contains(request.Status)) return Result.Fail("não é possível alterar para essa situação");

        var orderResult = await _orderRepository.GetById(request.OrderId);

        if (orderResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var order = orderResult.ValueOrDefault;
        var totalPrice = order.GetTotalPrice(order.Foods.Select(s => s.Food).ToList());

        order.UpdateStatus(request.Status);
        _orderRepository.Update(order);
        await _orderRepository.SaveChanges();
        return Result.Ok(new OrderResponseCommand(order.Id.Value, order.ClientId, totalPrice.ValueOrDefault,
            order.Status));
    }
}