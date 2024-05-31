using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButtonUI : ButtonUI
{
    public SaveLoadState Type;
    public enum SaveLoadState
    {
        Load,
        Save
    }

    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private VisualNovelManager visualNovelManager;

    public override void OnValidate()
    {
        base.OnValidate();

        if (!saveLoadManager)
            saveLoadManager = FindObjectOfType<SaveLoadManager>();

        if (!visualNovelManager)
            visualNovelManager = FindObjectOfType<VisualNovelManager>();
    }

    public override void Init()
    {
        base.Init();
        BindingButton(CheckState);
    }

    private void CheckState()
    {
        if(Type == SaveLoadState.Load)
        {
            PlayerData _playerData = saveLoadManager.LoadPlayerData();
            SceneController _sceneController = new SceneController();
            _sceneController.ChangeScene(_playerData.SceneData);
        }
        else
        {
            PlayerData _playerData = new PlayerData(SceneManager.GetActiveScene().name, visualNovelManager.Score, visualNovelManager.SpecialScore);
            saveLoadManager.SavePlayerData(_playerData);
        }
    }
}
