using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveLoadManager))]
public class TrashPickManager : MonoBehaviour
{
    public TrashPlace[] TrashLocations;

    public void Start()
    {
        CheckOnLoad();
    }

    private void CheckOnLoad()
    {
        foreach(var _place in TrashLocations)
        {
            if (PlayerPrefs.HasKey(_place.LocationName))
                _place.IsFinished = true;

            _place.UpdateTrashCondition();
        }
    }
}
