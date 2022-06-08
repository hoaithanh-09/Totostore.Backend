using Totostore.Backend.Shared.Events;

namespace Totostore.Backend.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}