using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Ingredient : DragDropItem
{
    public int ID;

    public bool IsBread;
    public bool IsPlastic;
    public bool IsHamCheese;

    public override void OnEndDrag(PointerEventData _eventData)
    {
        SandwichManager _sandWichManager = _eventData.pointerEnter.GetComponent<SandwichManager>();

        if(_sandWichManager != null)
        {
            if(IsBread)
            {
                _sandWichManager.IsBread = true;
            }

            if(IsPlastic)
            {
                _sandWichManager.IsPlastic = true;
            }

            if (IsHamCheese)
            {
                _sandWichManager.IsHamCheese = true;
            }
                

            if (_sandWichManager.Check(this)) Destroy(gameObject);
        }

        base.OnEndDrag(_eventData);
    }

}
