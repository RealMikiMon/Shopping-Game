using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] Sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        PlaySound("Theme");
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(Sounds, Sound => Sound.name == name);
        if (sound == null)
        {
            return;
        }
        sound.source.Play();
    }
}