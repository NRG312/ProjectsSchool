using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string Name;
    /*[Range(0f,1f)]
    public float Volume;*/

    public bool Loop;

    [HideInInspector]public AudioSource source;
}
