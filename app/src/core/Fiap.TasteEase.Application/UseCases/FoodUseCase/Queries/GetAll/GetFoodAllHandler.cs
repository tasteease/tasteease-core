using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetAll;

public class GetFoodAllHandler : IRequestHandler<GetFoodAllQuery, Result<IEnumerable<FoodResponseDto>>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;

    public GetFoodAllHandler(IMediator mediator, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _foodRepository = foodRepository;
    }


    public async Task<Result<IEnumerable<FoodResponseDto>>> Handle(GetFoodAllQuery request,
        CancellationToken cancellationToken)
    {
        var foodsResult = await _foodRepository.GetAll();

        if (foodsResult.IsFailed)
            return Result.Fail("Erro ao obter os comidas");

        var foods = foodsResult.ValueOrDefault;
        var response = foods.Adapt<IEnumerable<FoodResponseDto>>();

        return Result.Ok(response);
    }
}