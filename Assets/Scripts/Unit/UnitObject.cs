using UnityEngine;

[CreateAssetMenu(menuName = "Unit")]
public class UnitObject : ScriptableObject, IGUIDContainer
{
    [field: SerializeField] public UnitData Data;

    public string GUID => Data.GUID;

    public void GenerateGuid()
    {
        Data.GenerateGuid();
    }
}
