using Fiap.TasteEase.Api.ViewModels.Order;
using Fiap.TasteEase.Application.UseCases.OrderUseCase;
using Mapster;

namespace Fiap.TasteEase.Api.Mappers
{
    internal class OrderRequestMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<OrderRequest, Create>()
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.ClientId, src => src.ClientId)
                .Map(model => model.Foods, src => src.Foods, t => t.Foods != null);
            
            config.ForType<OrderFoodRequest, OrderFoodCreate>()
                .Map(model => model.FoodId, src => src.FoodId)
                .Map(model => model.Quantity, src => src.Quantity);
        }
    }
}
