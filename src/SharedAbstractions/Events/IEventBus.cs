namespace SharedAbstractions.Events
{
    public interface IEventBus
    {
        ValueTask SendAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}