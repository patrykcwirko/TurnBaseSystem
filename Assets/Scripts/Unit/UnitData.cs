using Abilities;
using System;
using UnityEngine;

public enum UnitTeam
{
    None = 0,
    Player = 1,
    Enemy = 2
}

[System.Serializable]
public class UnitData : IGUIDContainer
{
    [field: SerializeField] public int MaxHP { get; private set; } = 100;
    [field: SerializeField] public int MaxMana { get; private set; } = 100;
    [field: SerializeField] public int AttackPower { get; private set; } = 100;
    [HideInInspector] public int CurrnetHP = 0;
    [HideInInspector] public int CurrnetMana = 0;
    [HideInInspector] public UnitTeam Team = 0;
    [HideInInspector] public Skill skill;

    [SerializeField] private string guid = string.Empty;

    public UnitData(UnitObject _object, UnitTeam team)
    {
        MaxHP = _object.Data.MaxHP;
        MaxMana = _object.Data.MaxMana;
        AttackPower = _object.Data.AttackPower;
        CurrnetHP = _object.Data.MaxHP;
        CurrnetMana = _object.Data.MaxMana;
        Team = team;
    }

    public UnitData(UnitData _object)
    {
        MaxHP = _object.MaxHP;
        MaxMana = _object.MaxMana;
        CurrnetHP = _object.MaxHP;
        CurrnetMana = _object.MaxMana;
        Team = _object.Team;
    }

    public void AssigneSkill(Skill _skill)
    {
        skill = _skill;
    }

    public string GUID => guid;

    public void GenerateGuid()
    {
        if (guid == string.Empty)
        {
            guid = Guid.NewGuid().ToString();
        }
    }

    public void TakeDamage(int _amount)
    {
        CurrnetHP -= _amount;
    }
}
