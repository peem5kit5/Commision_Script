using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveLoadManager))]
public class GalleryList : MonoBehaviour
{
    public GalleryData[] UnlockedGalleryDatas;
    public GalleryData[] AllGalleryData;

    [SerializeField] private SaveLoadManager saveLoadManager;
    private Dictionary<string, GalleryData> galleryDictionary = new Dictionary<string, GalleryData>();

    private void Start()
    {
        foreach(GalleryData _data in AllGalleryData)
        {
            galleryDictionary.Add(_data.GalleryName, _data);
            galleryDictionary[_data.GalleryName].ToggleImage(false);
        }

        if (!saveLoadManager)
            saveLoadManager = FindObjectOfType<SaveLoadManager>();

        List<string> _list = new List<string>();

        for(int i = 0; i < AllGalleryData.Length; i++)
        {
            string _string = saveLoadManager.LoadGallery(AllGalleryData[i]);

            Debug.Log(AllGalleryData[i].GalleryName);

            if (_string == "") continue;

            _list.Add(_string);
        }

        if(_list.Count > 0)
            IsLoad(_list);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!saveLoadManager)
        {
            saveLoadManager = FindObjectOfType<SaveLoadManager>();

            if (!saveLoadManager)
                Debug.LogError("No SaveLoadManager in this scene.");
        }
    }
#endif

    private void IsLoad(List<string> _list)
    {
        foreach(string _name in _list)
        {
            if (galleryDictionary.TryGetValue(_name, out var _value))
                _value.ToggleImage(true);
        }
    }
}
