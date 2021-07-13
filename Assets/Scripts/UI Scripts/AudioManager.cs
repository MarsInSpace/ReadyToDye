using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;

            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Atmosphere");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.Source.Play();
    }
}
