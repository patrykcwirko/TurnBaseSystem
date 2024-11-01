using System.Collections.Generic;
using Systems.EventBus;
using UnityEngine;

public class UnitsAssigner : MonoBehaviour
{
    [SerializeField] private SkillAssiger skillAssigerPrefab;
    [SerializeField] private Transform skillAssigerParent;
    [SerializeField] private List<UnitObject> units;

    private List<SkillAssiger> skillAssigers;
    private OnSkillAssigned skillAssigned;

    private void Start()
    {
        skillAssigers = new List<SkillAssiger>();
        skillAssigned = new();

        for (int i = 0; i < units.Count; i++)
        {
            SkillAssiger _newUnit = ObjectPooler.Instance.GetObject(skillAssigerPrefab.gameObject).GetComponent<SkillAssiger>();
            _newUnit.Init(units[i]);
            _newUnit.transform.SetParent(skillAssigerParent);
            _newUnit.transform.localPosition = Vector3.zero;
            skillAssigers.Add(_newUnit);
        }
    }

    public void StartBattle()
    {
        List<UnitData> _units = new();

        for (int i = 0; i < skillAssigers.Count; i++)
        {
            if (skillAssigers[i].Skill == null) return;

            _units.Add(skillAssigers[i].GetFinalUnit());
        }

        skillAssigned.UnitData = _units;
        EventBus<OnSkillAssigned>.Raise(skillAssigned);
        gameObject.SetActive(false);
    }
}
