using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout;

public class PayOrderHandler : IRequestHandler<PayCommand, Result<OrderPaymentResponseCommand>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public PayOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderPaymentResponseCommand>> Handle(PayCommand request,
        CancellationToken cancellationToken)
    {
        var orderResult = await _orderRepository.GetById(request.OrderId);

        if (orderResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var order = orderResult.ValueOrDefault;

        var payment = order.Pay();
        order.UpdateStatus(OrderStatus.WaitPayment);
        _orderRepository.Update(order);
        await _orderRepository.SaveChanges();
        return Result.Ok(new OrderPaymentResponseCommand(order.Id.Value, payment.ValueOrDefault.PaymentLink));
    }
}