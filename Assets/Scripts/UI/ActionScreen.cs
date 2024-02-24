using Systems.EventBus;
using UnityEngine;

public class ActionScreen : MonoBehaviour
{
    [SerializeField] private UpgratedButton endTurnButton = null;

    private OnTurnEnd OnTurnEnd;

    private void Start()
    {
        OnTurnEnd = new();

        endTurnButton.OnCliked += OnCliked;
    }

    private void OnCliked()
    {
        EventBus<OnTurnEnd>.Raise(OnTurnEnd);
    }
}
