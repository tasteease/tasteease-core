using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Fiap.TasteEase.Domain.Ports;

namespace Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
 
public interface IFoodAggregate 
    : IAggregateRoot<Food, FoodId, CreateFoodProps, FoodProps, FoodModel> { } 