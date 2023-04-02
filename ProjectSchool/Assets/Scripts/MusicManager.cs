using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider MusicSlider;
    public Sound[] Music;
    [HideInInspector]
    public float MusicVolume;

    void Start()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        foreach (Sound f in Music)
        {
            f.source = gameObject.AddComponent<AudioSource>();
            f.source.clip = f.clip;
            f.source.volume = MusicVolume;
            f.source.loop = f.Loop;
        }
        if (MusicSlider != null)
        {
            MusicSlider.value = MusicVolume;
        }
        PlayMusic("Theme");
    }

    public void VolumeChangerMusic(float amount)
    {
        MusicVolume = amount;
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = MusicVolume;
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(Music, sound => sound.Name == name);
        s.source.Play();
    }
}
