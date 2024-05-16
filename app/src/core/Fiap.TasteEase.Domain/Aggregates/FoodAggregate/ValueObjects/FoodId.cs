using System.Diagnostics.CodeAnalysis;

namespace Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;

[ExcludeFromCodeCoverage]
public record FoodId(Guid Value);