namespace Powdernaut.Domain.Common;

public interface IEntity<TKey>
{
    TKey? Id { get; }
    EntityId EntityId { get; }
}

public interface IBaseEntity<TKey>
{
    TKey? Id { get; }
}

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey? Id { get; protected set; }

    protected BaseEntity() => _domainEvents = new();


    private readonly List<BaseEvent> _domainEvents;
    public IEnumerable<IEvent> GetEvents() => _domainEvents;

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class Entity<TKey> : IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey? Id { get; protected set; }
    public EntityId EntityId { get; protected set; } = EntityId.CreateInstance();

    protected Entity() => _domainEvents = new();


    private readonly List<BaseEvent> _domainEvents;
    public IEnumerable<IEvent> GetEvents() => _domainEvents;

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public interface IAuditableEntity<TKey> : IEntity<TKey>
     where TKey : struct,
          IComparable,
          IComparable<TKey>,
          IConvertible,
          IEquatable<TKey>,
          IFormattable
{
    bool IsDeleted { get; }
    void Delete();
    void Access();
    DateTime CreatedAt { get; }
    TKey CreatedBy { get; }
    DateTime? LastUpdatedAt { get; }
    TKey? LastUpdatedBy { get; }
    DateTime? DeletedAt { get; }
    TKey? DeletedBy { get; }
}
public abstract class BaseAuditableEntity<TKey> : Entity<TKey>, IAuditableEntity<TKey>
    where TKey : struct,
          IComparable,
          IComparable<TKey>,
          IConvertible,
          IEquatable<TKey>,
          IFormattable
{
    public bool IsDeleted { get; private set; }
    public void Access()
    {
        IsDeleted = false;
    }
    public void Delete()
    {
        IsDeleted = true;
    }
    public DateTime CreatedAt { get; }

    public TKey CreatedBy { get; }

    public DateTime? LastUpdatedAt { get; }

    public TKey? LastUpdatedBy { get; }

    public DateTime? DeletedAt { get; }

    public TKey? DeletedBy { get; }
}
public abstract class BaseAuditableEntity : BaseAuditableEntity<long>
{

}
