using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    // Outer References
    private TitleSceneAudioManager audioManager;

    // Inner References
    private GameObject goLevelPanel;
    private GameObject goButtonPanel;
    private GameObject goOptionPanel;


    // Start is called before the first frame update
    void Start()
    {
        // Panel GameObject 초기화
        GameObject mainCanvas = GameObject.Find("Canvas");
        goLevelPanel = mainCanvas.transform.Find("LevelPanel").gameObject;
        goButtonPanel = mainCanvas.transform.Find("MainButtonPanel").gameObject;
        goOptionPanel = mainCanvas.transform.Find("OptionPanel").gameObject;

        // 오디오 초기화
        float volume;
        audioManager = GameObject.Find("AudioManager").GetComponent<TitleSceneAudioManager>();
        volume = PlayerPrefs.GetFloat("Volume"); //
        PlayerPrefs.SetFloat("Volume", volume); //
        audioManager.SetVolume(volume);
    }

    public void Game_Start() {
        goButtonPanel.SetActive(false);
        goLevelPanel.SetActive(true);
        audioManager.PlayEffect("ClickButton");
    }

    public void Game_End() {
        audioManager.PlayEffect("ClickButton");


    }

    public void Back() {
        goButtonPanel.SetActive(true);
        goLevelPanel.SetActive(false);
        audioManager.PlayEffect("ClickButton");
    }

    public void OnOptionPanel()
    {
        goOptionPanel.SetActive(true);
        goButtonPanel.SetActive(false);
        goLevelPanel.SetActive(false);
        audioManager.PlayEffect("ClickButton");
    }

    public void OffOptionPanel()
    {
        goOptionPanel.SetActive(false);
        goButtonPanel.SetActive(true);
        audioManager.PlayEffect("ClickButton");
    }

    public void Reset()
    {

        Debug.Log("Reset");

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

    public void Allclear()
    {
        Debug.Log("All Clear");
        PlayerPrefs.SetInt("Stage1", 5); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage2", 15); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage3", 25); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage4", 35); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("Stage5", 45); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB1", 5); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB2", 15); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB3", 25); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB4", 35); // 플레그 접촉시 스테이지 저장
        PlayerPrefs.SetInt("StageB5", 45); // 플레그 접촉시 스테이지 저장
        audioManager.PlayEffect("ClickButton");

    }
}
