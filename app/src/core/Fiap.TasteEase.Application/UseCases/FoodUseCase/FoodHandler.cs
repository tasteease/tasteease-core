using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Application.UseCases.OrderUseCase;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using Mapster;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase
{
    public class FoodHandler : IRequestHandler<Create, Result<string>>,
                               IRequestHandler<Update, Result<string>>,
                               IRequestHandler<Delete, Result<string>>,
                               IRequestHandler<GetAll, Result<IEnumerable<FoodResponseDto>>>,
                               IRequestHandler<GetById, Result<FoodResponseDto>>
    {
        private readonly IMediator _mediator;
        private readonly IFoodRepository _foodRepository;

        public FoodHandler(IMediator mediator, IFoodRepository foodRepository)
        {
            _mediator = mediator;
            _foodRepository = foodRepository;
        }

        public async Task<Result<string>> Handle(Create request, CancellationToken cancellationToken)
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

        public async Task<Result<string>> Handle(Update request, CancellationToken cancellationToken)
        {
            var foodResult = await _foodRepository.GetById(request.Id);
            if(foodResult.IsFailed)
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

        public async Task<Result<string>> Handle(Delete request, CancellationToken cancellationToken)
        {
            var foodResult = await _foodRepository.GetById(request.Id);
            if (foodResult.IsFailed)
                return Result.Fail("Comida não existe");

            _foodRepository.Delete(foodResult.ValueOrDefault);

            await _foodRepository.SaveChanges();

            return Result.Ok("Comida deleteada com sucesso");
        }

        public async Task<Result<IEnumerable<FoodResponseDto>>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            var foodsResult = await _foodRepository.GetAll();

            if (foodsResult.IsFailed)
                return Result.Fail("Erro ao obter os comidas");

            var foods = foodsResult.ValueOrDefault;
            var response = foods.Adapt<IEnumerable<FoodResponseDto>>();

            return Result.Ok(response);
        }

        public async Task<Result<FoodResponseDto>> Handle(GetById request, CancellationToken cancellationToken)
        {
            var foodResult = await _foodRepository.GetById(request.Id);

            if (foodResult.IsFailed)
                return Result.Fail("Erro ao obter os comidas");

            var food = foodResult.ValueOrDefault;
            var response = food.Adapt<FoodResponseDto>();

            return Result.Ok(response);
        }
    }
}