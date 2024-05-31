using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoicesPanel : BaseUI
{
    [Header("Route")]
    public string RouteName;
    public ChoicesButton[] ChoiceButtonUIs;

    public GameObject BuyPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void OnValidate()
    {
        ChoiceButtonUIs = GetComponentsInChildren<ChoicesButton>();
    }

    public override void Init()
    {
        base.Init();

        foreach (ChoicesButton _choice in ChoiceButtonUIs)
        {
            _choice.BindingButton(Deactivated);
            Toggle(false);
        }
    }

    private void Deactivated() => Toggle(false);

    public void Toggle(bool _isToggle)
    {
        foreach (ChoicesButton _choice in ChoiceButtonUIs)
        {
            _choice.gameObject.SetActive(_isToggle);

            if (_choice.Type == ChoicesButton.ChoiceType.BuyButton)
                BuyPanel.SetActive(_isToggle);
        } 
    }
}
