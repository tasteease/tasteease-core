using Fiap.TasteEase.Application.UseCases.FoodUseCase.Create;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Update;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;

namespace Fiap.TasteEase.Application.Tests.Food.Fixture;

public class CommandFoodFixture
{
    public CreateFoodCommand MockCreateCommand;
    public DeleteFoodCommand MockDeleteCommand;
    public UpdateFoodCommand MockUpdateCommand;
    public Domain.Aggregates.FoodAggregate.Food MockFood;
    
    public CommandFoodFixture()
    {
        var id = Guid.NewGuid();
        CreateMockCommand(id);
        CreateFood(id);
    }

    private void CreateMockCommand(Guid id)
    {
        MockCreateCommand = new CreateFoodCommand
        {
            Name = "Hamburguer",
            Description = "Pão com carne",
            Price = 8.99M,
            Type = FoodType.Food
        };
        
        MockDeleteCommand = new DeleteFoodCommand
        {
            Id = id
        };
        
        MockUpdateCommand = new UpdateFoodCommand
        {
            Id = id,
            Name = "Torta Holandesa",
            Description = "Torta com bolachas de chocolate e recheio de creme",
            Price = 9.99M,
            Type = FoodType.Dessert
        };
    }
    
    private void CreateFood(Guid id)
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
            new FoodId(id)
        );

        MockFood = foodResult.ValueOrDefault;
    }
}