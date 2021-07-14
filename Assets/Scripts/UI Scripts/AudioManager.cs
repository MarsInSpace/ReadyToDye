using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public AudioMixerGroup SFX;
    public AudioMixerGroup Music;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            //s.Source.name = s.name;

            if (s.name == "Atmosphere")
                s.Source.outputAudioMixerGroup = Music;

            else
                s.Source.outputAudioMixerGroup = SFX;


            s.Source.volume = s.Volume;
            s.Source.playOnAwake = false;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        Play("Atmosphere");
    }

    public void Play(string sName)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == sName);

        if (s == null)
            return;

        s.Source.Play();
    }

    public void Stop(string sName)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == sName);
        s.Source.Stop();
    }
}
