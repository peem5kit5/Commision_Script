using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseUI : MonoBehaviour
{
    public Animator UIAnimator => uiAnimator;
    [SerializeField] private Animator uiAnimator;
    public event Action OnValueChange;
    public event Action<int> OnValueIntChange;
    public event Action<float> OnValueFloatChange;

    private void Start()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    public void BindingUI(Action _action) => OnValueChange += _action;
    public void BindingUI(Action<int> _action) => OnValueIntChange += _action;
    public void BindingUI(Action<float> _action) => OnValueFloatChange += _action;

    //Require Animator Setting
    public void Animating(string _animationName)
    {
        uiAnimator.Play(_animationName, 0, 1);
        uiAnimator.speed = 1;
    }
    public void StopAnimating() => uiAnimator.SetTrigger("Stop");
}
