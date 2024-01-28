using System.Linq.Expressions;
using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate;
using Fiap.TasteEase.Domain.Aggregates.OrderAggregate.ValueObjects;
using Fiap.TasteEase.Domain.Models;
using Fiap.TasteEase.Domain.Ports;
using Fiap.TasteEase.Infra.Context;
using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Fiap.TasteEase.Infra.Repository;

public class OrderRepository 
    : Repository<OrderModel, Order, OrderId, CreateOrderProps, OrderProps>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext db) : base(db) { }
    
    public override async Task<Result<IEnumerable<Order>>> Get(Expression<Func<OrderModel, bool>> predicate, params Expression<Func<OrderModel, EntityModel>>[] includes)
    {
        var query = DbSet.AsNoTracking();
        query = includes.Aggregate(query, (current, expression) => current.Include(expression));
        var models = await query.Include(i => i.Foods).ThenInclude(i => i.Food).Where(predicate).ToListAsync();
        var aggregates = models.Select(model => 
            Order.Rehydrate(model).ValueOrDefault);
        return Result.Ok(aggregates);
    }
    
    public async Task<Result<IEnumerable<Order>>> GetByFilters(List<OrderStatus> status, Guid? clientId)
    {
        var query = DbSet.AsNoTracking()
            .Include(i => i.Client)
            .Include(i => i.Foods)
            .ThenInclude(i => i.Food)
            .Where(w => true);

        if (status.Any()) query = query.Where(w => status.Contains(w.Status));
        if (clientId is not null) query = query.Where(w => w.ClientId == clientId);

        var models = await query.OrderByDescending(o => o.CreatedAt).ToListAsync();
        var aggregates = models.Select(model => 
            Order.Rehydrate(model).ValueOrDefault);
        return Result.Ok(aggregates);
    }
    
    public override async Task<Result<Order>> GetById(Guid id)
    {
        var query = await DbSet.AsNoTracking()
            .Include(i => i.Client)
            .Include(i => i.Foods)
            .ThenInclude(i => i.Food)
            .FirstOrDefaultAsync(f => f.Id == id);
        return query is null ? Result.Fail("não foi encontrado") : Result.Ok(Order.Rehydrate(query).ValueOrDefault);
    }
    
    public async Task<Result<Order>> GetByPaymentReference(string reference)
    {
        var query = await DbSet.AsNoTracking()
            .Include(i => i.Payments)
            .Include(i => i.Client)
            .Include(i => i.Foods)
            .ThenInclude(i => i.Food)
            .FirstOrDefaultAsync(f => f.Payments.FirstOrDefault(f => f.Reference == reference) != null);
        return query is null ? Result.Fail("não foi encontrado") : Result.Ok(Order.Rehydrate(query).ValueOrDefault);
    }
    
    public override Result<bool> Update(Order aggregate)
    {
        var result = aggregate.Adapt<OrderModel>();
        result.Client = null;
        foreach (var orderFoodModel in result.Foods)
        {
            orderFoodModel.Food = null;
        }
        DbSet.Update(result);
        return Result.Ok(true);
    }
}