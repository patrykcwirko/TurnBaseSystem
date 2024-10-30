using Abilities;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject selectionSprite = null;
    [SerializeField] private Bar lifeBar = null;
    [SerializeField] private Bar manaBar = null;

    public UnitTeam Team => data.Team;
    public UnitData Data => data;
    public UnitAI AI => ai;

    private UnitData data = null;
    private UnitAI ai = null;
    private bool isEnemy = false;

    public void Init(UnitObject _object, Skill _skill, UnitTeam _team)
    {
        data = new UnitData(_object, _team);
        ai = _object.AI;
        data.AssigneSkill(_skill);
        RefreshVisual();
    }

    public void SelectUnit()
    {
        selectionSprite.SetActive(true);
    }

    public void DeselectUnit()
    {
        selectionSprite.SetActive(false);
    }

    public void TakeDamage(int _amount)
    {
        data.TakeDamage(_amount);
        RefreshVisual();
    }

    private void RefreshVisual()
    {
        lifeBar.SetBar(data.CurrnetHP, data.MaxHP);
        manaBar.SetBar(data.CurrnetMana, data.MaxMana);
    }
}
