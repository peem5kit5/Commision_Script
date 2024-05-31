using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSystem : MonoBehaviour
{
    private static AudioSystem instance;

    public static AudioSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioSystem>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioSystem");
                    instance = obj.AddComponent<AudioSystem>();
                }
            }
            return instance;
        }

        private set { instance = value; }
    }

    public AudioMixer AudioMixer => audioMixer;
    public AudioSource BGMSource => bgmSource;
    public AudioSource SFXSource => sfxSource;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip bgm;

    [Header("Special Sound")]
    [SerializeField] private AudioClip trashGrab;
    [SerializeField] private AudioClip ui_sound;
    [SerializeField] private AudioClip pickingSound;

    public float CurrentVolume_BGM = 0.5f;
    public float CurrentVolume_SFX = 0.5f;

    public void SetBgmVolume(float _volume)
    {
        CurrentVolume_BGM = _volume;
        audioMixer.SetFloat("BGM", Mathf.Log10(CurrentVolume_BGM) * 20);
    }
    public void SetSfxVolume(float _volume) 
    {
        CurrentVolume_SFX = _volume;
        audioMixer.SetFloat("SFX", Mathf.Log10(CurrentVolume_SFX) * 20);
    }

    private void Awake()
    {
        SetLoaded();
    }

    private void Start()
    {
        if (!bgm)
        {
            Debug.LogError("No BGMs");
            return;
        }

        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    private void SetLoaded()
    {
        if (PlayerPrefs.HasKey("BGM_Voulume"))
            SetBgmVolume(PlayerPrefs.GetFloat("BGM_Volume"));

        if (PlayerPrefs.HasKey("SFX_Volume"))
            SetSfxVolume(PlayerPrefs.GetFloat("SFX_Volume"));

        if (PlayerPrefs.HasKey("Mute"))
        {
            int _value = PlayerPrefs.GetInt("Mute");

            if(_value == 1)
            {
                sfxSource.mute = true;
                bgmSource.mute = true;
            }
            else
            {
                sfxSource.mute = false;
                bgmSource.mute = false;
            }
        }
    }

    public void ChangeBGMAudioClip(AudioClip _audio)
    {
        bgmSource.Pause();
        bgmSource.clip = _audio;
        bgmSource.Play();
    }

    public void PlayTrashGrabSound()
    {
        sfxSource.clip = trashGrab;
        sfxSource.Play();
    }

    public void PlayPickingSound()
    {
        sfxSource.clip = pickingSound;
        sfxSource.Play();
    }

    public void PlayUiSound()
    {
        sfxSource.clip = ui_sound;
        sfxSource.Play();
    }

    public void PlaySFXAudioClip(AudioClip _audio) 
    {
        sfxSource.clip = _audio;
        sfxSource.Play();
    } 
}
