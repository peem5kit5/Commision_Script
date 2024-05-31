using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashChecker : MonoBehaviour
{
    public string PlaceID;
    [SerializeField] private TutorialManager tutorialManager;

    private void OnValidate()
    {
        if (!tutorialManager)
            tutorialManager = FindObjectOfType<TutorialManager>();
    }

    public void SetTrashPrefs()
    {
        PlayerPrefs.SetString(PlaceID, PlaceID);
    }
}
