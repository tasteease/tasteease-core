namespace Fiap.TasteEase.Domain.DTOs;

public class ClientDto
{
    public Guid Id { get; set; }
    public string? TaxpayerNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}