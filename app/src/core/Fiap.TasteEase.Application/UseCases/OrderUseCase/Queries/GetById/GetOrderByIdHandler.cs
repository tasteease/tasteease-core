using Fiap.TasteEase.Application.Ports;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderResponseQuery>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponseQuery>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var ordersResult = await _orderRepository.GetById(request.OrderId);

        if (ordersResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var orders = ordersResult.ValueOrDefault;
        var response = orders.Adapt<OrderResponseQuery>();
        return Result.Ok(response);
    }
}