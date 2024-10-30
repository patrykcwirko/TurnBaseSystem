using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class DamageAbilities : Abilitie
    {
        public int amount;

        public override bool MustSetectUnit() => true;

        public override bool DoAbilities(Skill _skill, UnitsManager _units)
        {
            //_units.CurrentUnit

            return false;
        }
    }
}