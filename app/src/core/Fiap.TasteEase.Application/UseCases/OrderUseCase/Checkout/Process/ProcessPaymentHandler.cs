using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout.Process;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Result<string>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public ProcessPaymentHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<string>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var orderResult = await _orderRepository.GetByPaymentReference(request.Reference);

        if (orderResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var order = orderResult.ValueOrDefault;

        order.ProcessPayment(request.Reference, request.Paid, request.PaidDate);

        if (request.Paid)
            order.UpdateStatus(OrderStatus.Paid);

        _orderRepository.Update(order);
        await _orderRepository.SaveChanges();
        return Result.Ok("processado com sucesso");
    }
}