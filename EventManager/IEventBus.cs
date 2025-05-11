namespace EventDriven.EventManager
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
        void Publish<TEvent>(TEvent @event) where TEvent : class;
    }
}
