using System;
using UnityEngine;

[System.Serializable]
public class UnitData : IGUIDContainer
{
    [SerializeField] private int HP = 100;
    [SerializeField] private int Mana = 100;

    [SerializeField] private string guid = string.Empty;

    public UnitData(UnitObject _object)
    {
        HP = _object.Data.HP;
        Mana = _object.Data.Mana;
    }

    public UnitData(UnitData _object)
    {
        HP = _object.HP;
        Mana = _object.Mana;
    }

    public string GUID => guid;

    public void GenerateGuid()
    {
        if (guid == string.Empty)
        {
            guid = Guid.NewGuid().ToString();
        }
    }
}
