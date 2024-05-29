using System.Collections;
using System.Collections.Generic;
using Systems.EventBus;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    [Header("Player Unit")]
    [SerializeField] private List<UnitObject> playerUnitsToSpawn = new();
    [SerializeField] private List<Transform> playerUnitTransforms = new();

    [Header("Enemy Unit")]
    [SerializeField] private List<UnitObject> enemyUnitsToSpawn = new();
    [SerializeField] private List<Transform> enemyUnitTransforms = new();
    [SerializeField] private float timeBetweenEnemyTurn = 0.2f;

    [SerializeField] private GameObject unitPrefab = null;

    private List<UnitBehaviour> unitOrder = new();
    public List<UnitBehaviour> PlayerUnits { get; private set; } = new();
    public List<UnitBehaviour> EnemyUnits { get; private set; } = new();
    private EventBinding<OnTurnEnd> turnEndBinding = null;
    private OnTurnEnd onturnEnd;

    private void Start()
    {
        turnEndBinding = new(OnEndTurn);
        EventBus<OnTurnEnd>.Register(turnEndBinding);

        unitOrder.Clear();
        SpawnUnits(playerUnitsToSpawn, playerUnitTransforms, false);
        SpawnUnits(enemyUnitsToSpawn, enemyUnitTransforms, true);

        unitOrder.First().SelectUnit();
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

            if(_isEnemy)
            {
                EnemyUnits.Add(_newUnit);
            }
            else
            {
                PlayerUnits.Add(_newUnit);
            }
        }
    }

    private void OnEndTurn()
    {
        unitOrder.First().DeselectUnit();

        UnitBehaviour _unit = unitOrder.First();
        unitOrder.Remove(_unit);
        unitOrder.Add(_unit);

        unitOrder.First().SelectUnit();

        if (unitOrder.First().IsEnemy)
        {
            StartCoroutine(DoEnemyTurn());
        }
    }

    private IEnumerator DoEnemyTurn()
    {
        yield return unitOrder.First().AI.DoAction(this, unitOrder.First());

        yield return Waiters.WaitForSeconds(timeBetweenEnemyTurn);

        EventBus<OnTurnEnd>.Raise(onturnEnd);
    }
}
