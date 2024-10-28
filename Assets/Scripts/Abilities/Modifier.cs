using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Modifier")]
    public class Modifier : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AbilitiesTag[] Tags { get; private set; }
    }
}