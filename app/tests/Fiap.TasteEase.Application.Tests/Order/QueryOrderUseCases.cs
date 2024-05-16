using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.Tests.Order.Fixture;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetAll;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetById;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetWithDescription;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;
using FluentResults;
using Moq;

namespace Fiap.TasteEase.Application.Tests.Order;

public class QueryOrderUseCases : IClassFixture<OrderFixture>
{
    private readonly Mock<IOrderRepository> _orderRepository = new();
    private readonly OrderFixture _fixture;

    public QueryOrderUseCases(OrderFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task GetAll()
    {
        // Arrange 
        var command = _fixture.MockGetlAllQuery;
        var cancellationToken = new CancellationToken();

        _orderRepository
            .Setup(m => m.GetByFilters(_fixture.MockGetlAllQuery.Status, _fixture.MockGetlAllQuery.ClientId))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockOrderList)));
        
        // Act
        var handler = new GetOrderAllHandler(_orderRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task GetById()
    {
        // Arrange 
        var command = _fixture.MockGetByIdQuery;
        var cancellationToken = new CancellationToken();

        _orderRepository
            .Setup(m => m.GetById(_fixture.MockGetByIdQuery.OrderId))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockOrder)));
        
        // Act
        var handler = new GetOrderByIdHandler(_orderRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task GetWithDescription()
    {
        // Arrange 
        var command = _fixture.MockGetWithDescriptionQueryQuery;
        var cancellationToken = new CancellationToken();

        _orderRepository
            .Setup(m => m.GetWithDescription())
            .Returns(Task.Run(() => Result.Ok(_fixture.MockOrderList)));
        
        // Act
        var handler = new GetOrderWithDescriptionHandler(_orderRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
}