using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;

    public void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(string name)
    {
        string path = "Sounds/" + name;
        Debug.Log("sound path: " + path);
        AudioClip clip;
        clip = (AudioClip)Resources.Load(path, typeof(AudioClip));
        if (clip == null) Debug.Log("coudn't find sound path " + path);
        audio.clip = clip;
        audio.Play();
    }
}
