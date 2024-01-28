using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries;

public class GetlOrderAllHandler : IRequestHandler<GetOrderAllQuery, Result<IEnumerable<OrderResponseQuery>>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public GetlOrderAllHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
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