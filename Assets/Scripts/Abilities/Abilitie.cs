using System;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class Abilitie
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AbilitiesTag[] Tags { get; private set; }

        public void DoAbilities(Skill _skill, UnitsManager _units)
        {
        }
    }
}