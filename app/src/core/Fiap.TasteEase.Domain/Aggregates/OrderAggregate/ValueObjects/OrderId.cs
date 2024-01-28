using Fiap.TasteEase.Domain.Aggregates.Common;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

public record OrderId(Guid Value);
public record OrderFoodId(Guid Value);