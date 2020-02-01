
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    private AudioSource BGMAudio;

    private float stopTime;
    private Coroutine stopCoroutine;
    private bool activeStopCoroutine;

    public void Awake()
    {
        audio = GetComponent<AudioSource>();
        BGMAudio = transform.Find("BGM").GetComponent<AudioSource>();
    }

    public void Init()
    {
        audio = GetComponent<AudioSource>();
        BGMAudio = transform.Find("BGM").GetComponent<AudioSource>();
    }

    public void PlayEffect(string name)
    {
        string path = "Sounds/Effects/" + name;
        Debug.Log("sound path: " + path);
        AudioClip clip;
        clip = (AudioClip)Resources.Load(path, typeof(AudioClip));
        if (clip == null) Debug.Log("coudn't find sound path " + path);
        audio.clip = clip;
        audio.PlayOneShot(clip);
    }

    public void PlayBG(string name)
    {
        string path = "Sounds/BG/" + name;
        Debug.Log("sound path: " + path);
        AudioClip clip;
        clip = (AudioClip)Resources.Load(path, typeof(AudioClip));
        if (clip == null) Debug.Log("coudn't find sound path " + path);
        BGMAudio.clip = clip;
        BGMAudio.Play();
    }

    public void SetVolume(float volume_)
    {
        audio.volume = volume_;
        BGMAudio.volume = volume_;
    }

    public void SetBGMVolume(float volume_)
    {
        BGMAudio.volume = volume_;
    }

    public void StopBG(float time)
    {
        stopTime = time;
        if (activeStopCoroutine) StopCoroutine(stopCoroutine);
        stopCoroutine = StartCoroutine(StopBG());
    }

    private IEnumerator StopBG()
    {
        WaitForSeconds waitTime = new WaitForSeconds(stopTime);

        while (true)
        {
            activeStopCoroutine = true;
            SetBGMVolume(0);

            yield return waitTime;

            float volume = PlayerPrefs.GetFloat("Volume");
            SetBGMVolume(volume);

            activeStopCoroutine = false;
            yield break;
        }
    }
}