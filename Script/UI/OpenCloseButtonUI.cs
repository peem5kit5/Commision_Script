using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseButtonUI : ButtonUI
{
    [SerializeField] private GameObject TargetToSetActive;
    public enum Type
    {
        Open,
        Close
    }

    public Type ButtonType;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!TargetToSetActive)
            Debug.LogError("Please Set Target to Setactive in this Button : " + gameObject.transform.parent.name);
    }

    public override void Init()
    {
        base.Init();
        BindingButton(OpenButton);
    }

    private void OpenButton() 
    {
        if (ButtonType == Type.Open)
            TargetToSetActive.SetActive(true);
        else
            TargetToSetActive.SetActive(false);
    } 
}
