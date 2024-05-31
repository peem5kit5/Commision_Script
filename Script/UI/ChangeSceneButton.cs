using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : ButtonUI
{
    public string SceneName;
    public int SceneIndex;

    public override void Init()
    {
        base.Init();
        BindingButton(ChangeScene);
    }

    public void ChangeScene()
    {
        if(SceneName == "Exit" || SceneName == "Quit")
        {
            Application.Quit();
            return;
        }
            
        if(SceneName == "Init")
        {
            PlayerData _playerData = new PlayerData("Init", 0, 0);
            SaveLoadManager _saveLoadManager = FindObjectOfType<SaveLoadManager>();
            _saveLoadManager.SavePlayerData(_playerData);
        }

        var _sceneController = new SceneController();

        if(SceneName == "")
        {
            if(SceneIndex == 0)
            {
                Debug.LogWarning("No Data Assign : " + gameObject.name);
                return;
            }

            _sceneController.ChangeScene(SceneIndex);
            return;
        }

        _sceneController.ChangeScene(SceneName);
    }
}
