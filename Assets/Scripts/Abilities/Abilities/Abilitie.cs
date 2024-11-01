using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public abstract class Abilitie
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AbilitiesTag[] Tags { get; private set; }
        [field: SerializeField] public int SelectionCount { get; private set; } = 0;
        [field: SerializeField] public UnitTeam SelectionTeam { get; private set; } = UnitTeam.Enemy;

        public abstract IEnumerator DoAbilities(Skill _skill, UnitsManager _units);
    }
}