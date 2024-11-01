using Abilities;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(SkillObject))]
public class SkillEditor : Editor
{
    private static List<Type> abilityTypes;
    private int selectedAbilityIndex = -1;

    private void OnEnable()
    {
        if (abilityTypes == null)
        {
            abilityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Abilitie)))
                .ToList();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Ability", EditorStyles.boldLabel);

        string[] abilityNames = abilityTypes.Select(t => t.Name).ToArray();
        selectedAbilityIndex = EditorGUILayout.Popup("Select Ability", selectedAbilityIndex, abilityNames);

        if (GUILayout.Button("Add Selected Ability") && selectedAbilityIndex >= 0)
        {
            SkillObject skill = (SkillObject)target;
            Abilitie newAbility = (Abilitie)Activator.CreateInstance(abilityTypes[selectedAbilityIndex]);
            skill.Abilities.Add(newAbility);

            EditorUtility.SetDirty(target);
        }
    }
}
