using System.Collections.Generic;
using UnityEngine;

public class SkillModifierDisplay : MonoBehaviour
{
    [SerializeField] private Transform orbsParent;
    [SerializeField] private SkillOrb skillOrbPrefab;
    [SerializeField] private ModifierOrb modifierOrbPrefab;
    [SerializeField] private Canvas canvas;

    private List<SkillOrb> orbs = new();
    private List<ModifierOrb> modifiers = new();

    private void Start()
    {
        DisplaySkills();
    }

    public void DisplaySkills()
    {
        ClearOrbs();

        for (int i = 0; i < AssetsDatabase.Skills.Count; i++)
        {
            SkillOrb _orb = ObjectPooler.Instance.GetObject(skillOrbPrefab.gameObject).GetComponent<SkillOrb>();
            _orb.SetSkill(AssetsDatabase.Skills[i]);
            _orb.transform.SetParent(orbsParent);
            _orb.GetComponent<DragDrop>().Canvas = canvas;
            orbs.Add(_orb);
        }
    }

    public void DisplayModifier()
    {
        ClearOrbs();

        for (int i = 0; i < AssetsDatabase.Modifier.Count; i++)
        {
            ModifierOrb _orb = ObjectPooler.Instance.GetObject(modifierOrbPrefab.gameObject).GetComponent<ModifierOrb>();
            _orb.SetMod(AssetsDatabase.Modifier[i]);
            _orb.transform.SetParent(orbsParent);
            _orb.GetComponent<DragDrop>().Canvas = canvas;
            modifiers.Add(_orb);
        }
    }

    private void ClearOrbs()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            ObjectPooler.Instance.ReturnGameObject(orbs[i].gameObject);
            orbs.RemoveAt(i);
        }

        for (int i = 0; i < modifiers.Count; i++)
        {
            ObjectPooler.Instance.ReturnGameObject(modifiers[i].gameObject);
            modifiers.RemoveAt(i);
        }
    }
}
