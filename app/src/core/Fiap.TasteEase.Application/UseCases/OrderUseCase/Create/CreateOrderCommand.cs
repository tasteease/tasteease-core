using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;

[ExcludeFromCodeCoverage]
public class CreateOrderCommand : IRequest<Result<OrderResponseCommand>>
{
    public string Description { get; init; }
    public Guid ClientId { get; init; }
    public IEnumerable<OrderFoodCreate>? Foods { get; init; } = null;
}

[ExcludeFromCodeCoverage]
public record OrderFoodCreate(
    Guid FoodId,
    int Quantity
);

[ExcludeFromCodeCoverage]
public record OrderResponseCommand(
    Guid OrderId,
    Guid ClientId,
    decimal TotalPrice,
    OrderStatus Status
);