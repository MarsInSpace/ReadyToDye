using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public void SetSFXVolume (float volume)
    {
        AudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
}
