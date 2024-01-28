using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Mapster;

namespace Fiap.TasteEase.Infra.Mappers
{
    internal class OrderMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Order, OrderModel>()
                .Map(model => model.Id, src => src.Id!.Value)
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.Status, src => src.Status)
                .Map(model => model.CreatedAt, src => src.CreatedAt)
                .Map(model => model.UpdatedAt, src => src.UpdatedAt);
            
            config.ForType<OrderFood, OrderFoodModel>()
                .Map(model => model.FoodId, src => src.FoodId)
                .Map(model => model.Quantity, src => src.Quantity)
                .Map(model => model.CreatedAt, src => src.CreatedAt);
        }
    }
}
