using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitManager : MonoBehaviour
{
    //======================
    // 대부분의 스크립트들은 Link-Init-Close 순서로 진행.
    // Link 에서는 다른 GameObject/Script를 참조
    // Init 에서는 참조한 바를 바탕으로 초기값 설정
    // Close 에서는 SetActvie를 False로 만들어야하는 것들을 실행함
    //======================
    private GameObject gmPlayer;

    private GameObject gmScriptCanvas;
    private GameObject gmScriptPanel;
    private ScriptCanvasManager scriptCanvasManager;

    private GameObject gmPlayerControlCanvas;
    private GameObject gmStopPanel;
    private GameObject gmStopPanelSlider;
    private SoundVolume soundVolume;

    private GameObject gmSceneDirectorCanvas;

    private GameObject gmEffectManager;

    private GameObject gmAudioManager;
    private AudioManager audioManager;

    private void Awake()
    {
        GetComponents();
        InitComponents();
        LinkComponents();
        CloseComponents();

    }

    private void GetComponents()
    {
        gmPlayer = GameObject.Find("Player");

        gmScriptCanvas = GameObject.Find("ScriptCanvas");
        gmScriptPanel = gmScriptCanvas.transform.Find("ScriptPanel").gameObject;
        scriptCanvasManager = gmScriptCanvas.GetComponent<ScriptCanvasManager>();

        gmPlayerControlCanvas = GameObject.Find("PlayerControlCanvas");
        gmStopPanel = gmPlayerControlCanvas.transform.Find("StopPanel").gameObject;
        gmStopPanelSlider = gmStopPanel.transform.Find("Slider").gameObject;
        soundVolume = gmStopPanelSlider.GetComponent<SoundVolume>();

        gmSceneDirectorCanvas = GameObject.Find("SceneDirectorCanvas");

        gmEffectManager = GameObject.Find("EffectManager");

        gmAudioManager = GameObject.Find("AudioManager");
        audioManager = gmAudioManager.GetComponent<AudioManager>();


    }

    private void InitComponents()
    {
        scriptCanvasManager.Init();
        audioManager.Init();
        soundVolume.Init();
    }


    private void LinkComponents()
    {
        scriptCanvasManager.Link(gmScriptPanel);
        soundVolume.Link();
    }


    private void CloseComponents()
    {
        scriptCanvasManager.Close();
    }

}
