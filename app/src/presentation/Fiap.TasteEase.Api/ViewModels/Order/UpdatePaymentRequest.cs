using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;

namespace Fiap.TasteEase.Api.ViewModels.Order;

public record UpdatePaymentRequest(
    bool Paid,
    DateTime? PaidDate,
    string Reference
);