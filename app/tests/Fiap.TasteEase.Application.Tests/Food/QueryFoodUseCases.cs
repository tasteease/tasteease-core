using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.Tests.Food.Fixture;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using FluentResults;
using Moq;

namespace Fiap.TasteEase.Application.Tests.Food;

public class QueryFoodUseCases : IClassFixture<GetFoodFixture>
{ 
    private readonly Mock<IFoodRepository> _foodRepository = new();
    private readonly GetFoodFixture _fixture;

    public QueryFoodUseCases(GetFoodFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task GetById()
    {
        // Arrange 
        var query = _fixture.MockQuery;
        var cancellationToken = new CancellationToken();

        _foodRepository
            .Setup(m => m.GetById(query.Id))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockFood)));

        // Act
        var handler = new GetFoodByIdHandler(_foodRepository.Object);
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task GetAll()
    {
        // Arrange 
        var query = _fixture.MockQueryAll;
        var cancellationToken = new CancellationToken();

        _foodRepository
            .Setup(m => m.GetAll())
            .Returns(Task.Run(() => Result.Ok(_fixture.MockFoodList)));

        // Act
        var handler = new GetFoodAllHandler(_foodRepository.Object);
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.ValueOrDefault.Any());
    }
}