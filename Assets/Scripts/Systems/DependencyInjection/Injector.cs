using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Systems.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute
    {
        public InjectAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : Attribute
    {
        public ProvideAttribute() { }
    }

    public interface IDependencyProvider { }

    public class Injector : MonoBehaviour
    {
        private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly Dictionary<Type, object> registry = new();

        public void Awake()
        {
            //Find all modules implementing IDependencyProvider
            var _providers = FindMonoBehaviours().OfType<IDependencyProvider>();

            foreach (var _provider in _providers)
            {
                RegisterProvider(_provider);
            }

            //Find all injectable objects and inject their dependencies
            var _injectables = FindMonoBehaviours().Where(IsInInjectable);

            foreach (var _injectable in _injectables)
            {
                Inject(_injectable);
            }
        }

        private void Inject(MonoBehaviour _injectable)
        {
            Type _type = _injectable.GetType();
            Type _filedType = null;
            object _resolvedInstance=null;
            var _injectableFields = _type.GetFields(bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            if (DebugData.INJECTION) Debug.Log("-------- START Inject Data -----------");

            foreach (var _field in _injectableFields)
            {
                _filedType = _field.FieldType;
                _resolvedInstance = Resolve(_filedType);

                if (_resolvedInstance == null)
                {
                    throw new Exception($"Fiels to resolve {_filedType.Name} for {_type.Name}");
                }

                _field.SetValue(_injectable, _resolvedInstance);
                if (DebugData.INJECTION) Debug.Log($"Injected {_filedType.Name} into {_type.Name}");
            }
        }

        private object Resolve(Type _type)
        {
            registry.TryGetValue(_type, out var _resolveInstance);
            return _resolveInstance;
        }

        private static bool IsInInjectable(MonoBehaviour _obj)
        {
            var _members = _obj.GetType().GetMembers(bindingFlags);
            return _members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        private void RegisterProvider(IDependencyProvider _provider)
        {
            Type _returnType = null;
            object _providedInstance = null;
            MethodInfo[] _methods = _provider.GetType().GetMethods(bindingFlags);

            for (int i = 0; i < _methods.Length; i++)
            {
                if (Attribute.IsDefined(_methods[i], typeof(ProvideAttribute)) == false) continue;

                _returnType = _methods[i].ReturnType;
                _providedInstance = _methods[i].Invoke(_provider, null);

                if (_providedInstance != null)
                {
                    registry.Add(_returnType, _providedInstance);
                    if (DebugData.INJECTION) Debug.Log($"Registered {_returnType.Name} from {_provider.GetType().Name}");
                }
                else
                {
                    throw new Exception($"Provider {_provider.GetType().Name} returned nu    for {_returnType.Name}");
                }
            }
        }

        private static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsOfType<MonoBehaviour>();
        }
    }
}
