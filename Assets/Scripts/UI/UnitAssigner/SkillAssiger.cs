using Abilities;
using UnityEngine;

public class SkillAssiger : MonoBehaviour
{
    [SerializeField] private SkillSlot skill;
    [SerializeField] private ModifierSlot[] modifiers;

    public SkillObject Skill => skill.Skill;

    private UnitObject unit;

    private void Start()
    {
        for (int i = 0; i < modifiers.Length; i++)
        {
            modifiers[i].Init(this);
        }
    }

    public void Init(UnitObject _unit)
    {
        unit = _unit;
    }

    public bool CanAssignMod(Modifier _mod)
    {
        return skill.Skill.CanAssigneModifier(_mod);
    }

    public UnitData GetFinalUnit()
    {
        UnitData _unit = new(unit, UnitTeam.Player);
        _unit.AssigneSkill(new(skill.Skill));

        for (int i = 0; i < modifiers.Length; i++)
        {
            if (modifiers[i].Modifier != null)
            {
                _unit.Skill.AddModifier(modifiers[i].Modifier);
            }
        }

        return _unit;
    }
}
