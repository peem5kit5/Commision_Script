using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SaveLoadManager))]
public class VisualNovelManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI talkerText;
    [SerializeField] private GameObject panelDialogue;
    [SerializeField] private DialogueData currentDialogue;
    [SerializeField] private ButtonUI nextDialogueButton;
    [SerializeField] private CharacterVisualNovelController characterNovelController;
    [SerializeField] private ChoicesPanel[] choicesPanels;
    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private Image BGimage;

    public DialogueDataHolder CurrentDataHolder;
    public DialogueDataHolder[] DialogueDataHolders;
    public int Score;
    public int SpecialScore;

    public SaveLoadManager SaveLoadManager => saveLoadManager;

    [Header("Status")]
    public string CurrentRoute = "";
    public int LocalDialogueIndex = 0;
    public bool isTyping;
    public int CharacterIndex = 0;
    public bool IsAuto;

    [Header("Setting")]
    public float TypingSpeed;
    public float AutoSpeed = 3;

    private Dictionary<string, DialogueDataHolder> dialogueDataHolderDict = new Dictionary<string, DialogueDataHolder>();
    private Dictionary<string, ChoicesPanel> choicePanelsDict = new Dictionary<string, ChoicesPanel>();

    private void Start()
    {
        if (!saveLoadManager)
            saveLoadManager = FindObjectOfType<SaveLoadManager>();

        PlayerData _playerData = saveLoadManager.LoadPlayerData();
        Score = _playerData.PointData;
        SpecialScore = _playerData.SpecialPointData;

        nextDialogueButton.BindingButton(NextDialogue);
        dialogueDataHolderDict = GetDialogueDataDict();
        choicePanelsDict = GetChoicePanelDataDict();

        CurrentDataHolder = DialogueDataHolders[0];
        currentDialogue = CurrentDataHolder.DialogueDatas[0];
        StartTyping();
    }

    private Dictionary<string, DialogueDataHolder> GetDialogueDataDict()
    {
        Dictionary<string, DialogueDataHolder> _dict = new Dictionary<string, DialogueDataHolder>();
        foreach (DialogueDataHolder _data in DialogueDataHolders)
            _dict.Add(_data.RouteName, _data);

        return _dict;
    }
    private Dictionary<string, ChoicesPanel> GetChoicePanelDataDict()
    {
        Dictionary<string, ChoicesPanel> _dict = new Dictionary<string, ChoicesPanel>();
        foreach (ChoicesPanel _data in choicesPanels)
            _dict.Add(_data.RouteName, _data);

        return _dict;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!panelDialogue)
        {
            var _panelDialogue = GameObject.Find("PanelDialogue");

            if (!_panelDialogue)
                Debug.LogError("No PanelDialogue UI!");

            panelDialogue = _panelDialogue;
        }

        if (!talkerText)
        {
            var _talkerText = GameObject.Find("TalkerText").GetComponent<TextMeshProUGUI>();
            if (!_talkerText)
                Debug.LogError("There no Talker Text in this scene");

            talkerText = _talkerText;
        }

        if (!nextDialogueButton)
        {
            var _nextButton = GameObject.Find("NextButton").GetComponent<ButtonUI>();

            if (!_nextButton)
                Debug.LogError("No NextButton!");

            nextDialogueButton = _nextButton;
        }

        if (!characterNovelController)
        {
            characterNovelController = FindObjectOfType<CharacterVisualNovelController>();

            if (!characterNovelController)
                Debug.LogError("No CharacterController!");

        }

        if(choicesPanels.Length == 0)
        {
            choicesPanels = FindObjectsOfType<ChoicesPanel>();

            if (choicesPanels.Length == 0)
                Debug.LogError("There no choices panels in this scene.");
        }

        if (!saveLoadManager)
        {
            saveLoadManager = GetComponent<SaveLoadManager>();

            if (!saveLoadManager)
                Debug.LogError("There no save Load Manager");
        }

        if (!BGimage)
            BGimage = GameObject.Find("BG").GetComponent<Image>();
    }
