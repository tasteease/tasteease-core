using Fiap.TasteEase.Application.Ports;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Update;

public class UpdateFoodHandler : IRequestHandler<UpdateFoodCommand, Result<string>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;

    public UpdateFoodHandler(IMediator mediator, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _foodRepository = foodRepository;
    }

    public async Task<Result<string>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        var foodResult = await _foodRepository.GetById(request.Id);
        if (foodResult.IsFailed)
            return Result.Fail("Comida não existe");

        var newFood = foodResult.ValueOrDefault.Update(
            new CreateFoodProps(
                request.Name,
                request.Description,
                request.Price,
                request.Type));

        if (foodResult.IsFailed)
            return Result.Fail("Erro atualizando comida");

        _foodRepository.Update(newFood.ValueOrDefault);

        await _foodRepository.SaveChanges();

        return Result.Ok("Comida atualizada com sucesso");
    }
}