using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

public record CreateOrderResponse(
    Guid OrderId,
    Guid ClientId,
    decimal TotalPrice,
    OrderStatus Status
);