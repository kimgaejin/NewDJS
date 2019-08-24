using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIM : MonoBehaviour
{
    public GameObject[] level1 = new GameObject[5];
    public GameObject[] level2 = new GameObject[5];
    public GameObject[] level3 = new GameObject[5];
    public int stage1;
    public int stage2;
    public int stage3;
    // Start is called before the first frame update
    void Start()
    {    
        stage1 = PlayerPrefs.GetInt("StageB1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("StageB2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("StageB3"); //게임 시작시 현재까지 깬 스테이지 로드

        if (stage1 == 0)
        {
            stage1 = 1;
            PlayerPrefs.SetInt("Stage1", 1); // 플레그 접촉시 스테이지 저장
            PlayerPrefs.SetInt("StageB1", 1); // 플레그 접촉시 스테이지 저장

        }
        if (stage2 == 0)
        {
            stage2 = 11;

            PlayerPrefs.SetInt("Stage2", 11); // 플레그 접촉시 스테이지 저장
            PlayerPrefs.SetInt("StageB2", 11); // 플레그 접촉시 스테이지 저장

        }
        if (stage3 == 0)
        {
            stage3 = 21;

            PlayerPrefs.SetInt("Stage3", 21); // 플레그 접촉시 스테이지 저장
            PlayerPrefs.SetInt("StageB3", 21); // 플레그 접촉시 스테이지 저장
        }

        Debug.Log("현재 스테이지" + stage1 + "   " + stage2 + "   " + stage3);
        for (int i = 0; i < 5; i++) {
            if (stage1 - int.Parse(level1[i].tag) >= 0)
            {
                level1[i].SetActive(true);
            }
            else {
                level1[i].gameObject.transform.position = new Vector2(111111, 111111);
                //level1[i].SetActive(false);
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            if (stage2 - int.Parse(level2[i].tag) >= 0)
            {
        
                level2[i].SetActive(true);
            }
            else
            {
                level2[i].SetActive(false);
            }
        }
        
        for (int i = 0; i < 5; i++)
        {
            if (stage3 - int.Parse(level3[i].tag) >= 0)
            {
                level3[i].SetActive(true);
            }
            else
            {
                level3[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        stage1 = PlayerPrefs.GetInt("StageB1"); //게임 시작시 현재까지 깬 스테이지 로드
        stage2 = PlayerPrefs.GetInt("StageB2"); //게임 시작시 현재까지 깬 스테이지 로드
        stage3 = PlayerPrefs.GetInt("StageB3"); //게임 시작시 현재까지 깬 스테이지 로드
        for (int i = 0; i < 5; i++)
        {
            if (stage1 - int.Parse(level1[i].tag) >= 0)
            {
                level1[i].SetActive(true);
            }
            else
            {
                level1[i].gameObject.transform.position = new Vector2(111111, 111111);
                //level1[i].SetActive(false);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (stage2 - int.Parse(level2[i].tag) >= 0)
            {

                level2[i].SetActive(true);
            }
            else
            {
                level2[i].SetActive(false);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (stage3 - int.Parse(level3[i].tag) >= 0)
            {
                level3[i].SetActive(true);
            }
            else
            {
                level3[i].SetActive(false);
            }
        }


    }
}
