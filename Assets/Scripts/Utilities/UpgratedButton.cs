using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgratedButton : Button
{
    public event UnityAction<bool> OnSelectionChanged = null;
    public event UnityAction OnSelectButton = null;
    public event UnityAction OnDeselectButton = null;
    public event UnityAction OnCliked = null;
    public event UnityAction<Button> OnClikedButton = null;

    public object value = null;

    public bool IsSelected { get; private set; }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        IsSelected = true;

        OnSelectButton?.Invoke();
        OnSelectionChanged?.Invoke(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        IsSelected = false;

        OnDeselectButton?.Invoke();
        OnSelectionChanged?.Invoke(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        OnCliked?.Invoke();
        OnClikedButton?.Invoke(this);
    }

    public void OnClick()
    {
        OnCliked?.Invoke();
        OnClikedButton?.Invoke(this);
    }

    public void Deselect()
    {
        OnDeselect(null);
        IsSelected = false;
        OnDeselectButton?.Invoke();
        OnSelectionChanged?.Invoke(false);
    }
}
