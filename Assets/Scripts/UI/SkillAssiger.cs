using Abilities;
using UnityEngine;

public class SkillAssiger : MonoBehaviour
{
    [SerializeField] private SkillSlot skill;
    [SerializeField] private ModifierSlot[] modifiers;

    private void Start()
    {
        for (int i = 0; i < modifiers.Length; i++)
        {
            modifiers[i].Init(this);
        }
    }

    public bool CanAssignMod(Modifier _mod)
    {
        return skill.Skill.CanAssigneModifier(_mod);
    }
}
