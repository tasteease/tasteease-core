using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

[ExcludeFromCodeCoverage]
public record CreateOrderResponse(
    Guid OrderId,
    Guid ClientId,
    decimal TotalPrice,
    OrderStatus Status
);