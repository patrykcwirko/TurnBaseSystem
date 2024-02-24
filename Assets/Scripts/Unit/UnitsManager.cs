using System.Collections;
using System.Collections.Generic;
using Systems.EventBus;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    [Header("Player Unit")]
    [SerializeField] private List<UnitObject> playerUnits = new();
    [SerializeField] private List<Transform> playerUnitTransforms = new();

    [Header("Enemy Unit")]
    [SerializeField] private List<UnitObject> enemyUnits = new();
    [SerializeField] private List<Transform> enemyUnitTransforms = new();

    [SerializeField] private GameObject unitPrefab = null;

    private List<UnitBehaviour> unitOrder = new();
    private EventBinding<OnTurnEnd> turnEndBinding = null;
    private OnTurnEnd onturnEnd;

    private void Start()
    {
        turnEndBinding = new(OnEndTurn);
        EventBus<OnTurnEnd>.Register(turnEndBinding);

        unitOrder.Clear();
        SpawnUnits(playerUnits, playerUnitTransforms, false);
        SpawnUnits(enemyUnits, enemyUnitTransforms, true);

        unitOrder[0].SelectUnit();
    }

    private void SpawnUnits(List<UnitObject> _units, List<Transform> unitTransforms, bool _isEnemy)
    {
        for (int i = 0; i < _units.Count; i++)
        {
            UnitBehaviour _newUnit = ObjectPooler.Instance.GetObject(unitPrefab).GetComponent<UnitBehaviour>();
            _newUnit.Init(_units[i], _isEnemy);
            _newUnit.transform.SetParent(unitTransforms[i]);
            _newUnit.transform.localPosition = Vector3.zero;
            unitOrder.Add(_newUnit);
        }
    }

    private void OnEndTurn()
    {
        unitOrder[0].DeselectUnit();

        UnitBehaviour _unit = unitOrder[0];
        unitOrder.Remove(_unit);
        unitOrder.Add(_unit);

        unitOrder[0].SelectUnit();

        if (unitOrder[0].IsEnemy)
        {
            StartCoroutine(DoEnemyTurn());
        }
    }

    private IEnumerator DoEnemyTurn()
    {
        yield return new WaitForSeconds(0.1f);

        EventBus<OnTurnEnd>.Raise(onturnEnd);
    }
}
