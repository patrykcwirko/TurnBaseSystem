using System;

namespace Systems.EventBus
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> OnEvent = _ => { };
        private Action OnEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent { get => OnEvent; set => OnEvent = value; }
        Action IEventBinding<T>.OnEventNoArgs { get => OnEventNoArgs; set => OnEventNoArgs = value; }

        public EventBinding(Action<T> _onEvent) => this.OnEvent = _onEvent;
        public EventBinding(Action _onEventNoArgs) => this.OnEventNoArgs = _onEventNoArgs;

        public void Add(Action _onEventNoArgs) => this.OnEventNoArgs += _onEventNoArgs;
        public void Remnove(Action _onEventNoArgs) => this.OnEventNoArgs -= _onEventNoArgs;
        public void Add(Action<T> _onEvent) => this.OnEvent += _onEvent;
        public void Remove(Action<T> _onEvent) => this.OnEvent -= _onEvent;

    }
}
