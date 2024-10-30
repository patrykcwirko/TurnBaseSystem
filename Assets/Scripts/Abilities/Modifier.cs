using System;
using UnityEngine;

namespace Abilities
{
    public abstract class Modifier : ScriptableObject, IGUIDContainer
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AbilitiesTag[] Tags { get; private set; }

        private string guid = string.Empty;
        public string GUID => guid;

        public void GenerateGuid()
        {
            if (guid == string.Empty)
            {
                guid = Guid.NewGuid().ToString();
            }
        }

        public abstract Abilitie[] DoModifier(Abilitie[] _abilities);
    }
}