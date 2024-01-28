using Fiap.TasteEase.Api.ViewModels.Food;
using Fiap.TasteEase.Application.UseCases.FoodUseCase;
using Mapster;

namespace Fiap.TasteEase.Api.Mappers.Food
{
    public class CreateFoodRequestMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateFoodRequest, Create>()
                .Map(model => model.Name, src => src.Name)
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.Price, src => src.Price)
                .Map(model => model.Type, src => src.Type);
        }
    }

    public class UpdateFoodRequestMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<UpdateFoodRequest, Update>()
                .Map(model => model.Id, src => src.Id)
                .Map(model => model.Name, src => src.Name)
                .Map(model => model.Description, src => src.Description)
                .Map(model => model.Price, src => src.Price)
                .Map(model => model.Type, src => src.Type);
        }
    }

    public class DeleteFoodRequestMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<DeleteFoodRequest, Delete>()
                .Map(model => model.Id, src => src.Id);
        }
    }
}