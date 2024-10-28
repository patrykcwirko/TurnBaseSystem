using Abilities;
using TMPro;
using UnityEngine;

public class SkillOrb : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public SkillObject SkillObject { get; private set; }

    public void SetSkill(SkillObject _skill)
    {
        SkillObject = _skill;
        text.text = _skill.Name;
    }
}
