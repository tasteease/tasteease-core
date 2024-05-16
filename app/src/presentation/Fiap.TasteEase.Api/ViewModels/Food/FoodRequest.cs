using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Food;

[ExcludeFromCodeCoverage]
public class CreateFoodRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public FoodType Type { get; set; }
}

[ExcludeFromCodeCoverage]
public class UpdateFoodRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public FoodType Type { get; set; }
}

[ExcludeFromCodeCoverage]
public class DeleteFoodRequest
{
    public Guid Id { get; set; }
}