using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    [SerializeField] private List<UnitObject> units = new();
    [SerializeField] private List<Transform> unitTransforms = new();
    [SerializeField] private GameObject unitPrefab = null;

    private void Start()
    {
        for (int i = 0; i < units.Count; i++)
        {
            UnitBehaviour _newUnit = ObjectPooler.Instance.GetObject(unitPrefab).GetComponent<UnitBehaviour>();
            _newUnit.Init(units[i]);
            _newUnit.transform.SetParent(unitTransforms[i]);
            _newUnit.transform.localPosition = Vector3.zero;
        }
    }
}
