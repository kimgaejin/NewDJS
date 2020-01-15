using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    // Outer References
    private AudioManager audioManager;

    // Inner References
    public GameObject Level_Panel;
    public GameObject Panel;



    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Game_Start() {
        Panel.SetActive(false);
        Level_Panel.SetActive(true);
        audioManager.PlayEffect("ClickButton");
    }

    public void Game_End() {
        audioManager.PlayEffect("ClickButton");


    }

    public void Back() {
        Panel.SetActive(true);
        Level_Panel.SetActive(false);
        audioManager.PlayEffect("ClickButton");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Stage1", 1); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage2", 11); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage3", 21); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage4", 31); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage5", 41); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB1", 1); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB2", 11); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB3", 21); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB4", 31); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB5", 41); // 플레그 접촉시 스테이지 저장
        audioManager.PlayEffect("ClickButton");

    }
}
