using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/DialogueData")]
public class DialogueData : ScriptableObject
{
    [Header("Property")]
    public string TalkerName;
    public string AnimationNames;
    public string NextSceneName;
    public AudioClip AfterDialogueSound;
    public bool HaveChoice;

    [Header("BG Setting")]
    public Sprite BGToChange;
    public string BGToSaveName;

    [Header("Message")]
    [TextArea]
    public string Message;

    public void OnValidate()
    {
        if (TalkerName == "P")
            TalkerName = "P-Chan";
    }
}
