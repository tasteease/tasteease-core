using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetAll;

public class GetOrderAllHandler : IRequestHandler<GetOrderAllQuery, Result<IEnumerable<OrderResponseQuery>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderAllHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<IEnumerable<OrderResponseQuery>>> Handle(GetOrderAllQuery request,
        CancellationToken cancellationToken)
    {
        var ordersResult = await _orderRepository.GetByFilters(request.Status, request.ClientId);

        if (ordersResult.IsFailed)
            return Result.Fail("Erro ao obter os pedidos");

        var orders = ordersResult.ValueOrDefault;
        var response = orders.Adapt<IEnumerable<OrderResponseQuery>>();
        return Result.Ok(response);
    }
}