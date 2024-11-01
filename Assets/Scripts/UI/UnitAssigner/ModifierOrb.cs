using Abilities;
using TMPro;
using UnityEngine;

public class ModifierOrb : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public Modifier Modifier { get; private set; }

    public void SetMod(Modifier _skill)
    {
        Modifier = _skill;
        text.text = _skill.Name;
    }
}
