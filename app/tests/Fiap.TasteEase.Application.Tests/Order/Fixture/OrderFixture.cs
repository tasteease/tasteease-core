using System.Reflection;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Mapster;

namespace Fiap.TasteEase.Application.Tests.Order.Fixture;

public class OrderFixture
{
    public CreateOrderCommand MockCreateCommand;
    public UpdateOrderCommand MockUpdateOkCommand;
    public UpdateOrderCommand MockUpdateFailCommand;
    public GetOrderAllQuery MockGetlAllQuery;
    public GetOrderByIdQuery MockGetByIdQuery;
    public Domain.Aggregates.FoodAggregate.Food MockFood;
    public Domain.Aggregates.OrderAggregate.Order MockOrder;
    public OrderFood MockOrderFood;
    public List<Domain.Aggregates.FoodAggregate.Food> MockFoodList;
    public IEnumerable<Domain.Aggregates.OrderAggregate.Order> MockOrderList;
    
    public OrderFixture()
    {
        var foodId = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        var orderFoodId = Guid.NewGuid();
        CreateFood(foodId);
        CreateOrderFood(orderFoodId, foodId);
        CreateOrder(orderId, clientId);
        CreateMockCommand(clientId, foodId, orderId);
        ConfigMapster();
    }

    private void ConfigMapster()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        var mappersAssemblies = Array.Empty<Assembly>();

        mappersAssemblies = mappersAssemblies.Append(typeof(DependencyInjection).Assembly).ToArray();

        config.Scan(assemblies: mappersAssemblies);
        config.Default.AddDestinationTransform(DestinationTransform.EmptyCollectionIfNull);
        config.Compile();
    }
    
    private void CreateMockCommand(Guid clientId, Guid foodId, Guid orderId)
    {
        MockCreateCommand = new CreateOrderCommand
        {
            ClientId = clientId,
            Description = "Order #1",
            Foods = new[]
            {
                new OrderFoodCreate(foodId, 1)
            }
        };
        
        MockUpdateOkCommand = new UpdateOrderCommand
        {
            OrderId = orderId,
            Status = OrderStatus.Paid
        };
        
        MockUpdateFailCommand = new UpdateOrderCommand
        {
            OrderId = orderId,
            Status = OrderStatus.WaitPayment
        };

        MockGetlAllQuery = new GetOrderAllQuery
        {
            ClientId = clientId,
            Status = new List<OrderStatus>
            {
                OrderStatus.Paid
            }
        };

        MockGetByIdQuery = new GetOrderByIdQuery
        {
            OrderId = orderId
        };
    }
    
    private void CreateOrderFood(Guid orderFoodId, Guid foodId)
    {
        var orderFoodResult = new OrderFood
        {
            Id = orderFoodId,
            CreatedAt = DateTime.UtcNow,
            FoodId = foodId,
            Quantity = 1,
            Food = MockFood
        };

        MockOrderFood = orderFoodResult;
    }
    
    private void CreateFood(Guid foodId)
    {
        var foodResult = Domain.Aggregates.FoodAggregate.Food.Rehydrate(
            new FoodProps(
                "Hamburguer",
                "Pão com carne",
                8.99M,
                FoodType.Food,
                DateTime.UtcNow,
                DateTime.UtcNow
            ),
            new FoodId(foodId)
        );

        MockFood = foodResult.ValueOrDefault;
        MockFoodList = new List<Domain.Aggregates.FoodAggregate.Food> { foodResult.ValueOrDefault };
    }
    
    private void CreateOrder(Guid orderId, Guid clientId)
    {
        var orderResult = Domain.Aggregates.OrderAggregate.Order.Rehydrate(
            new OrderProps(
                "Order #1",
                OrderStatus.Created,
                clientId,
                DateTime.UtcNow,
                DateTime.UtcNow,
                new List<OrderFood>
                {
                    MockOrderFood
                }
            ),
            new OrderId(orderId)
        );

        MockOrder = orderResult.ValueOrDefault;
        MockOrderList = new List<Domain.Aggregates.OrderAggregate.Order> { orderResult.ValueOrDefault };
    }
}