using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Update;

public class UpdateFoodCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public FoodType Type { get; set; }
}