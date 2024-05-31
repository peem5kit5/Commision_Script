using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashPlace : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public string LocationName;
    public bool IsFinished;

    private void OnValidate()
    {
        if (!image)
            image = GetComponent<Image>();

        if (!button)
            button = GetComponent<Button>();
    }

    public void UpdateTrashCondition() 
    {
        image.enabled = !IsFinished;
        button.interactable = !IsFinished;
    } 
}
