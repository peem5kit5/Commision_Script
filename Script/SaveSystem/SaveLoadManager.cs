using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private string PlayerSavePath;
    private string SavePath;

    private void Awake()
    {
        PlayerSavePath = Application.persistentDataPath + "/PlayerData.json";
        SavePath = Application.persistentDataPath;
    }

    public void SaveGallery(GalleryUnlockData _gallery)
    {
        Debug.Log(_gallery.Name);
        string _json = JsonUtility.ToJson(_gallery);
        Debug.Log(SavePath + "/" + _gallery.Name + ".json");
        File.WriteAllText(SavePath + "/" + _gallery.Name + ".json", _json);
    }

    public string LoadGallery(GalleryData _galleryData)
    {
        Debug.Log(SavePath + "/" + _galleryData.GalleryName + ".json");
        if (File.Exists(SavePath + "/" + _galleryData.GalleryName + ".json"))
        {
            string _json = File.ReadAllText(SavePath + "/" + _galleryData.GalleryName + ".json");
            GalleryUnlockData _unlocked = JsonUtility.FromJson<GalleryUnlockData>(_json);
            return _unlocked.Name;
        }
        else
        {
            Debug.Log("Not have this Gallery ! : " + _galleryData.GalleryName);
            return "";
        }
        
    }

    public void SavePlayerData(PlayerData _playerData)
    {
        string _json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(PlayerSavePath, _json);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(PlayerSavePath))
        {
            string _json = File.ReadAllText(PlayerSavePath);
            return JsonUtility.FromJson<PlayerData>(_json);
        }

        PlayerData _playerData = new PlayerData("Tutorial", 0, 0);
        return _playerData;
    }
}

public class PlayerData
{
    public string SceneData;
    public int PointData;
    public int SpecialPointData;

    public PlayerData(string _sceneData, int _pointData, int _specialPointData)
    {
        SceneData = _sceneData;
        PointData = _pointData;
        SpecialPointData = _specialPointData;
    }
}

public class GalleryUnlockData 
{
    public string Name;

    public GalleryUnlockData(string _name)
    {
        Name = _name;
    }
}
