using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Checkout.Process;

public class ProcessPaymentCommand : IRequest<Result<string>>
{
    public string Reference { get; init; }
    public bool Paid { get; init; }
    public DateTime? PaidDate { get; init; }
}