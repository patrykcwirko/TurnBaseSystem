using UnityEngine;

[CreateAssetMenu(menuName = "Unit")]
public class UnitObject : ScriptableObject, IGUIDContainer
{
    [field: SerializeField] public UnitData Data;
    [SerializeField] private UnitAI aI = null;

    public UnitAI AI => aI;
    public string GUID => Data.GUID;

    public void GenerateGuid()
    {
        Data.GenerateGuid();
    }
}
