using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Fiap.TasteEase.Infra.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TasteEase.Infra.Repository
{
    public class FoodRepository 
        : Repository<FoodModel, Food, FoodId, CreateFoodProps, FoodProps>, IFoodRepository
    {
        public FoodRepository(ApplicationDbContext db) : base(db) { }
        
        public async Task<Result<List<Food>>> GetByIds(IEnumerable<Guid> ids)
        {
            var models = await DbSet.AsNoTracking().Where(f => ids.Contains(f.Id)).ToListAsync();
            var aggregates = models.Select(model => 
                Food.Rehydrate(model).ValueOrDefault);
            return Result.Ok(aggregates.ToList());
        }
    }
}