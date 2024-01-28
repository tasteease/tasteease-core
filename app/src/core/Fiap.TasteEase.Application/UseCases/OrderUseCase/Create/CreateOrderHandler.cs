using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<OrderResponseCommand>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(IMediator mediator, IOrderRepository orderRepository, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
        _foodRepository = foodRepository;
    }

    public async Task<Result<OrderResponseCommand>> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var orderProps = request.Adapt<CreateOrderProps>();
        var orderResult = Order.Create(orderProps);

        if (orderResult.IsFailed)
            return Result.Fail("Erro ao registrar o pedido");

        var order = orderResult.ValueOrDefault;

        if (request.Foods?.Any() ?? false)
        {
            var orderFoods = request.Foods.Adapt<List<OrderFood>>();
            order.AddFood(orderFoods);
        }

        var result = _orderRepository.Add(order);
        await _orderRepository.SaveChanges();

        var foodIds = order.Foods.Select(s => s.FoodId);
        var foodsResult = await _foodRepository.GetByIds(foodIds);
        var totalPrice = order.GetTotalPrice(foodsResult.ValueOrDefault).ValueOrDefault;

        if (orderResult.IsFailed)
            return Result.Fail("Erro ao calcular o valor");

        return Result.Ok(new OrderResponseCommand(order.Id.Value, order.ClientId, totalPrice, order.Status));
    }
}