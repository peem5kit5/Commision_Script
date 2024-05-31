using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderUI : BaseUI
{
    public Slider Slider => slider;
    [SerializeField] private Slider slider;

    public float MinValue;
    public float MaxValue;

    public void OnValidate()
    {
        if (!slider)
        {
            slider = GetComponent<Slider>();

            if (!slider)
                Debug.LogError("This is not Slider!");
        }
    }

    public override void Init()
    {
        slider.maxValue = MaxValue;
        slider.minValue = MinValue;
    }

    public void BindingSlider(UnityEngine.Events.UnityAction<float> _action) => slider.onValueChanged.AddListener(_action);
}
