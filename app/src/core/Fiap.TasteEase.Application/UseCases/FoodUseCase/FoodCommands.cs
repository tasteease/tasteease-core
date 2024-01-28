using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.FoodUseCase
{
    public class Create : IRequest<Result<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public FoodType Type { get; set; }
    }

    public class Update : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public FoodType Type { get; set; }
    }

    public class Delete : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }       
}