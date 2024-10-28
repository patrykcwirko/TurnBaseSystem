using Abilities;
using System.Collections.Generic;
using UnityEngine;

public class AssetsDatabase : StaticBehaviour<AssetsDatabase>
{
    [SerializeField] private List<SkillObject> skills;
    [SerializeField] private List<Modifier> modifier;

    public static List<SkillObject> Skills => instance.skills;
    public static List<Modifier> Modifier => instance.modifier;

    public override void AsigneInstance()
    {
        instance = this;
    }
}
