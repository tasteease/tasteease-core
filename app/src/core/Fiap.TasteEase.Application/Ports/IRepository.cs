using Fiap.TasteEase.Domain.Ports;
using FluentResults;
using System.Linq.Expressions;

namespace Fiap.TasteEase.Application.Ports;

public interface IRepository<TEntity, TAggregate>
{
    Task<Result<IEnumerable<TAggregate>>> Get(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, EntityModel>>[] includes);
    Task<Result<TAggregate>> GetById(Guid id);
    Task<Result<IEnumerable<TAggregate>>> GetAll();
    Result<bool> Add(TAggregate model);
    Result<bool> Update(TAggregate aggregate);
    Result<bool> Delete(TAggregate aggregate);
    Task<Result<int>> SaveChanges();
}