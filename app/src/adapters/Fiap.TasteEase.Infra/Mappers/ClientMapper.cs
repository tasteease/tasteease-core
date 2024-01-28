using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using Fiap.TasteEase.Domain.Models;
using Mapster;

namespace Fiap.TasteEase.Infra.Mappers
{
    internal class ClientMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Client, ClientModel>()
                .Map(model => model.Id, client => client.Id!.Value)
                .Map(model => model.Name, client => client.Name)
                .Map(model => model.TaxpayerNumber, client => client.TaxpayerNumber)
                .Map(model => model.CreatedAt, client => client.CreatedAt)
                .Map(model => model.UpdatedAt, client => client.UpdatedAt);
        }
    }
}