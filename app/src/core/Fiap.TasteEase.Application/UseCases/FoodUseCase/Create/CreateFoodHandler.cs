using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase.Create;

public class CreateFoodHandler : IRequestHandler<CreateFoodCommand, Result<string>>
{
    private readonly IFoodRepository _foodRepository;
    private readonly IMediator _mediator;

    public CreateFoodHandler(IMediator mediator, IFoodRepository foodRepository)
    {
        _mediator = mediator;
        _foodRepository = foodRepository;
    }

    public async Task<Result<string>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var foodResult = Food.Create(
            new CreateFoodProps(
                request.Name,
                request.Description,
                request.Price,
                request.Type));

        if (foodResult.IsFailed)
            return Result.Fail("Erro registrando comida");

        var result = _foodRepository.Add(foodResult.ValueOrDefault);

        await _foodRepository.SaveChanges();

        return Result.Ok("Comida registrada com sucesso");
    }
}