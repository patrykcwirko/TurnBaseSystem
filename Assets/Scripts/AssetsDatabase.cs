using Abilities;
using System.Collections.Generic;
using UnityEngine;

public class AssetsDatabase : StaticBehaviour<AssetsDatabase>
{
    [Header("Scriptable Reference")]
    [SerializeField] private List<SkillObject> skills;
    [SerializeField] private List<Modifier> modifier;

    [Header("Colors")]
    [SerializeField] private Color PlayerUnitSelectionColor;
    [SerializeField] private Color EnemyUnitSelectionColor;

    public static List<SkillObject> Skills => instance.skills;
    public static List<Modifier> Modifier => instance.modifier;

    private Dictionary<string, Modifier> modifierDictionary = new();

    public override void AsigneInstance()
    {
        instance = this;
    }

    public override void Awake()
    {
        base.Awake();

        modifierDictionary = new();

        for (int i = 0; i < modifier.Count; i++)
        {
            modifierDictionary.Add(modifier[i].GUID, modifier[i]);
        }
    }

    public static Modifier GetModifier(string _guid) => instance.modifierDictionary[_guid];

    public static Color GetSelectionColor(UnitTeam _team)
    {
        switch (_team)
        {
            case UnitTeam.Player:
                return instance.PlayerUnitSelectionColor;
            case UnitTeam.Enemy:
                return instance.EnemyUnitSelectionColor;
            case UnitTeam.None:
            default:
                return Color.clear;
        }
    }
}
