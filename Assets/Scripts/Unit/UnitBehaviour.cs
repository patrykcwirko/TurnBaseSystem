using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject selectionSprite = null;
    [SerializeField] private Bar lifeBar = null;
    [SerializeField] private Bar manaBar = null;

    public bool IsEnemy => isEnemy;

    private UnitData data = null;
    private bool isEnemy = false;

    public void Init(UnitObject _object, bool _isEnemy)
    {
        isEnemy = _isEnemy;
        data = new UnitData(_object);
        lifeBar.SetBar(data.CurrnetHP, data.MaxHP);
        manaBar.SetBar(data.CurrnetMana, data.MaxMana);
    }

    public void SelectUnit()
    {
        selectionSprite.SetActive(true);
    }

    public void DeselectUnit()
    {
        selectionSprite.SetActive(false);
    }
}
