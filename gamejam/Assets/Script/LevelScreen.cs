using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelScreen : MonoBehaviour
{
    public GameObject Level;
    public int stage1;
    public int stage2;
    public int stage3;
    // Start is called before the first frame update
    void Start()
    {
        stage1 = PlayerPrefs.GetInt("Stage1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("Stage2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("Stage3"); //게임 시작시 현재까지 깬 스테이지 로드

    
            if (stage1 ==0)
            {
                stage1 = 11;
            }
            if (stage2 ==0)
            {
                stage2 = 21;
            }
            if (stage3 == 0)
            {
               stage3 = 31;
             }


        Debug.Log("현재 스테이지" + stage1 + "   " + stage2 + "   " + stage3);
        if (int.Parse(this.tag) < 20)
        {
            if (int.Parse(this.tag) <= stage1)
            {
                Level.SetActive(true);

            }
            else
            {
                Level.SetActive(false);
            }
        }
        else if (int.Parse(this.tag) < 30) {
            if (int.Parse(this.tag) <= stage2)
            {
                Level.SetActive(true);

            }
            else
            {
                Level.SetActive(false);
            }
        }
        else if (int.Parse(this.tag) < 40) {
            if (int.Parse(this.tag) <= stage3)
            {
                Level.SetActive(true);

            }
            else
            {
                Level.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void L()
    {
        Player.Stage = int.Parse(this.tag);
        SceneManager.LoadScene("Chapter2");
    }
}
