using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class MiniGameBase : MonoBehaviour
{
    public SaveLoadManager SaveLoadManager => saveLoadManager;
    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private BaseUI completedUI;

    public int Score;
    public string NextScene = "Menu";
    public int PlayerDialoguePoint => playerDialoguePoint;
    private int playerDialoguePoint;
    public Action<int> OnValueChanged;

    public virtual void OnValidate()
    {
        if (!saveLoadManager)
        {
            saveLoadManager = GetComponent<SaveLoadManager>();

            if (!saveLoadManager)
                Debug.LogError("There no Save Load Manager");
        }

        if (!completedUI)
            completedUI = GameObject.Find("CompleteUI").GetComponent<BaseUI>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        if (!saveLoadManager)
            saveLoadManager = GetComponent<SaveLoadManager>();

        PlayerData _loadPlayerData = saveLoadManager.LoadPlayerData();

        Score = _loadPlayerData.SpecialPointData;
        
        playerDialoguePoint = _loadPlayerData.PointData;

        OnValueChanged += ChangedScore;
    }

    private void ChangedScore(int _score) => Score += _score;

    public void SaveScoreMinigame()
    {
        PlayerData _playerData = new PlayerData(NextScene, playerDialoguePoint , Score);
        saveLoadManager.SavePlayerData(_playerData);
        completedUI.Animating("Complete");

        StartCoroutine(Timing());
    }

    private IEnumerator Timing()
    {
        yield return new WaitForSeconds(5);

        var _sceneController = new SceneController();
        _sceneController.ChangeScene(NextScene);
    }

}
