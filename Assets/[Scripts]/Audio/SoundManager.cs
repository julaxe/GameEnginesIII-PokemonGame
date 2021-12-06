using System;
using UnityEngine.Audio;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    
    public static SoundManager soundManagerInstace;

    public SoundClass[] sounds;


    void Awake()
    {
        if(soundManagerInstace == null)
        {
            soundManagerInstace = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(SoundClass s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
           s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound " + name + " no Found");
            return;
        }
    }

    public void StopSound(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound " + name + " no Found");
            return;
        }
    }

    public bool IsPlaying(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            return s.source.isPlaying;
                
        }
        else
        {
            Debug.LogWarning("Sound " + name + " no Found");
            return false;
        }
    }
}
