using System;
using System.Collections.Generic;

namespace UnityServiceLocator
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> services = new();
        public IEnumerable<object> RegisteredServices => services.Values;

        public bool TryGet<T>(out T _service) where T : class
        {
            Type _type = typeof(T);

            if (services.TryGetValue(_type, out object _serviceObj))
            {
                _service = _serviceObj as T;
                return true;
            }

            _service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            Type _type = typeof(T);

            if (services.TryGetValue(_type, out object _service))
            {
                return _service as T;
            }

            throw new ArgumentException($"SERVICE MANAGER: Get() - Service of type {_type.FullName} not registered");
        }

        public ServiceManager Register<T>(T _service)
        {
            Type _type = typeof(T);

            if (services.TryAdd(_type, _service) == false)
            {
                UnityEngine.Debug.LogError($"SERVICE MANAGER: Register() - Service of type {_type.FullName} already registered");
            }

            return this;
        }

        public ServiceManager Register(Type _type, object _service)
        {
            if (_type.IsInstanceOfType(_service) == false)
            {
                throw new ArgumentException("Tpe of service does not match type of service interface", nameof(_service));
            }

            if (services.TryAdd(_type, _service) == false)
            {
                UnityEngine.Debug.LogError($"SERVICE MANAGER: Register() - Service of type {_type.FullName} already registered");
            }

            return this;
        }
    }
}