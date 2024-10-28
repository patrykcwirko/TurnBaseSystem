using UnityEngine;
using UnityEngine.EventSystems;

namespace Slots
{
    public abstract class Slot : MonoBehaviour, IDropHandler
    {
        public bool oquiped = false;

        public abstract void OnDrop(PointerEventData eventData);
    }
}