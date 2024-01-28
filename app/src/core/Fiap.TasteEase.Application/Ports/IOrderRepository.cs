using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using FluentResults;

namespace Fiap.TasteEase.Application.Ports;

public interface IOrderRepository
    : IRepository<OrderModel, Order>
{
    Task<Result<IEnumerable<Order>>> GetByFilters(List<OrderStatus> status, Guid? clientId);
    Task<Result<Order>> GetByPaymentReference(string reference);
    Task<Result<IEnumerable<Order>>> GetWithDescription();
}