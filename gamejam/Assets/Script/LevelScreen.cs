using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelScreen : MonoBehaviour
{
    public int stage1;
    public int stage2;
    public int stage3;
    // Start is called before the first frame update
    void Start()
    {
        stage1 = PlayerPrefs.GetInt("Stage1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("Stage2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("Stage3"); //게임 시작시 현재까지 깬 스테이지 로

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
            
            SceneManager.LoadScene("Chapter2");
            
        }
        else if (int.Parse(this.tag) < 20)
        {
            SceneManager.LoadScene("Chapter3");
        }
        else if (int.Parse(this.tag) < 30)
        {
            SceneManager.LoadScene("Chapter0");
        }
 
    }
}
