using System.Reflection;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Mapster;

namespace Fiap.TasteEase.Application.Tests.Food.Fixture;

public class GetFoodFixture
{
    public Domain.Aggregates.FoodAggregate.Food MockFood;
    public GetFoodByIdQuery MockQuery;
    public GetFoodAllQuery MockQueryAll;

    public IEnumerable<Domain.Aggregates.FoodAggregate.Food> MockFoodList;

    public GetFoodFixture()
    {
        var id = Guid.NewGuid();
        ConfigMapster();
        CreateQuery(id);
        CreateFood(id);
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
    
    private void CreateQuery(Guid id)
    {
        MockQuery = new GetFoodByIdQuery
        {
            Id = id
        };

        MockQueryAll = new GetFoodAllQuery();
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
        MockFoodList = new List<Domain.Aggregates.FoodAggregate.Food> { foodResult.ValueOrDefault };
    }
}