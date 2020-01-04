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

    private string stageNameToGo;
    private GameObject fade;
    private Animator fadeAnim;

    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        stage1 = PlayerPrefs.GetInt("Stage1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("Stage2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("Stage3"); //게임 시작시 현재까지 깬 스테이지 로
        stage4 = PlayerPrefs.GetInt("Stage4"); //게임 시작시 현재까지 깬 스테이지 로
        stage5 = PlayerPrefs.GetInt("Stage5"); //게임 시작시 현재까지 깬 스테이지 로
        fade = GameObject.Find("Canvas").transform.Find("Fade").gameObject;
        fadeAnim = fade.GetComponent<Animator>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void L()
    {
        Player.Stage = int.Parse(this.tag);
        if (int.Parse(this.tag) < 10)
        {
<<<<<<< Updated upstream
            
            SceneManager.LoadScene("Chapter0");
            
        }
        else if (int.Parse(this.tag) < 20)
        {
            SceneManager.LoadScene("Chapter1");
        }
        else if (int.Parse(this.tag) < 30)
        {
            SceneManager.LoadScene("Chapter2");
        }
        else if (int.Parse(this.tag) < 40)
        {
            SceneManager.LoadScene("Chapter3");
        }
        else if (int.Parse(this.tag) < 50)
        {
            SceneManager.LoadScene("Chapter4");
=======
            stageNameToGo = "Chapter1";
        }
        else if (int.Parse(this.tag) < 20)
        {
            stageNameToGo = "Chapter2";
        }
        else if (int.Parse(this.tag) < 30)
        {
            stageNameToGo = "Chapter3";
        }
        else if (int.Parse(this.tag) < 40)
        {
            stageNameToGo = "Chapter4";
        }
        else if (int.Parse(this.tag) < 50)
        {
            stageNameToGo = "Chapter5";
>>>>>>> Stashed changes
        }

        StartCoroutine("GoToScene");

    }

    private IEnumerator GoToScene()
    {
        WaitForSeconds waitPerSecond = new WaitForSeconds(1.0f);
        while (true)
        {
            // 사운드
            audio.Play("ClickButton");

            fade.SetActive(true);
            fade.transform.position = transform.position;
            fadeAnim.Play("FadeOut");


            yield return waitPerSecond;

            // 씬이동
            SceneManager.LoadScene(stageNameToGo);

            yield break;
        }
    }
}
