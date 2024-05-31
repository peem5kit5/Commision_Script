using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ChoicesButton : ButtonUI
{
    [SerializeField] private Color white;
    [SerializeField] private Color pink;
    [SerializeField] private TextMeshProUGUI text;

    public enum ChoiceType
    {
        Normal,
        BuyButton
    }

    [Header("BuyButton")]
    [SerializeField] private GiftRecievedUI giftRecievedUI;
    public ChoiceType Type;
    public int Cost;

    [Header("Route")]
    [SerializeField] private ChoicesPanel choicePanels;
    [SerializeField] private VisualNovelManager visualNovelManager;
    public string RouteToChangeName;
    public string SceneToChangeName = "";
    public int Points;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!visualNovelManager)
            visualNovelManager = FindObjectOfType<VisualNovelManager>();

        if (!choicePanels)
            choicePanels = transform.parent.GetComponent<ChoicesPanel>();

        if (!giftRecievedUI)
            giftRecievedUI = FindObjectOfType<GiftRecievedUI>();

        if (!text)
            text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void BuyButton()
    {
        PlayerData _playerData = visualNovelManager.SaveLoadManager.LoadPlayerData();
        if (_playerData.SpecialPointData >= Cost)
        {
            _playerData.SpecialPointData -= Cost;
            giftRecievedUI.BuyGift(Cost, this);
        }
        else
        {
            Button.interactable = false;
            choicePanels.Toggle(true);
        }
    }

    public override void Init()
    {
        base.Init();
        if (Type == ChoiceType.Normal)
            BindingButton(ChangeRoute);
        else
            BindingButton(BuyButton);
    }

    public void ChangeTextWhiteColor()
    {
        text.color = white;
    }

    public void ChangeTextPinkColor()
    {
        text.color = pink;
    }

    public void ChangeRoute() 
    {
        Debug.Log("ChangeRoute");
        visualNovelManager.Score += Points;

        if (SceneToChangeName == "")
            visualNovelManager.ChangeRoute(RouteToChangeName);
        else 
        {
            PlayerData _playerData = new PlayerData(SceneToChangeName, visualNovelManager.Score, visualNovelManager.SpecialScore);
            visualNovelManager.SaveLoadManager.SavePlayerData(_playerData);

            var _sceneController = new SceneController();
            _sceneController.ChangeScene(SceneToChangeName);
        }

        visualNovelManager.ToggleDialoguePanel(true);
    } 
}
