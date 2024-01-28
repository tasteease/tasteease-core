using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Fiap.TasteEase.Application.UseCases.OrderUseCase
{
    public class Create : IRequest<Result<OrderResponseCommand>>
    {
        public string Description { get; init; }
        public Guid ClientId { get; init; }
        public IEnumerable<OrderFoodCreate>? Foods { get; init; } = null;
    }
    
    public class UpdateStatus : IRequest<Result<OrderResponseCommand>>
    {
        public Guid OrderId { get; init; }
        public OrderStatus Status { get; init; }
    }
    
    public class ProcessPayment : IRequest<Result<string>>
    {
        public string Reference { get; init; }
        public bool Paid { get; init; }
        public DateTime? PaidDate { get; init; }
    }
    
    public class Pay : IRequest<Result<OrderPaymentResponseCommand>>
    {
        public Guid OrderId { get; init; }
    }

    public record OrderFoodCreate(
        Guid FoodId,
        int Quantity
    );
    
    public record OrderResponseCommand(
        Guid OrderId,
        Guid ClientId,
        decimal TotalPrice,
        OrderStatus Status
    );
    
    public record OrderPaymentResponseCommand(
        Guid OrderId,
        string PaymentLink
    );
}