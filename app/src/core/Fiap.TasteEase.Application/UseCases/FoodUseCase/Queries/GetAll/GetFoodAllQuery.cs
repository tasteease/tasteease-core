using System.Diagnostics.CodeAnalysis;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;

[ExcludeFromCodeCoverage]
public class GetFoodAllQuery : IRequest<Result<IEnumerable<FoodResponseDto>>>
{
}