using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

public class OrderPayment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public bool Paid { get; set; }
    public string Reference { get; set; }
    public string PaymentLink { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidDate { get; set; }
}