using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Models;
using FluentResults;

namespace Fiap.TasteEase.Application.Ports;

public interface IFoodRepository
    : IRepository<FoodModel, Food>
{
    Task<Result<List<Food>>> GetByIds(IEnumerable<Guid> ids);
}