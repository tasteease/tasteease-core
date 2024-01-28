namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

public enum OrderStatus
{
    Created,
    WaitPayment,
    Paid,
    Preparing,
    Prepared,
    Delivered
}