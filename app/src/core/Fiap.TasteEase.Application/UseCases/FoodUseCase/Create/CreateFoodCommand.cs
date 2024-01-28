using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Create;

public class CreateFoodCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public FoodType Type { get; set; }
}