namespace Fiap.TasteEase.Domain.Aggregates.Common;

public class Entity<TKey, TProps>
{
    private List<IEntityEvent> _domainEvents;
    protected TKey? _id;

    protected int? _requestedHashCode;

    public Entity(TProps props, TKey? id)
    {
        _id = Equals(id, default(TKey)) ? default : id;
        _domainEvents = new List<IEntityEvent>();
        Props = props;
    }

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
        return Equals(_id, default(TKey));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj is not Entity<TKey, TProps>)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (Entity<TKey, TProps>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        return Equals(item._id, _id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode =
                    _id!.GetHashCode() ^
                    31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }

        return base.GetHashCode();
    }

    public static bool operator ==(Entity<TKey, TProps> left, Entity<TKey, TProps> right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        return left.Equals(right);
    }

    public static bool operator !=(Entity<TKey, TProps> left, Entity<TKey, TProps> right)
    {
        return !(left == right);
    }
}