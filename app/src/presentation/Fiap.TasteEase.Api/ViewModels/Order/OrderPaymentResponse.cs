namespace Fiap.TasteEase.Api.ViewModels.Order;

public record OrderPaymentResponse(
    Guid OrderId,
    string PaymentLink
);