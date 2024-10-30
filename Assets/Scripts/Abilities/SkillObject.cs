using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Skill")]
    public class SkillObject : ScriptableObject, IGUIDContainer
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeReference] public Abilitie[] Abilities { get; private set; }

        private string guid = string.Empty;
        public string GUID => guid;

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

        public void GenerateGuid()
        {
            if (guid == string.Empty)
            {
                guid = Guid.NewGuid().ToString();
            }
        }
    }

    public class Skill
    {
        public Abilitie[] Abilities { get; private set; }
        public List<string> Modifier;

        public Skill(SkillObject _skillObject)
        {
            this.Abilities = new Abilitie[_skillObject.Abilities.Length];

            for (int i = 0; i < Abilities.Length; i++)
            {
                Abilities[i] = _skillObject.Abilities[i];
            }
        }

        public void DoSkill(UnitsManager _units)
        {
            for (int i = 0; i < Modifier.Count; i++)
            {
                Abilities = AssetsDatabase.GetModifier(Modifier[i]).DoModifier(Abilities);
            }

            for (int i = 0; i < Abilities.Length; i++)
            {
                Abilities[i].DoAbilities(this, _units);
            }
        }

        public void AddModifier(Modifier _mod)
        {
            Modifier.Add(_mod.GUID);
        }

        public void RemoveModifier(Modifier _mod)
        {
            Modifier.Remove(_mod.GUID);
        }

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