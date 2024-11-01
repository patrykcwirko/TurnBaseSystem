using System;
using System.Collections;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class DamageAbilities : Abilitie
    {
        public int amount;

        public override IEnumerator DoAbilities(Skill _skill, UnitsManager _units)
        {
            for (int i = 0; i < _units.SelectedUnits.Count; i++)
            {
                _units.SelectedUnits[i].TakeDamage(amount);
            }

            yield return null;
        }
    }
}