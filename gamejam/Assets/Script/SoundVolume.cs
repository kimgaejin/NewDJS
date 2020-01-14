using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public Slider soundvolume;
    static public float volume;
    private AudioManager audioManager;


    // Start is called before the first frame update
    public void Init()
    {
        volume = PlayerPrefs.GetFloat("Volume"); //
        soundvolume.value = volume;
    }

    public void Link()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.SetVolume(volume);
    }

    // Update is called once per frame
    void Update()
    {
        volume = soundvolume.value;
        Debug.Log("volume   " + volume);
        PlayerPrefs.SetFloat("Volume", volume); //
        audioManager.SetVolume(volume);
    }
}
