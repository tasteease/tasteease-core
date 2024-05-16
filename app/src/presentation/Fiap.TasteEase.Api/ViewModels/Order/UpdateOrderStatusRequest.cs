using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

[ExcludeFromCodeCoverage]
public record UpdateOrderStatusRequest(
    OrderStatus Status
);