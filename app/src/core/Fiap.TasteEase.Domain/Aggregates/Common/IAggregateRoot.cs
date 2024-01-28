using Fiap.TasteEase.Domain.Ports;
using FluentResults;

namespace Fiap.TasteEase.Domain.Aggregates.Common;

public interface IAggregateRoot<TAggregate, TKey, TCreateProps, TRehydrateProps, TModel>
{
    static abstract Result<TAggregate> Create(TCreateProps props);
    static abstract Result<TAggregate> Rehydrate(TRehydrateProps props, TKey key);
    static abstract Result<TAggregate> Rehydrate(TModel props);
}