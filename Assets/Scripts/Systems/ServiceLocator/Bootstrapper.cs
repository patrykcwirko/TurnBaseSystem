using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ServiceLocator))]
    public abstract class Bootstrapper : MonoBehaviour
    {
        private ServiceLocator container = null;
        bool hasBeenBootstrapped = false;

        internal ServiceLocator Container => container.OrNull() ?? (container = GetComponent<ServiceLocator>());

        private void Awake() => BootstraponDemand();

        public void BootstraponDemand()
        {
            if (hasBeenBootstrapped) return;
            hasBeenBootstrapped = true;
            Bootstrap();
        }

        protected abstract void Bootstrap();
    }
}