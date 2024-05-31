using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualNovelController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnValidate()
    {
        if (!anim)
            anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!anim)
            anim = GetComponent<Animator>();
    }

    public void Animating(string _name) 
    {
        Debug.Log("Play : + "  + _name);
        anim.Play(_name);
    } 
}
