namespace Fiap.TasteEase.Domain.Aggregates.Common;

public class Entity<TKey, TProps>
{
    public Entity(TProps props, TKey? id)
    {
        _id = Equals(id, default(TKey)) ? default : id;
        _domainEvents = new();
        Props = props;
    }

    protected int? _requestedHashCode;
    protected TKey? _id;
    private List<IEntityEvent> _domainEvents;

    public TKey? Id => _id;
    public TProps Props { get; internal set; }
    public IReadOnlyCollection<IEntityEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IEntityEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IEntityEvent>();
        _domainEvents.Add(eventItem);
    }

    protected void RemoveDomainEvent(IEntityEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    protected void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected bool IsTransient()
    {
        return Equals(this._id, default(TKey));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity<TKey, TProps>)
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity<TKey, TProps> item = (Entity<TKey, TProps>)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return Equals(item._id, this._id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this._id!.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator ==(Entity<TKey, TProps> left, Entity<TKey, TProps> right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TKey, TProps> left, Entity<TKey, TProps> right)
    {
        return !(left == right);
    }
}


