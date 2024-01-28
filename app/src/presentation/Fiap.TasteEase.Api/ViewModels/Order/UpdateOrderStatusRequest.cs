using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

public record UpdateOrderStatusRequest(
    OrderStatus Status
);