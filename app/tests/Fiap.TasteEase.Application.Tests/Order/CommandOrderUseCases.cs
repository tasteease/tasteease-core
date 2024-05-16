using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.Tests.Order.Fixture;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;
using FluentResults;
using Moq;

namespace Fiap.TasteEase.Application.Tests.Order;

[Collection("Collection_Fixture")]
public class CommandOrderUseCases : IClassFixture<OrderFixture>
{
    private readonly Mock<IFoodRepository> _foodRepository = new();
    private readonly Mock<IOrderRepository> _orderRepository = new();
    private readonly OrderFixture _fixture;

    public CommandOrderUseCases(OrderFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task Create()
    {
        // Arrange 
        var command = _fixture.MockCreateCommand;
        var cancellationToken = new CancellationToken();

        _orderRepository
            .Setup(m => m.Add(It.IsAny<Domain.Aggregates.OrderAggregate.Order>()))
            .Returns(Result.Ok(true));
        
        _orderRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));
        
        _foodRepository
            .Setup(m => m.GetByIds(new List<Guid> { _fixture.MockOrderFood.FoodId }))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockFoodList)));

        // Act
        var handler = new CreateOrderHandler(_orderRepository.Object, _foodRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task UpdateStatusOk()
    {
        // Arrange 
        var command = _fixture.MockUpdateOkCommand;
        var cancellationToken = new CancellationToken();
        
        _orderRepository
            .Setup(m => m.GetById(_fixture.MockOrder.Id!.Value))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockOrder)));

        _orderRepository
            .Setup(m => m.Update(It.IsAny<Domain.Aggregates.OrderAggregate.Order>()))
            .Returns(Result.Ok(true));
        
        _orderRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));

        // Act
        var handler = new UpdateOrderHandler(_orderRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task UpdateStatusFail()
    {
        // Arrange 
        var command = _fixture.MockUpdateFailCommand;
        var cancellationToken = new CancellationToken();
        
        _orderRepository
            .Setup(m => m.GetById(_fixture.MockOrder.Id!.Value))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockOrder)));

        _orderRepository
            .Setup(m => m.Update(It.IsAny<Domain.Aggregates.OrderAggregate.Order>()))
            .Returns(Result.Ok(true));
        
        _orderRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));

        // Act
        var handler = new UpdateOrderHandler(_orderRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
    }
}