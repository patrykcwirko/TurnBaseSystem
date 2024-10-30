using System.Collections;
using System.Collections.Generic;
using Systems.EventBus;
using UnityEngine;
using UnityEngine.Events;
using UnityServiceLocator;

public class UnitsManager : MonoBehaviour
{
    public event UnityAction<UnitTeam> OnStartSelecting;
    public event UnityAction OnEndSelecting;

    #region Seteup
    [Header("Player Unit")]
    [SerializeField] private List<UnitObject> playerUnitsToSpawn = new();
    [SerializeField] private List<Transform> playerUnitTransforms = new();

    [Header("Enemy Unit")]
    [SerializeField] private List<UnitObject> enemyUnitsToSpawn = new();
    [SerializeField] private List<Transform> enemyUnitTransforms = new();
    [SerializeField] private float timeBetweenEnemyTurn = 0.2f;

    [SerializeField] private GameObject unitPrefab = null;
    #endregion

    public List<UnitBehaviour> PlayerUnits { get; private set; } = new();
    public List<UnitBehaviour> EnemyUnits { get; private set; } = new();
    public UnitBehaviour CurrentUnit => unitOrder.First();

    private List<UnitBehaviour> unitOrder = new();
    private EventBinding<OnTurnEnd> turnEndBinding = null;
    private OnTurnEnd onturnEnd;

    private void Awake()
    {
        turnEndBinding = new(OnEndTurn);
        EventBus<OnTurnEnd>.Register(turnEndBinding);

        ServiceLocator.For(this).Register(this);
    }

    private void Start()
    {
        unitOrder.Clear();
        SpawnUnits(playerUnitsToSpawn, playerUnitTransforms, UnitTeam.Player);
        SpawnUnits(enemyUnitsToSpawn, enemyUnitTransforms, UnitTeam.Enemy);

        unitOrder.First().SelectUnit();
    }

    private void SpawnUnits(List<UnitObject> _units, List<Transform> unitTransforms, UnitTeam _team)
    {
        for (int i = 0; i < _units.Count; i++)
        {
            UnitBehaviour _newUnit = ObjectPooler.Instance.GetObject(unitPrefab).GetComponent<UnitBehaviour>();
            _newUnit.Init(_units[i], null, _team);
            _newUnit.transform.SetParent(unitTransforms[i]);
            _newUnit.transform.localPosition = Vector3.zero;
            unitOrder.Add(_newUnit);

            if(_team == UnitTeam.Enemy)
            {
                EnemyUnits.Add(_newUnit);
            }
            else
            {
                PlayerUnits.Add(_newUnit);
            }
        }
    }

    public void UseSkill()
    {
        OnStartSelecting?.Invoke(UnitTeam.Enemy);
    }

    private void OnEndTurn()
    {
        unitOrder.First().DeselectUnit();

        UnitBehaviour _unit = unitOrder.First();
        unitOrder.Remove(_unit);
        unitOrder.Add(_unit);

        unitOrder.First().SelectUnit();

        if (unitOrder.First().Team == UnitTeam.Enemy)
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
