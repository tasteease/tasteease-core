using Fiap.TasteEase.Application.UseCases.OrderUseCase;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Mapster;

namespace Fiap.TasteEase.Application.Mappers.Order
{
    internal class OrderCommandMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Create, CreateOrderProps>()
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.ClientId, src => src.ClientId);
            
            config.ForType<OrderFoodCreate, OrderFood>()
                .Map(model => model.Id, src => Guid.NewGuid())
                .Map(model => model.FoodId, src => src.FoodId)
                .Map(model => model.Quantity, src => src.Quantity)
                .Map(model => model.CreatedAt, src => DateTime.UtcNow);
        }
    }
}
