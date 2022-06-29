using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private List<AudioSource> _BGM_list;
    [SerializeField] private List<AudioSource> _sfx_list;
    [SerializeField] private AudioMixer masterMixer;
    public bool isOn;

    private void Start()
    {
        DontDestroyOnLoadInit();
    }

    public void PlaySFX(int i)
    {
        if (i < _sfx_list.Count)
            _sfx_list[i].Play();
        else
            Debug.Log("Index Out Of Range");
    }


    public void PlayBGM(int i)
    {
        if (i < _sfx_list.Count)
        {
            if (!_BGM_list[i].isPlaying)
            {
                _BGM_list[i].Play();
            }
        }
        else
            Debug.Log("Index Out Of Range");
    }

    public void StopBGM(int i)
    {
        if (i < _sfx_list.Count)
            _BGM_list[i].Stop();
        else
            Debug.Log("Index Out Of Range");
    }

   

    public void ToggleSound(bool state)
    {
        switch (state)
        {
            case true:
                SoundOn();
                break;
            default:
                SoundOff();
                break;
        }
    }

    public void SoundOn()
    {
        isOn = true;
        AudioListener.volume = 1;
    }

    public void SoundOff()
    {
        isOn = false;
        AudioListener.volume = 0;
    }

    public void VolumeChange(float value)
    {
        masterMixer.SetFloat("musicVol", value);
    }

    public float GetVolume()
    {
        masterMixer.GetFloat("musicVol", out var volume);
        return volume;
    }
}