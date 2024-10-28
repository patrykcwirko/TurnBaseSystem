using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityAction<bool> OnDragChanged = null;

    public Canvas Canvas;
    public Vector2 startPosition = Vector2.zero;
    public bool cardInSlot = false;
    public bool BlokMovement = false;

    private RectTransform rectTransform = null;
    private CanvasGroup canvasGroup = null;
    private Transform parentTransform = null;
    private int childIndex = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        if (BlokMovement)
        {
            return;
        }

        startPosition = rectTransform.position;

        parentTransform = transform.parent;
        childIndex = transform.GetSiblingIndex();
        transform.SetParent(Canvas.transform);
        transform.SetSiblingIndex(Canvas.transform.childCount - 1);

    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        if (BlokMovement)
        {
            return;
        }

        canvasGroup.blocksRaycasts = false;
        OnDragChanged?.Invoke(true);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if (BlokMovement)
        {
            return;
        }

        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        if (BlokMovement)
        {
            return;
        }

        if (parentTransform != null)
        {
            transform.SetParent(parentTransform);
            transform.SetSiblingIndex(childIndex);
        }

        canvasGroup.blocksRaycasts = true;
        OnDragChanged?.Invoke(false);

        if (cardInSlot == false)
        {
            RestartPosition();
        }
    }

    public void RestartPosition()
    {
        rectTransform.position = startPosition;
    }
}
