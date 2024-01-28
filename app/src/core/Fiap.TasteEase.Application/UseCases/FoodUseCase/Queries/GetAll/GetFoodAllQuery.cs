using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;

public class GetFoodAllQuery : IRequest<Result<IEnumerable<FoodResponseDto>>>
{
}