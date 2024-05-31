using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas Canvas => canvas;
    public RectTransform RectTransform => rectTransform;

    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectTransform;

    public Vector2 OldPosition;

    public virtual void OnValidate()
    {
        if (!canvas)
            canvas = gameObject.transform.root.GetComponent<Canvas>();

        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
            OldPosition = rectTransform.anchoredPosition;
        }
    }

    private void Awake()
    {
        if(!canvas)
            canvas = gameObject.transform.root.GetComponent<Canvas>();

        if (!rectTransform)
            rectTransform = GetComponent<RectTransform>();

        OldPosition = rectTransform.anchoredPosition;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
   
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = OldPosition;
    }
}
