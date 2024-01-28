using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;

public class DeleteFoodCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
}