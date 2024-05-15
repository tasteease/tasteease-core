using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.Tests.Food.Fixture;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Create;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Update;
using FluentResults;
using Moq;

namespace Fiap.TasteEase.Application.Tests.Food;

public class CommandFoodUseCases : IClassFixture<CommandFoodFixture>
{ 
    private readonly Mock<IFoodRepository> _foodRepository = new();
    private readonly CommandFoodFixture _fixture;

    public CommandFoodUseCases(CommandFoodFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task Create()
    {
        // Arrange 
        var command = _fixture.MockCreateCommand;
        var cancellationToken = new CancellationToken();

        _foodRepository
            .Setup(m => m.Add(It.IsAny<Domain.Aggregates.FoodAggregate.Food>()))
            .Returns(Result.Ok(true));
        
        _foodRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));

        // Act
        var handler = new CreateFoodHandler(_foodRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task Delete()
    {
        // Arrange 
        var command = _fixture.MockDeleteCommand;
        var cancellationToken = new CancellationToken();

        _foodRepository
            .Setup(m => m.GetById(command.Id))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockFood)));
            
        _foodRepository
            .Setup(m => m.Delete(It.IsAny<Domain.Aggregates.FoodAggregate.Food>()))
            .Returns(Result.Ok(true));
        
        _foodRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));

        // Act
        var handler = new DeleteFoodHandler(_foodRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task Update()
    {
        // Arrange 
        var command = _fixture.MockUpdateCommand;
        var cancellationToken = new CancellationToken();

        _foodRepository
            .Setup(m => m.GetById(command.Id))
            .Returns(Task.Run(() => Result.Ok(_fixture.MockFood)));
            
        _foodRepository
            .Setup(m => m.Update(It.IsAny<Domain.Aggregates.FoodAggregate.Food>()))
            .Returns(Result.Ok(true));
        
        _foodRepository
            .Setup(m => m.SaveChanges())
            .Returns(Task.Run(() => Result.Ok(1)));

        // Act
        var handler = new UpdateFoodHandler(_foodRepository.Object);
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
    }
}