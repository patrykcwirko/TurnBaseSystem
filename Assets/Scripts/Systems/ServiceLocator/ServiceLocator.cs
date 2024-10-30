using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        const string GLOBAL_SERVICE_LOCATOR_NAME = "ServiceLocator [Global]";
        const string SCENE_SERVICE_LOCATOR_NAME = "ServiceLocator [Scene]";

        private static ServiceLocator global;
        private static Dictionary<Scene, ServiceLocator> sceneContainers = new();
        private static List<GameObject> tmpSceneObjects = new();

        private ServiceManager services = new();

        public static ServiceLocator Global
        {
            get
            {
                if (global != null) return global;

                if (FindFirstObjectByType<ServiceLocatorGlobalBootstrapper>() is { } _found)
                {
                    _found.BootstraponDemand();
                    return global;
                }

                var _container = new GameObject(GLOBAL_SERVICE_LOCATOR_NAME, typeof(ServiceLocator));
                _container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstraponDemand();

                return global;
            }
        }

        private void OnDestroy()
        {
            if (this == global)
            {
                global = null;
            }
            else if (sceneContainers.ContainsValue(this))
            {
                sceneContainers.Remove(gameObject.scene);
            }
        }

        internal void ConfigureAsGlobal(bool _dontDestroyOnLoad)
        {
            if (global == this)
            {
                Debug.LogWarning("SERVICE LOCATOR :: ConfigureAsGlobal() - Already configured as gloabal", this);
            }
            else if (global != null)
            {
                Debug.LogError("SERVICE LOCATOR :: ConfigureGlobal() - Another ServiceLocator is already configured as global", this);
            }
            else
            {
                global = this;
                if (_dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            Scene _scene = gameObject.scene;

            if (sceneContainers.ContainsKey(_scene))
            {
                Debug.LogError("SERVICE LOCATOR :: ConfigureForScene() - Another Servicelocator is already configured for this scene", this);
                return;
            }

            sceneContainers.Add(_scene, this);
        }

        public static ServiceLocator For(MonoBehaviour _mb)
        {
            return _mb.GetComponent<ServiceLocator>().OrNull() ?? ForSceneOf(_mb) ?? Global;
        }

        public static ServiceLocator ForSceneOf(MonoBehaviour _mb)
        {
            Scene _scene = _mb.gameObject.scene;

            if (sceneContainers.TryGetValue(_scene, out var _container) && _container != _mb)
            {
                return _container;
            }

            tmpSceneObjects.Clear();
            _scene.GetRootGameObjects(tmpSceneObjects);

            foreach (GameObject _obj in tmpSceneObjects.Where(_obj => _obj.GetComponent<ServiceLocatorSceneBootstrapper>() != null))
            {
                if (_obj.TryGetComponent(out ServiceLocatorSceneBootstrapper _bootstrapper) && _bootstrapper.Container != _mb)
                {
                    _bootstrapper.BootstraponDemand();
                    return _bootstrapper.Container;
                }
            }

            return Global;
        }

        public ServiceLocator Register<T>(T _sercive)
        {
            services.Register(_sercive);
            return this;
        }

        public ServiceLocator Register(Type _type, object _sercive)
        {
            services.Register(_type, _sercive);
            return this;
        }

        public ServiceLocator Get<T>(out T _service) where T : class
        {
            if (TryGetService(out _service)) return this;

            if (TryGetnextInHierarchy(out ServiceLocator _container))
            {
                _container.Get(out _service);
                return this;
            }

            throw new ArgumentException($"SERVICE LOCATOR: Get() - Service of type {typeof(T).FullName} not regtistered");
        }

        public T Get<T>() where T : class
        {
            T _service = null;

            if (TryGetService(out _service)) return _service;

            if (TryGetnextInHierarchy(out ServiceLocator _container))
            {
                _container.Get(out _service);
                return _service;
            }

            throw new ArgumentException($"SERVICE LOCATOR: Get() - Service of type {typeof(T).FullName} not regtistered");
        }

        private bool TryGetService<T>(out T _sercive) where T : class
        {
            return services.TryGet(out _sercive);
        }

        private bool TryGetnextInHierarchy(out ServiceLocator _container)
        {
            if (this == global)
            {
                _container = null;
                return false;
            }

            _container = transform.parent.OrNull().GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return _container != null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            global = null;
            sceneContainers = new();
            tmpSceneObjects = new();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Global")]
        private static void AddGlobal()
        {
            var _go = new GameObject(GLOBAL_SERVICE_LOCATOR_NAME, typeof(ServiceLocatorGlobalBootstrapper));
        }

        [MenuItem("GameObject/ServiceLocator/Add Scene")]
        private static void AddScene()
        {
            var _go = new GameObject(SCENE_SERVICE_LOCATOR_NAME, typeof(ServiceLocatorSceneBootstrapper));
        }
#endif
    }
}