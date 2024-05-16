using System.Diagnostics.CodeAnalysis;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

[ExcludeFromCodeCoverage]
public record OrderId(Guid Value);

[ExcludeFromCodeCoverage]
public record OrderFoodId(Guid Value);