using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    AudioSource[] sounds;

    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();    
    }

    public void MuteAll()
    {
        foreach (AudioSource source in sounds)
        {
            source.mute = true;
        }
    }

    public void UnmuteAll()
    {
        foreach (AudioSource source in sounds)
        {
            source.mute = false;
        }
    }

    public void PlayClick()
    {
        sounds[1].Play();
    }

    public void PlayGenerator()
    {
        sounds[2].Play();
    }

    public void PlayUpgrade()
    {
        sounds[3].Play();
    }

}
