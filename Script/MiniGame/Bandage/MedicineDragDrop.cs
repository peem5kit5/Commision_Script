using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedicineDragDrop : DragDropItem
{
    public int ID;
    [SerializeField] private WoundManager woundManager;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!woundManager)
            woundManager = FindObjectOfType<WoundManager>();
    }

    public override void OnEndDrag(PointerEventData _eventData)
    {
        CheckCollider(_eventData);
        base.OnEndDrag(_eventData);
    }

    private void CheckCollider(PointerEventData _eventData)
    {
        Wounds _wounds = _eventData.pointerEnter.GetComponent<Wounds>();
        if (_wounds != null)
        {
            if (woundManager.CheckWounds(ID))
                Destroy(gameObject);
        }
    }
}
