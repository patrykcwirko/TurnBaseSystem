using System;
using UnityEngine;

namespace UnityServiceLocator
{
    [AddComponentMenu("Servicelocator/Servicelocator Global")]
    public class ServiceLocatorGlobalBootstrapper : Bootstrapper
    {
        [SerializeField] private bool dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(dontDestroyOnLoad);
        }
    }
}