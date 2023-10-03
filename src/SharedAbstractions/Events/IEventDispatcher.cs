namespace SharedAbstractions.Events;

public interface IEventDispatcher
{
    ValueTask DispatchAsync<TEvent>(TEvent @event) where TEvent : IEvent;

    void Subscribe();
}