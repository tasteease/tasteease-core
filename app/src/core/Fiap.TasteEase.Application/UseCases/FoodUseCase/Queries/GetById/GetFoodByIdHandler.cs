using Fiap.TasteEase.Application.Ports;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Queries.GetById;

public class GetFoodByIdHandler : IRequestHandler<GetFoodByIdQuery, Result<FoodResponseDto>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;

    public GetFoodByIdHandler(IMediator mediator, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _foodRepository = foodRepository;
    }

    public async Task<Result<FoodResponseDto>> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
    {
        var foodResult = await _foodRepository.GetById(request.Id);

        if (foodResult.IsFailed)
            return Result.Fail("Erro ao obter os comidas");

        var food = foodResult.ValueOrDefault;
        var response = food.Adapt<FoodResponseDto>();

        return Result.Ok(response);
    }
}