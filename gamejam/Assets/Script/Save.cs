using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private AudioManager audioManager;

    private int stage;
    Animator animator;
    bool passed;
    // Start is called before the first frame update
    void Start()
    {
        stage = int.Parse(this.tag);
        animator = transform.GetChild(0).GetComponent<Animator>();
        if (stage / 10 == 0) {
            if (stage < PlayerPrefs.GetInt("StageB1")) {
                animator.SetBool("passed", true);
            }
        }
        else if (stage / 10 == 1)
        {
            if (stage < PlayerPrefs.GetInt("StageB2"))
            {
                animator.SetBool("passed", true);
            }
        }
        else if (stage / 10 == 2)
        {
            if (stage < PlayerPrefs.GetInt("StageB3"))
            {
                animator.SetBool("passed", true);
            }
        }
        else if (stage / 10 == 3)
        {
            if (stage < PlayerPrefs.GetInt("StageB4"))
            {
                animator.SetBool("passed", true);
            }
        }
        else if (stage / 10 == 4)
        {
            if (stage < PlayerPrefs.GetInt("StageB5"))
            {
                animator.SetBool("passed", true);
            }
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //Debug.Log(stage);
    }

    // Update is called once per frame
    void Update()
    {
       // animator.SetBool("passed", true);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("Player"))  
        {
            if (audioManager)
                audioManager.PlayEffect("Ding Sound Effect");

            if (stage / 10 == 0)
            {
                PlayerPrefs.SetInt("Stage1", stage); // 플레그 접촉시 스테이지 저장
                if (stage < PlayerPrefs.GetInt("StageB1")) { }
                else { PlayerPrefs.SetInt("StageB1", stage); }
           
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 1 "+stage);
            }
            else if (stage / 10 == 1) {
                PlayerPrefs.SetInt("Stage2", stage); // 플레그 접촉시 스테이지 저장
                if (stage < PlayerPrefs.GetInt("StageB2")) { }
                else { PlayerPrefs.SetInt("StageB2", stage); }
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 2 "+stage);
            }
            else if (stage / 10 == 2) {
                PlayerPrefs.SetInt("Stage3", stage); // 플레그 접촉시 스테이지 저장
                if (stage < PlayerPrefs.GetInt("StageB3")) { }
                else { PlayerPrefs.SetInt("StageB3", stage); }
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 3 "+stage);
            }
            else if (stage / 10 == 3)
            {
                PlayerPrefs.SetInt("Stage4", stage); // 플레그 접촉시 스테이지 저장
                if (stage < PlayerPrefs.GetInt("StageB4")) { }
                else { PlayerPrefs.SetInt("StageB4", stage); }
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 4 " + stage);
            }
            else if (stage / 10 == 4)
            {
                PlayerPrefs.SetInt("Stage5", stage); // 플레그 접촉시 스테이지 저장
                if (stage < PlayerPrefs.GetInt("StageB5")) { }
                else { PlayerPrefs.SetInt("StageB5", stage); }
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 5 " + stage);
            }

            animator.SetBool("passed", true);
            //passed = true;


        }
    }

    IEnumerator ExitScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            // 메인 씬으로 이동
        }
    }
}
