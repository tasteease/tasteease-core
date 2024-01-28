using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;

namespace Fiap.TasteEase.Domain.Aggregates.FoodAggregate;

public interface IFoodAggregate
    : IAggregateRoot<Food, FoodId, CreateFoodProps, FoodProps, FoodModel>
{
}