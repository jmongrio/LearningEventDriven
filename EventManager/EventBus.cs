namespace EventDriven.EventManager
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _handlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Action<object>>();
            }
            _handlers[eventType].Add(e => handler((TEvent)e));
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handler in _handlers[eventType])
                {
                    handler(@event);
                }
            }
        }
    }
}
