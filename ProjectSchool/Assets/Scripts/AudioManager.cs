using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Slider SoundSlider;
    [Header("SFX Audio")]
    public Sound[] Clips;

    [HideInInspector]public float SoundVolume;
    [HideInInspector]public GameObject ButtonGameObject;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != ButtonGameObject)
        {
            PlaySounds("Hit");
        }
    }
    public void Start()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        foreach (Sound s in Clips)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = SoundVolume;
            s.source.loop = s.Loop;
        }
        if (SoundSlider != null)
        {
            SoundSlider.value = SoundVolume;
        }
    }
    public void ChangeVolumeSounds(float amount)
    {
        SoundVolume = amount;
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        if (GameObject.Find("AudioManager").GetComponent<AudioSource>() == false)
        {
            return;
        }
        else
        {
            GameObject.Find("AudioManager").GetComponent<AudioSource>().volume = SoundVolume;
        }
    }


    public void PlaySounds(string name)
    {
        Sound s = Array.Find(Clips, sound => sound.Name == name);
        s.source.Play();
    }
}
