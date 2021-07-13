using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range (-80f, 20f)]
    public float Volume;

    public bool Loop;

    [HideInInspector]
    public AudioSource Source;
}
