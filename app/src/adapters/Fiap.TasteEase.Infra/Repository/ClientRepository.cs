using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Fiap.TasteEase.Infra.Context;

namespace Fiap.TasteEase.Infra.Repository;

public class ClientRepository
    : Repository<ClientModel, Client, ClientId, CreateClientProps, ClientProps>, IClientRepository
{
    public ClientRepository(ApplicationDbContext db) : base(db) { }

}