namespace Fiap.TasteEase.Domain.DTOs;

public class OrderPaymentDto
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public bool Paid { get; set; }
    public string Reference { get; set; }
    public string PaymentLink { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidDate { get; set; }
}