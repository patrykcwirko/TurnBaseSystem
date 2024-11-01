using Systems.EventBus;
using TMPro;
using UnityEngine;
using UnityServiceLocator;

public class ActionScreen : MonoBehaviour
{
    [SerializeField] private UpgratedButton endTurnButton = null;
    [SerializeField] private TMP_Text skilName = null;

    private EventBinding<OnSkillAssigned> OnSkillAssigned;
    private EventBinding<OnTurnEnd> turnEnd;
    private OnTurnEnd OnTurnEnd;
    private UnitsManager unitsManager;

    private void Awake()
    {
        OnTurnEnd = new();

        turnEnd = new(RefreshSkillname);
        EventBus<OnTurnEnd>.Register(turnEnd);

        OnSkillAssigned = new(ShowActionBar);
        EventBus<OnSkillAssigned>.Register(OnSkillAssigned);

        ServiceLocator.For(this).Get(out unitsManager);

        endTurnButton.OnCliked += OnCliked;
    }

    private void OnCliked()
    {
        EventBus<OnTurnEnd>.Raise(OnTurnEnd);
    }

    private void ShowActionBar()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void RefreshSkillname()
    {
        skilName.text = unitsManager.CurrentUnit.Data.Skill.Name;
    }
}
