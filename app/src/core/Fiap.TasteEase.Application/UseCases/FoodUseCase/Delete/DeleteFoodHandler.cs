using Fiap.TasteEase.Application.Ports;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Delete;

public class DeleteFoodHandler : IRequestHandler<DeleteFoodCommand, Result<string>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;

    public DeleteFoodHandler(IMediator mediator, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _foodRepository = foodRepository;
    }


    public async Task<Result<string>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var foodResult = await _foodRepository.GetById(request.Id);
        if (foodResult.IsFailed)
            return Result.Fail("Comida não existe");

        _foodRepository.Delete(foodResult.ValueOrDefault);

        await _foodRepository.SaveChanges();

        return Result.Ok("Comida deleteada com sucesso");
    }
}