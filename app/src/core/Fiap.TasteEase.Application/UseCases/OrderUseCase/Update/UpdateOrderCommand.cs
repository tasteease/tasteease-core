using Fiap.TasteEase.Application.UseCases.OrderUseCase.Create;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Update;

public class UpdateOrderCommand : IRequest<Result<OrderResponseCommand>>
{
    public Guid OrderId { get; init; }
    public OrderStatus Status { get; init; }
}