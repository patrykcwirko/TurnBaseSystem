using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private UnitData data = null;

    public void Init(UnitObject _object)
    {
        data = new UnitData(_object);
    }
}
