using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelScreen : MonoBehaviour
{
    public int stage1;
    public int stage2;
    public int stage3;
    public int stage4;
    public int stage5;

    private TitleSceneAudioManager titleAudioManager;

    private bool isClicked;
    private string sceneNameToGo;
    // Start is called before the first frame update
    void Start()
    {
        stage1 = PlayerPrefs.GetInt("Stage1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("Stage2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("Stage3"); //게임 시작시 현재까지 깬 스테이지 로
        stage4 = PlayerPrefs.GetInt("Stage4"); //게임 시작시 현재까지 깬 스테이지 로
        stage5 = PlayerPrefs.GetInt("Stage5"); //게임 시작시 현재까지 깬 스테이지 로

        titleAudioManager = GameObject.Find("AudioManager").GetComponent<TitleSceneAudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void L()
    {
        if (isClicked == false)
        {
            isClicked = true;
            Player.Stage = int.Parse(this.tag);
            if (int.Parse(this.tag) < 10)
            {
                sceneNameToGo = "Chapter1";
            }
            else if (int.Parse(this.tag) < 20)
            {
                sceneNameToGo = "Chapter2";
            }
            else if (int.Parse(this.tag) < 30)
            {
                sceneNameToGo = "Chapter3";
            }
            else if (int.Parse(this.tag) < 40)
            {
                sceneNameToGo = "Chapter4";
            }
            else if (int.Parse(this.tag) < 50)
            {
                sceneNameToGo = "Chapter5";
            }

            StartCoroutine("GoToScene");
            if (titleAudioManager)
                titleAudioManager.PlayEffect("Ding Sound Effect");
        }

    }

    private IEnumerator GoToScene()
    {
        WaitForSeconds waitPerSecond = new WaitForSeconds(1.0f);
        while (true)
        {

            FadeOut();

            yield return waitPerSecond;

            SceneManager.LoadScene(sceneNameToGo);
        }
    }

    private void FadeOut()
    {
        Animator fadeAnim = null;
        GameObject canvas = GameObject.Find("Canvas");
        Transform tfFade = canvas.transform.Find("Fade");
        if (tfFade)
        {
            tfFade.gameObject.SetActive(true);
            fadeAnim = tfFade.GetComponent<Animator>();
            tfFade.position = transform.position;
        }
        if (fadeAnim) fadeAnim.Play("FadeOut");
    }
}
