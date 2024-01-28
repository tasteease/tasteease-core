using Fiap.TasteEase.Application.UseCases.FoodUseCase;
using Mapster;

namespace Fiap.TasteEase.Application.Mappers.Food
{
    internal class FoodQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Aggregates.FoodAggregate.Food, FoodResponseDto>()
                .Map(model => model.Id, src => src.Id.Value)
                .Map(model => model.Name, src => src.Name)
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.Price, src => src.Price)
                .Map(model => model.Type, src => src.Type);
        }
    }
}