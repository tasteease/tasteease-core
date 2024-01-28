using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase;

public class CreateOrderHandler : IRequestHandler<Create, Result<OrderResponseCommand>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;
    private readonly IFoodRepository _foodRepository;

    public CreateOrderHandler(IMediator mediator, IOrderRepository orderRepository, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
        _foodRepository = foodRepository;
    }

    public async Task<Result<OrderResponseCommand>> Handle(Create request, CancellationToken cancellationToken)
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

public class GetlAllOrderHandler : IRequestHandler<GetAll, Result<IEnumerable<OrderResponseQuery>>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public GetlAllOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<IEnumerable<OrderResponseQuery>>> Handle(GetAll request, CancellationToken cancellationToken)
    {
        var ordersResult = await _orderRepository.GetByFilters(request.Status, request.ClientId);
        
        if (ordersResult.IsFailed)
            return Result.Fail("Erro ao obter os pedidos");

        var orders = ordersResult.ValueOrDefault;
        var response = orders.Adapt<IEnumerable<OrderResponseQuery>>();
        return Result.Ok(response);
    }
}

public class GetByIdHandler : IRequestHandler<GetById, Result<OrderResponseQuery>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public GetByIdHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponseQuery>> Handle(GetById request, CancellationToken cancellationToken)
    {
        var ordersResult = await _orderRepository.GetById(request.OrderId);
        
        if (ordersResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var orders = ordersResult.ValueOrDefault;
        var response = orders.Adapt<OrderResponseQuery>();
        return Result.Ok(response);
    }
}

public class UpdateOrderStatusHandler : IRequestHandler<UpdateStatus, Result<OrderResponseCommand>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderStatusHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderResponseCommand>> Handle(UpdateStatus request, CancellationToken cancellationToken)
    {
        var validStatus = new List<OrderStatus> { OrderStatus.Delivered, OrderStatus.Prepared, OrderStatus.Preparing };
        if (!validStatus.Contains(request.Status)) return Result.Fail("não é possível alterar para essa situação");
        
        var orderResult = await _orderRepository.GetById(request.OrderId);
        
        if (orderResult.IsFailed)
            return Result.Fail("não foi encontrado");

        var order = orderResult.ValueOrDefault;
        var totalPrice = order.GetTotalPrice(order.Foods.Select(s => s.Food).ToList());

        order.UpdateStatus(request.Status);
        _orderRepository.Update(order);
        await _orderRepository.SaveChanges();
        return Result.Ok(new OrderResponseCommand(order.Id.Value, order.ClientId, totalPrice.ValueOrDefault, order.Status));
    }
}

public class PayOrderHandler : IRequestHandler<Pay, Result<OrderPaymentResponseCommand>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public PayOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderPaymentResponseCommand>> Handle(Pay request, CancellationToken cancellationToken)
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

public class ProcessPaymentOrderHandler : IRequestHandler<ProcessPayment, Result<string>>
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public ProcessPaymentOrderHandler(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator;
        _orderRepository = orderRepository;
    }

    public async Task<Result<string>> Handle(ProcessPayment request, CancellationToken cancellationToken)
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