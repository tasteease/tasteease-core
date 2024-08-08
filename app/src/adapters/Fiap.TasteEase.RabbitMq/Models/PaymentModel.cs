namespace Fiap.TasteEase.RabbitMq.Models;

public class PaymentModel
{
    public bool Paid { get; set; }
    public DateTime? PaidDate { get; set; }
    public string? Reference { get; set; }
    public Guid OrderId { get; set; }
}