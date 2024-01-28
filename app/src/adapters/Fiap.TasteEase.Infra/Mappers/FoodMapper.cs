using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Models;
using Mapster;

namespace Fiap.TasteEase.Infra.Mappers
{
    internal class FoodMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Food, FoodModel>()
                .Map(model => model.Id, food => food.Id!.Value)
                .Map(model => model.Name, food => food.Name)
                .Map(model => model.Description, food => food.Description)
                .Map(model => model.Price, food => food.Price)
                .Map(model => model.CreatedAt, food => food.CreatedAt)
                .Map(model => model.UpdatedAt, food => food.UpdatedAt);
        }
    }
}