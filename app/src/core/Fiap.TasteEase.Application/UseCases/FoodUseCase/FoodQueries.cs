using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase
{
    public class GetAll : IRequest<Result<IEnumerable<FoodResponseDto>>> { }

    public class GetById : IRequest<Result<FoodResponseDto>>
    {
        public Guid Id { get; set; }
    }

    public record FoodResponseDto(
        Guid Id,
        string Name,
        string Description,
        double Price,
        FoodType Type,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}