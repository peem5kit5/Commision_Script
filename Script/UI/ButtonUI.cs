using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonUI : BaseUI
{
    public Button Button;

    public virtual void OnValidate()
    {
        Button = gameObject.GetComponent<Button>();
        if (!Button) Debug.LogError("This is not Button!");
    }

    public void BindingButton(UnityEngine.Events.UnityAction _action) 
    {
        Button.onClick.AddListener(_action);
        Button.onClick.AddListener(Sound);
        
    } 

    private void Sound()
    {
        if (AudioSystem.Instance != null)
            AudioSystem.Instance.PlayUiSound();
    }
}
