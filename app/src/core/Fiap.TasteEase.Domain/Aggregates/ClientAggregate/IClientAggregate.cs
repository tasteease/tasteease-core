using Fiap.TasteEase.Domain.Aggregates.ClientAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Models;

namespace Fiap.TasteEase.Domain.Aggregates.ClientAggregate
{
    public interface IClientAggregate 
        : IAggregateRoot<Client, ClientId, CreateClientProps, ClientProps, ClientModel> { }
}