#endif
    
    private void StartTyping() => StartCoroutine(Typing());

    private IEnumerator Typing()
    {
        isTyping = true;

        if(currentDialogue.AnimationNames != "")
        characterNovelController.Animating(currentDialogue.AnimationNames);

        //nextDialogueButton.gameObject.SetActive(false);
        talkerText.text = currentDialogue.TalkerName;
        text.text = "";

        while(CharacterIndex < currentDialogue.Message.Length)
        {
            text.text += currentDialogue.Message[CharacterIndex];
            CharacterIndex++;
            yield return new WaitForSeconds(TypingSpeed);
        }

        isTyping = false;
        nextDialogueButton.gameObject.SetActive(true);

        if (IsAuto && !isTyping)
            AutoDialogue();
    }

    public void ChangeRoute(string _route)
    {
        if (dialogueDataHolderDict.TryGetValue(_route, out var _data))
        {
            CurrentDataHolder = _data;
            currentDialogue = _data.DialogueDatas[0];
            LocalDialogueIndex = 0;
            CharacterIndex = 0;

            StartTyping();
        }
        else
            Debug.LogError("There no this route in this Scene : " + _route);
    }

    private void AutoDialogue() => StartCoroutine(timingAuto());

    public void SetSceneToChange(string _name)
    {
        PlayerData _playerData = new PlayerData(_name, Score, SpecialScore);
        saveLoadManager.SavePlayerData(_playerData);

        var _sceneController = new SceneController();
        _sceneController.ChangeScene(_name);
    } 

    private void NextDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            text.text = currentDialogue.Message;
            isTyping = false;
            nextDialogueButton.gameObject.SetActive(true);
            return;
        }

        if (currentDialogue.BGToChange != null)
        {
            BGimage.sprite = currentDialogue.BGToChange;

            if(currentDialogue.BGToSaveName != "")
            {
                var _galleryUnlocked = new GalleryUnlockData(currentDialogue.BGToSaveName);
                saveLoadManager.SaveGallery(_galleryUnlocked);
            }
        }

        if (currentDialogue.AfterDialogueSound != null)
            AudioSystem.Instance.PlaySFXAudioClip(currentDialogue.AfterDialogueSound);

        if (currentDialogue.NextSceneName != "")
        {
            SetSceneToChange(currentDialogue.NextSceneName);
            return;
        }

        var _currentDialogue = CurrentDataHolder.DialogueDatas[LocalDialogueIndex];

        if (LocalDialogueIndex == CurrentDataHolder.DialogueDatas.Length - 1)
        {
            if (!_currentDialogue.HaveChoice)
                nextDialogueButton.gameObject.SetActive(true);
            else
            {
                ToggleDialoguePanel(false);

                if (choicesPanels.Length == 0) 
                {
                    Debug.LogError("Please put choices Panels in this.");
                    return;
                }

                if (choicePanelsDict.TryGetValue(CurrentDataHolder.RouteName, out var _panel))
                    _panel.Toggle(true);
                else
                    Debug.LogError("No choice panels assign in this route : " + CurrentDataHolder.RouteName);

                return;
            }
        } 

        LocalDialogueIndex++;
        currentDialogue = CurrentDataHolder.DialogueDatas[LocalDialogueIndex];
        CharacterIndex = 0;

        StartTyping();
    }

    private IEnumerator timingAuto()
    {
        yield return new WaitForSeconds(AutoSpeed);
        NextDialogue();
    }

    public void ToggleDialoguePanel(bool _isToggle)
    {
        text.enabled = _isToggle;
        talkerText.enabled = _isToggle;
        panelDialogue.SetActive(_isToggle);
        nextDialogueButton.gameObject.SetActive(_isToggle);
    }
}

[System.Serializable]
public class DialogueDataHolder
{
    public string RouteName;
    public DialogueData[] DialogueDatas;
}
