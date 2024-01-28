using Fiap.TasteEase.Api.ViewModels.Client;
using Fiap.TasteEase.Application.UseCases.ClientUseCase;
using Mapster;

namespace Fiap.TasteEase.Api.Mappers.Client
{
    public class ClientRequestMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateClientRequest, Create>()
                .Map(model => model.Name, src => src.Name)
                .Map(model => model.TaxpayerNumber, src => src.TaxpayerNumber);
        }
    }
}