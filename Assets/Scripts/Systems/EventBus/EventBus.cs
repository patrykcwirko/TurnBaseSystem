using System.Collections.Generic;
using UnityEngine;

namespace Systems.EventBus
{
    public interface IEvent { }

    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> bindings = new();

        public static void Register(IEventBinding<T> _binding) => bindings.Add(_binding);
        public static void Deregister(IEventBinding<T> _binding) => bindings.Remove(_binding);

        public static void Raise(T _event)
        {
            foreach (var _binding in bindings)
            {
                _binding.OnEvent.Invoke(_event);
                _binding.OnEventNoArgs.Invoke();
            }
        }

        static void Clear()
        {
            if (DebugData.EVENTBUS) Debug.Log($"Clearing {typeof(T).Name} binding");
            bindings.Clear();
        }
    }
}