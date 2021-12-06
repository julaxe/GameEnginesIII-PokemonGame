using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class SoundClass
{
    public string name;

    public AudioClip audioClip;

    [Range(0.0f, 1.0f)]
    public float volume;

    [Range(0.0f, 3.0f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
