using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject selectionSprite = null;

    public bool IsEnemy => isEnemy;

    private UnitData data = null;
    private bool isEnemy = false;

    public void Init(UnitObject _object, bool _isEnemy)
    {
        data = new UnitData(_object);
        isEnemy = _isEnemy;
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
