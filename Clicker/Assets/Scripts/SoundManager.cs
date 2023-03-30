using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    AudioSource[] sounds;
    public GameObject image;
    public int muted = 0;

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
        muted = PlayerPrefs.GetInt("muted", 0);
        sounds = GetComponents<AudioSource>();
        if (muted == 1)
            MuteAll();
    }

    public void MuteAll()
    {
        foreach (AudioSource source in sounds)
        {
            source.mute = true;
        }
        muted = 1;
        image.SetActive(true);
    }

    public void UnmuteAll()
    {
        foreach (AudioSource source in sounds)
        {
            source.mute = false;
        }
        muted = 0;
        image.SetActive(false);
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
