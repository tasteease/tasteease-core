using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Fiap.TasteEase.Domain.Ports;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate;

public interface IOrderAggregate
    : IAggregateRoot<Order, OrderId, CreateOrderProps, OrderProps, OrderModel> { }