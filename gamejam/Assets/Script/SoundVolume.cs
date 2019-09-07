using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public Slider soundvolume;
    static public float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("Volume"); //
        soundvolume.value = volume;

    }

    // Update is called once per frame
    void Update()
    {
        volume = soundvolume.value;
        Debug.Log("volume   " + volume);
        PlayerPrefs.SetFloat("Volume", volume); //
    }
}
