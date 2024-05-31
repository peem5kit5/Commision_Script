using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] private SliderUI bgmSliderUI;
    [SerializeField] private SliderUI sfxSliderUI;
    [SerializeField] private ToggleButtonUI muteButton;

    [SerializeField] private AudioSystem audioSystem;

    private void OnValidate()
    {
        if (!bgmSliderUI)
            bgmSliderUI = GameObject.Find("BGM_Slider").GetComponent<SliderUI>();

        if (!sfxSliderUI)
            sfxSliderUI = GameObject.Find("SFX_Slider").GetComponent<SliderUI>();

        if (!audioSystem)
            audioSystem = FindObjectOfType<AudioSystem>();

        if (!muteButton)
            muteButton = GameObject.Find("Mute_Button").GetComponent<ToggleButtonUI>();
    }

    private void Start()
    {
        bgmSliderUI.BindingSlider(ChangeBGMValue);
        sfxSliderUI.BindingSlider(ChangeSfxValue);
        muteButton.BindingButton(Mute);

        bgmSliderUI.Slider.value = audioSystem.CurrentVolume_BGM;
        sfxSliderUI.Slider.value = audioSystem.CurrentVolume_SFX;

        bgmSliderUI.MaxValue = 1;
        sfxSliderUI.MaxValue = 1;

        if (PlayerPrefs.HasKey("Mute"))
        {
            int _value = PlayerPrefs.GetInt("Mute");

            if(_value == 1)
                muteButton.Toggle();
            
        }
    }

    private void ChangeBGMValue(float _value)
    {
        audioSystem.SetBgmVolume(_value);
        PlayerPrefs.SetFloat("BGM_Volume", _value);
    }
    private void ChangeSfxValue(float _value) 
    {
        audioSystem.SetSfxVolume(_value);
        PlayerPrefs.SetFloat("SFX_Volume", _value);
    } 

    private void Mute()
    {
        audioSystem.BGMSource.mute = !audioSystem.BGMSource.mute;
        audioSystem.SFXSource.mute = !audioSystem.SFXSource.mute;

        if (audioSystem.SFXSource.mute && audioSystem.BGMSource.mute)
            PlayerPrefs.SetInt("Mute", 1);
        else
            PlayerPrefs.SetInt("Mute", 0);
    }
}
