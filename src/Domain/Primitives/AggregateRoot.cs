using MediatR;

namespace Domain.Primitives;

public abstract class AggregateRoot
{
    private readonly List<INotification> _domainEvents = new();

    public IReadOnlyCollection<INotification> GetDomainEvents()
        => _domainEvents.AsReadOnly();

   
    protected void AddDomainEvent(INotification eventItem)
        => _domainEvents.Add(eventItem);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}