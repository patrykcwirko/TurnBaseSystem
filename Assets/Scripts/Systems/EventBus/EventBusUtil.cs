using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Systems.EventBus
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange _state)
        {
            PlayModeState = _state;

            if (_state == PlayModeStateChange.ExitingEditMode)
            {
                ClearAllBuses();
            }
        }

#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefineAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        public static void ClearAllBuses()
        {
            MethodInfo _clearMethod = null;

            for (int i = 0; i < EventBusTypes.Count; i++)
            {
                _clearMethod = EventBusTypes[i].GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                _clearMethod.Invoke(null, null);
                if (DebugData.EVENTBUS) Debug.Log($"Clear EventBus<{EventBusTypes[i].Name}>");
            }
        }

        private static IReadOnlyList<Type> InitializeAllBuses()
        {
            List<Type> _allBuses = new List<Type>();
            Type _typedef = typeof(EventBus<>);

            if (DebugData.EVENTBUS) Debug.Log("-------- START Initialize Event Buses -----------");

            for (int i = 0; i < EventTypes.Count; i++)
            {
                _allBuses.Add(_typedef.MakeGenericType(EventTypes[i]));
                if (DebugData.EVENTBUS) Debug.Log($"Initialized EventBus<{EventTypes[i].Name}>");
            }

            return _allBuses;
        }
    }
}