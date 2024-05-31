using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveLoadManager))]
public class GalleryCutscene : MonoBehaviour
{
    [SerializeField] private SaveLoadManager saveLoadManager;

    public float Duration = 7f;
    public string NextScene;
    public string ID;
    public bool CheckEnding;
    bool isPressing;
    private void OnValidate()
    {
        if (!saveLoadManager)
            saveLoadManager = GetComponent<SaveLoadManager>();
    }

    private void Start()
    {
        var _galleryUnlocked = new GalleryUnlockData(ID);
        saveLoadManager.SaveGallery(_galleryUnlocked);

        StartCoroutine(Timing());
    }

    private IEnumerator Timing()
    {
        yield return new WaitForSeconds(Duration);

        //if (CheckEnding)
        //{
        //    PlayerData _playerData = saveLoadManager.LoadPlayerData();
        //}

        var _sceneController = new SceneController();
        _sceneController.ChangeScene(NextScene);
    }
}
