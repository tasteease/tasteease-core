using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout;

public class PayCommand : IRequest<Result<OrderPaymentResponseCommand>>
{
    public Guid OrderId { get; init; }
}

public record OrderPaymentResponseCommand(
    Guid OrderId,
    string PaymentLink
);