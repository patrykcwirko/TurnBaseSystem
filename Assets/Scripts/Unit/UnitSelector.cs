using Systems.EventBus;
using UnityEngine;
using UnityServiceLocator;

[RequireComponent(typeof(UnitBehaviour))]
public class UnitSelector : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectionSprite;
    private UnitTeam team = UnitTeam.None;
    private UnitsManager unitsManager;
    private UnitBehaviour unit;

    private void Awake()
    {
        unit = GetComponent<UnitBehaviour>();
    }

    private void Start()
    {
        ServiceLocator.For(this).Get(out unitsManager);
        unitsManager.OnStartSelecting += ChangeSelectionMode;
        unitsManager.OnEndSelecting += EndSelecting;
    }

    private void EndSelecting()
    {
        ChangeSelectionMode(UnitTeam.None);
    }

    private void OnMouseDown()
    {
        //if (team == UnitTeam.None) return;

        if (unit.Team == team)
        {
            EventBus<OnUnitSelect>.Raise(new OnUnitSelect() { Unit = unit });
        }
    }

    private void OnMouseEnter()
    {
        if (team == UnitTeam.None) return;
        Debug.Log("select");

        selectionSprite.color = AssetsDatabase.GetSelectionColor(unit.Team);
    }

    private void OnMouseExit()
    {
        selectionSprite.color = Color.clear;
    }

    private void ChangeSelectionMode(UnitTeam _team)
    {
        team = _team;
    }
}
