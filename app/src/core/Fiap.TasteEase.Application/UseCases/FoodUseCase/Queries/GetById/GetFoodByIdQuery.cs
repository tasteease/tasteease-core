using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;

[ExcludeFromCodeCoverage]
public class GetFoodByIdQuery : IRequest<Result<FoodResponseDto>>
{
    public Guid Id { get; set; }
}

[ExcludeFromCodeCoverage]
public record FoodResponseDto(
    Guid Id,
    string Name,
    string Description,
    double Price,
    FoodType Type,
    DateTime CreatedAt,
    DateTime UpdatedAt);