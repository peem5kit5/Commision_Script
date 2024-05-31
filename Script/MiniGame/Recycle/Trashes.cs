using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trashes : DragDropItem
{
    public string TrashID;
    [SerializeField] private RecycleManager recycleManager;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!recycleManager)
            recycleManager = FindObjectOfType<RecycleManager>();
    }

    private void Start()
    {
        if (!recycleManager)
            recycleManager = FindObjectOfType<RecycleManager>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 _localPoint);

        if (RectTransformUtility.RectangleContainsScreenPoint(Canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera))
            RectTransform.anchoredPosition = _localPoint;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        CheckTrash(eventData);
        base.OnEndDrag(eventData);
    }

    private void CheckTrash(PointerEventData _eventData)
    {
        Bin _bin = _eventData.pointerEnter.GetComponent<Bin>();
        if(_bin != null)
        {
            if (recycleManager.CheckTrash(_bin, this))
            {
                Debug.Log("Correct : " + _bin.BinID + "/ " + TrashID);
                if (AudioSystem.Instance != null)
                    AudioSystem.Instance.PlayTrashGrabSound();

                Destroy(gameObject);
            }
        }
    }
}
