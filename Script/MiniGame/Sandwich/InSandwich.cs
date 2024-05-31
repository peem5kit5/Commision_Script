using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InSandwich : MonoBehaviour
{
    public int ID;

    [SerializeField] private Image image;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!image)
            image = GetComponent<Image>();
    }
#endif
    public void Wopper() => image.enabled = true;
}
