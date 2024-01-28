using Fiap.TasteEase.Api.ViewModels.Order;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetWithDescription;
using Mapster;

namespace Fiap.TasteEase.Api.Mappers;

internal class OrderRequestMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<OrderRequest, CreateOrderCommand>()
            .Map(model => model.Description, src => src.Description)
            .Map(model => model.ClientId, src => src.ClientId)
            .Map(model => model.Foods, src => src.Foods, t => t.Foods != null);

        config.ForType<OrderFoodRequest, OrderFoodCreate>()
            .Map(model => model.FoodId, src => src.FoodId)
            .Map(model => model.Quantity, src => src.Quantity);

        config.ForType<OrderWithDescriptionResponse, OrderWithDescriptionQuery>()
            .Map(model => model.Id, src => src.Id)
            .Map(model => model.Description, src => src.Description)
            .Map(model => model.ClientName, src => src.ClientName)
            .Map(model => model.TotalPrice, src => src.TotalPrice)
            .Map(model => model.CreatedAt, src => src.CreatedAt)
            .Map(model => model.UpdatedAt, src => src.UpdatedAt);
    }
}