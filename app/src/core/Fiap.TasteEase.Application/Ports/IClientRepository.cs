using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;

namespace Fiap.TasteEase.Application.Ports;

public interface IClientRepository 
    : IRepository<ClientModel, Client>
{

}