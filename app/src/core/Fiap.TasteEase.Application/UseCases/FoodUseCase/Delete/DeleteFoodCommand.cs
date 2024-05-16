using System.Diagnostics.CodeAnalysis;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;

[ExcludeFromCodeCoverage]
public class DeleteFoodCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
}