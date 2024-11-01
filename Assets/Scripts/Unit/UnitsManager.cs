using Abilities;
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
    public List<UnitBehaviour> SelectedUnits { get; private set; } = new();

    private List<UnitBehaviour> unitOrder = new();
    private EventBinding<OnTurnEnd> turnEndBinding = null;
    private EventBinding<OnUnitSelect> onUnitSelect = null;
    private EventBinding<OnSkillAssigned> onSkillAssigned = null;
    private OnTurnEnd onturnEnd;

    [SerializeField] private SkillObject testSkill;

    private void Awake()
    {
        turnEndBinding = new(OnEndTurn);
        EventBus<OnTurnEnd>.Register(turnEndBinding);

        onUnitSelect = new(OnUnitSelected);
        EventBus<OnUnitSelect>.Register(onUnitSelect);

        onSkillAssigned = new(Startbattle);
        EventBus<OnSkillAssigned>.Register(onSkillAssigned);

        ServiceLocator.For(this).Register(this);
    }

    private void Startbattle(OnSkillAssigned _units)
    {
        unitOrder.Clear();

        for (int i = 0; i < _units.UnitData.Count; i++)
        {
            UnitBehaviour _newUnit = ObjectPooler.Instance.GetObject(unitPrefab).GetComponent<UnitBehaviour>();
            _newUnit.Init(_units.UnitData[i]);
            _newUnit.transform.SetParent(playerUnitTransforms[i]);
            _newUnit.transform.localPosition = Vector3.zero;
            unitOrder.Add(_newUnit);
            PlayerUnits.Add(_newUnit);
        }

        SpawnEnemy();
        unitOrder.First().SelectUnit();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyUnitsToSpawn.Count; i++)
        {
            UnitBehaviour _newUnit = ObjectPooler.Instance.GetObject(unitPrefab).GetComponent<UnitBehaviour>();
            _newUnit.Init(enemyUnitsToSpawn[i], new(testSkill), UnitTeam.Enemy);
            _newUnit.transform.SetParent(enemyUnitTransforms[i]);
            _newUnit.transform.localPosition = Vector3.zero;
            unitOrder.Add(_newUnit);
            EnemyUnits.Add(_newUnit);
        }
    }

    public void StartSelectionForSkill()
    {
        if (CurrentUnit.Data.Skill.GetUnitsToSelect() != 0)
        {
            OnStartSelecting?.Invoke(UnitTeam.Enemy);
        }
        else
        {
            DoSkill();
        }
    }

    private void OnUnitSelected(OnUnitSelect _selected)
    {
        SelectedUnits.Add(_selected.Unit);

        if (CurrentUnit.Data.Skill.GetUnitsToSelect() == SelectedUnits.Count)
        {
            OnEndSelecting?.Invoke();
        }

        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            SelectedUnits[i].GetComponent<UnitSelector>().ResetSelection();
        }

        DoSkill();
    }

    private void DoSkill()
    {
        StartCoroutine(CurrentUnit.Data.Skill.DoSkill(this));
    }

    private void OnEndTurn()
    {
        SelectedUnits.Clear();
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
