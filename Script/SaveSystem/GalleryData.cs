using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryData : MonoBehaviour
{
    public string GalleryName;
    public Image GalleryImage;

    private void OnValidate()
    {
        if (!GalleryImage)
            GalleryImage.GetComponent<Image>();
    }

    public void ToggleImage(bool _isToggle) => GalleryImage.enabled = _isToggle;
}
