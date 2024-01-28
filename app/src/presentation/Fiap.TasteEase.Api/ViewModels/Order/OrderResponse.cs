using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

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

public record OrderFoodResponse(
    Guid FoodId,
    string FoodName,
    FoodType FoodType,
    string FoodDescription,
    decimal FoodPrice,
    int Quantity,
    DateTime CreatedAt
);