using Totostore.Backend.Shared.Events;

namespace Totostore.Backend.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}