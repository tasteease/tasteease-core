using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase.Queries.GetWithDescription
{
    public class GetOrderWithDescriptionQuery : IRequest<Result<IEnumerable<OrderWithDescriptionQuery>>>
    {

    }

    public record OrderWithDescriptionQuery(
        Guid Id,
        string Description,
        OrderStatus Status,
        string ClientName,
        decimal TotalPrice,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}