using UnityEngine;

namespace UnityServiceLocator
{
    [AddComponentMenu("Servicelocator/Servicelocator Scene")]
    public class ServiceLocatorSceneBootstrapper : Bootstrapper
    {
        [SerializeField] private bool dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            Container.ConfigureForScene();
        }
    }
}