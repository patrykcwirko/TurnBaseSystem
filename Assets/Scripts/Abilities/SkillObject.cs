using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Systems.EventBus;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Skill")]
    public class SkillObject : ScriptableObject, IGUIDContainer
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeReference] public List<Abilitie> Abilities { get; private set; }

        private string guid = string.Empty;
        public string GUID => guid;

        public bool ContainTag(AbilitiesTag _tag)
        {
            for (int i = 0; i < Abilities.Count; i++)
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
        public string Name { get; private set; }
        public Abilitie[] Abilities { get; private set; }
        public List<string> Modifier;

        public Skill(SkillObject _skillObject)
        {
            Modifier = new();
            Name = _skillObject.Name;

            this.Abilities = new Abilitie[_skillObject.Abilities.Count];

            for (int i = 0; i < Abilities.Length; i++)
            {
                Abilities[i] = _skillObject.Abilities[i];
            }
        }

        public IEnumerator DoSkill(UnitsManager _units)
        {
            for (int i = 0; i < Abilities.Length; i++)
            {
                yield return Abilities[i].DoAbilities(this, _units);
            }

            EventBus<OnTurnEnd>.Raise(new());
        }

        public int GetUnitsToSelect()
        {
            int _result = 0;

            for (int i = 0; i < Abilities.Length; i++)
            {
                _result += Abilities[i].SelectionCount;
            }

            return _result;
        }

        public void AddModifier(Modifier _mod)
        {
            Abilities = _mod.DoModifier(Abilities);

            Modifier.Add(_mod.GUID);
        }

        public void RemoveModifier(Modifier _mod)
        {
            Abilities = _mod.UndoModifier(Abilities);

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