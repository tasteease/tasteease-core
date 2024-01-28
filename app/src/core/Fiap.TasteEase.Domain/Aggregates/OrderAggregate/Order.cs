using Fiap.TasteEase.Domain.Aggregates.ClientAggregate;
using Fiap.TasteEase.Domain.Aggregates.ClientAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate;
using Fiap.TasteEase.Domain.Aggregates.FoodAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using FluentResults;

namespace Fiap.TasteEase.Domain.Aggregates.OrderAggregate;

public class Order : Entity<OrderId, OrderProps>, IOrderAggregate
{
    public Order(OrderProps props, OrderId? id = default) : base(props, id) { }

    public string Description => Props.Description;
    public OrderStatus Status => Props.Status;
    public Guid ClientId => Props.ClientId;
    public DateTime CreatedAt => Props.CreatedAt;
    public DateTime UpdatedAt => Props.UpdatedAt;
    public IReadOnlyList<OrderFood> Foods => Props.Foods;
    public IReadOnlyList<OrderPayment> Payments => Props.Payments;
    public Client Client => Props.Client;

    public static Result<Order> Create(CreateOrderProps props)
    {
        var date = DateTime.UtcNow;
        var orderProps = new OrderProps(
            props.Description,
            OrderStatus.Created,
            props.ClientId,
            date,
            date
        );
        
        var order = new Order(orderProps, new OrderId(Guid.NewGuid()));
        return Result.Ok(order);
    }

    public static Result<Order> Rehydrate(OrderProps props, OrderId id)
        => Result.Ok(new Order(props, id));

    public static Result<Order> Rehydrate(OrderModel model)
    {
        List<OrderFood>? foods = null;
        if (model.Foods?.Any() ?? false)
        {
            foods = model.Foods
            .Select(s => 
                new OrderFood
                {
                    Id = s.Id,
                    FoodId = s.FoodId,
                    Quantity = s.Quantity, 
                    CreatedAt = s.CreatedAt,
                    Food = s.Food is not null ? Food.Rehydrate(new FoodProps(
                        s.Food.Name,
                        s.Food.Description,
                        s.Food.Price,
                        s.Food.Type,
                        s.Food.CreatedAt,
                        s.Food.UpdatedAt
                    ), new FoodId(s.Food.Id)).ValueOrDefault : null
                }
            ).ToList();
        }
        
        List<OrderPayment>? payments = null;
        if (model.Payments?.Any() ?? false)
        {
            payments = model.Payments
                .Select(s => 
                    new OrderPayment
                    {
                        Id = s.Id,
                        Amount = s.Amount,
                        CreatedAt = s.CreatedAt,
                        Paid = s.Paid,
                        PaidDate = s.PaidDate,
                        PaymentLink = s.PaymentLink,
                        Reference = s.Reference
                    }
                ).ToList();
        }

        Client? client = null;
        if (model.Client is not null)
        {
            client = ClientAggregate.Client.Rehydrate(
                new ClientProps(
                    model.Client.Name,
                    model.Client.TaxpayerNumber,
                    model.Client.CreatedAt,
                    model.Client.UpdatedAt
                ),
                new ClientId(model.Client.Id)
            ).ValueOrDefault;
        }
        
        var order = new Order(
            new OrderProps(
                model.Description,
                model.Status,
                model.ClientId,
                model.CreatedAt,
                model.UpdatedAt,
                client,
                foods,
                payments
            ), 
            new OrderId(model.Id)
        );
        
        return Result.Ok(order);
    }
    
    public Result<Order> AddFood(List<OrderFood> foods)
    {
        var foodProps = new List<OrderFood>(foods.Count + (Props.Foods?.Count ?? 0));
        foodProps.AddRange(Props?.Foods ?? Enumerable.Empty<OrderFood>());
        foodProps.AddRange(foods);
        
        Props = Props with {
            Foods = foodProps
        };

        return Result.Ok(this);
    }
    
    
    public Result<decimal> GetTotalPrice(List<Food> foods)
    {
        if (!foods.Any()) Result.Fail("não foi possível calcular o valor");
        
        var total = Props.Foods.Sum(s => (decimal)s.Quantity * (foods.FirstOrDefault(f => f.Id.Value == s.FoodId)?.Price ?? 0M));
        return Result.Ok(total);
    }

    public Result<Order> UpdateStatus(OrderStatus newStatus)
    {
        Props = Props with { 
            Status = newStatus,
            UpdatedAt = DateTime.UtcNow
        };

        return Result.Ok(this);
    }
    
    public Result<OrderPayment> Pay()
    {
        var payment = new OrderPayment
        {
            Amount = GetTotalPrice(Props.Foods.Select(s => s.Food).ToList()).ValueOrDefault,
            CreatedAt = DateTime.UtcNow,
            Paid = false,
            PaymentLink = "https://pay-me.com",
            Reference = $"ORDER_ID-{Id.Value}"
        };
        
        var paymentProps = new List<OrderPayment>((Props.Payments?.Count ?? 0) + 1);
        paymentProps.AddRange(Props?.Payments ?? Enumerable.Empty<OrderPayment>());
        paymentProps.Add(payment);

        Props = Props with
        {
            Payments = paymentProps
        };
        
        return Result.Ok(payment);
    }
    
    public Result<Order> ProcessPayment(string reference, bool paid, DateTime? paidDate)
    {
        var payment = Props.Payments.FirstOrDefault(f => f.Reference == reference);

        if (payment is null)
            Result.Fail("pagamento não encontrado");

        payment.Paid = paid;
        payment.PaidDate = paidDate;
        
        return Result.Ok(this);
    }
}

public record OrderProps(
    string Description,
    OrderStatus Status,
    Guid ClientId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Client Client = null,
    List<OrderFood>? Foods = null,
    List<OrderPayment>? Payments = null
);

public record CreateOrderProps(
    string Description,
    Guid ClientId
);