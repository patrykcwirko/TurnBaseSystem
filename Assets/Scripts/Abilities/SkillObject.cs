using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Skill")]
    public class SkillObject : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Abilitie[] Abilities { get; private set; }

        public bool ContainTag(AbilitiesTag _tag)
        {
            for (int i = 0; i < Abilities.Length; i++)
            {
                if (Abilities[i].Tags.Contains(_tag))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Skill
    {
        public SkillObject skillObject { get; private set; }
        public List<Modifier> Modifier;

        public Skill(SkillObject _skillObject)
        {
            this.skillObject = _skillObject;
        }

        public void DoSkill(UnitsManager _units)
        {
            for (int i = 0; i < skillObject.Abilities.Length; i++)
            {
                skillObject.Abilities[i].DoAbilities(this, _units);
            }
        }
    }

    public static class SkillExtensions
    {
        public static bool CanAssigneModifier(this SkillObject _skill, Modifier _mod)
        {
            for (int i = 0; i < _mod.Tags.Length; i++)
            {
                if (_skill.ContainTag(_mod.Tags[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}