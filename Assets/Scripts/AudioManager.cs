using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public Sound[] sounds;

    Sound currentBackgroundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if (currentBackgroundMusic != null)
        {
            StopBackgroundMusic();
        }
        Play("IntroMusic");
        
    }

    public void ChangeBackgroundVolume(float volume)
    {
        if (currentBackgroundMusic != null) currentBackgroundMusic.source.volume = volume;
    }

    public void StopBackgroundMusic()
    {
        currentBackgroundMusic.source.Stop();
    }



    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;

        if (s.loop) currentBackgroundMusic = s;

        s.source.Play();
    }
}
