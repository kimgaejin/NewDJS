using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play(string name)
    {
        string path = "Sound/" + name;

        AudioClip clip;
        clip = (AudioClip)Resources.Load(path, typeof(AudioClip)) ;

        if (clip == null) Debug.Log("Clip is null "+ path);

        audio.clip = clip;
        audio.Play();
        
    }
}
