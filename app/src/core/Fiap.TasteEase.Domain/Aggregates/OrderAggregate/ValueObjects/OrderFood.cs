using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

[ExcludeFromCodeCoverage]
public class OrderFood
{
    public Guid Id { get; set; }
    public Guid FoodId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public Food Food { get; set; }
}