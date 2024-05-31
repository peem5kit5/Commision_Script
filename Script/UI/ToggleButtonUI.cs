using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButtonUI : ButtonUI
{
    public bool IsTrue;
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite spriteTrue;
    [SerializeField] private Sprite spriteFalse;

    public override void Init()
    {
        base.Init();
        BindingButton(Toggle);
    }

    public void Toggle()
    {
        IsTrue = !IsTrue;
        if (IsTrue)
            targetImage.sprite = spriteTrue;
        else
            targetImage.sprite = spriteFalse;
    }
}
