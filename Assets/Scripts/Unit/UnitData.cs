using System;
using UnityEngine;

[System.Serializable]
public class UnitData : IGUIDContainer
{
    [field: SerializeField] public int MaxHP { get; private set; } = 100;
    [field: SerializeField] public int MaxMana { get; private set; } = 100;
    [field: SerializeField] public int AttackPower { get; private set; } = 100;
    [HideInInspector] public int CurrnetHP = 0;
    [HideInInspector] public int CurrnetMana = 0;

    [SerializeField] private string guid = string.Empty;

    public UnitData(UnitObject _object)
    {
        MaxHP = _object.Data.MaxHP;
        MaxMana = _object.Data.MaxMana;
        AttackPower = _object.Data.AttackPower;
        CurrnetHP = _object.Data.MaxHP;
        CurrnetMana = _object.Data.MaxMana;
    }

    public UnitData(UnitData _object)
    {
        MaxHP = _object.MaxHP;
        MaxMana = _object.MaxMana;
        CurrnetHP = _object.MaxHP;
        CurrnetMana = _object.MaxMana;
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
