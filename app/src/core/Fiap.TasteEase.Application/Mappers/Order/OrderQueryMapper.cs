using Fiap.TasteEase.Application.UseCases.OrderUseCase;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Mapster;

namespace Fiap.TasteEase.Application.Mappers.Order
{
    internal class OrderQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Aggregates.OrderAggregate.Order, OrderResponseQuery>()
                .Map(model => model.Id, src => src.Id.Value)
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.ClientId, src => src.ClientId)
                .Map(model => model.CreatedAt, src => src.CreatedAt)
                .Map(model => model.UpdatedAt, src => src.UpdatedAt)
                .Map(model => model.Foods, src => src.Foods);
            
            config.ForType<OrderFood, OrderFoodResponseQuery>()
                .Map(model => model.FoodId, src => src.FoodId)
                .Map(model => model.FoodName, src => src.Food.Name)
                .Map(model => model.FoodType, src => src.Food.Type)
                .Map(model => model.FoodDescription, src => src.Food.Description)
                .Map(model => model.FoodPrice, src => src.Food.Price)
                .Map(model => model.Quantity, src => src.Quantity)
                .Map(model => model.CreatedAt, src => src.CreatedAt);
        }
    }
}
