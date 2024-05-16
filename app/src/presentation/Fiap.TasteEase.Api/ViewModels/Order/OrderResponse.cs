using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

[ExcludeFromCodeCoverage]
public record OrderResponse(
    Guid Id,
    string Description,
    OrderStatus Status,
    Guid ClientId,
    string ClientName,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IEnumerable<OrderFoodResponse>? Foods
);

[ExcludeFromCodeCoverage]
public record OrderFoodResponse(
    Guid FoodId,
    string FoodName,
    FoodType FoodType,
    string FoodDescription,
    decimal FoodPrice,
    int Quantity,
    DateTime CreatedAt
);

[ExcludeFromCodeCoverage]
public record OrderWithDescriptionResponse(
    Guid Id,
    string Description,
    OrderStatus Status,
    string ClientName,
    decimal TotalPrice,
    DateTime CreatedAt,
    DateTime UpdatedAt
);