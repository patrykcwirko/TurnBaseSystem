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
    private bool selected = false;

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

    private void OnMouseDown()
    {
        if (team == UnitTeam.None) return;
        if (unit.Team != team) return;

        EventBus<OnUnitSelect>.Raise(new OnUnitSelect() { Unit = unit });
        selected = true;       
    }

    private void OnMouseEnter()
    {
        if (team == UnitTeam.None) return;
        if (unit.Team != team) return;

        selectionSprite.color = AssetsDatabase.GetSelectionColor(unit.Team);
    }

    private void OnMouseExit()
    {
        if (selected) return;

        selectionSprite.color = Color.clear;
    }

    public void ResetSelection()
    {
        selected = false;
        selectionSprite.color = Color.clear;
    }

    private void ChangeSelectionMode(UnitTeam _team)
    {
        team = _team;
    }

    private void EndSelecting()
    {
        ChangeSelectionMode(UnitTeam.None);
    }
}
