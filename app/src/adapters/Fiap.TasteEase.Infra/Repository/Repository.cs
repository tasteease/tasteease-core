using System.Linq.Expressions;
using Fiap.TasteEase.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Fiap.TasteEase.Application.Ports;
using Fiap.TasteEase.Domain.Aggregates.Common;
using Fiap.TasteEase.Domain.Ports;
using FluentResults;
using Mapster;

namespace Fiap.TasteEase.Infra.Repository;

public abstract class Repository<TEntity, TAggregate, TKey, TCreateProps, TRehydrateProps> 
    : IRepository<TEntity, TAggregate> 
    where TEntity : EntityModel
    where TAggregate : IAggregateRoot<TAggregate, TKey, TCreateProps, TRehydrateProps, TEntity>
{
    protected readonly ApplicationDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(ApplicationDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }
    
    public virtual async Task<Result<IEnumerable<TAggregate>>> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, EntityModel>>[] includes)
    {
        var query = DbSet.AsNoTracking();
        query = includes.Aggregate(query, (current, expression) => current.Include(expression));
        var models = await query.Where(predicate).ToListAsync();
        var aggregates = models.Select(model => 
            TAggregate.Rehydrate(model).ValueOrDefault);
        return Result.Ok(aggregates);
    }
    
    public virtual async Task<Result<TAggregate>> GetById(Guid id)
    {
        var model = await DbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        return model is null ? Result.Fail("not found") : Result.Ok(TAggregate.Rehydrate(model).ValueOrDefault);
    }

    public virtual async Task<Result<IEnumerable<TAggregate>>> GetAll()
    {
        var models = await DbSet.AsNoTracking().ToListAsync();
        var aggregates = models.Select(model => 
            TAggregate.Rehydrate(model).ValueOrDefault);
        return Result.Ok(aggregates);
    }

    public virtual Result<bool> Add(TAggregate aggregate)
    {
        var result = aggregate.Adapt<TEntity>();
        DbSet.Add(result);
        return Result.Ok(true);
    }

    public virtual Result<bool> Update(TAggregate aggregate)
    {
        var result = aggregate.Adapt<TEntity>();
        DbSet.Update(result);
        return Result.Ok(true);
    }

    public virtual Result<bool> Delete(TAggregate aggregate)
    {
        var result = aggregate.Adapt<TEntity>();
        DbSet.Remove(result);
        return Result.Ok(true);
    }

    public async Task<Result<int>> SaveChanges()
    {
        var result = await Db.SaveChangesAsync();
        return Result.Ok(result);
    }
    
    // public async Task<int> CountAsync()
    // {
    //     return await DbSet.CountAsync();
    // }
    //
    // public void Dispose()
    // {
    //     Db?.Dispose();
    // }
}