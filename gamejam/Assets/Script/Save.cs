using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private int stage;
    // Start is called before the first frame update
    void Start()
    {
        stage = int.Parse(this.tag);
       //Debug.Log(stage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("Player"))  
        {
            if (stage / 10 == 0)
            {
                PlayerPrefs.SetInt("Stage1", stage); // 플레그 접촉시 스테이지 저장
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 1 "+stage);
            }
            else if (stage / 10 == 1) {
                PlayerPrefs.SetInt("Stage2", stage); // 플레그 접촉시 스테이지 저장
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 2 "+stage);
            }
            else if (stage / 10 == 2) {
                PlayerPrefs.SetInt("Stage3", stage); // 플레그 접촉시 스테이지 저장
                Player.Stage_pos = GameObject.FindWithTag(stage.ToString()).gameObject.transform.position;
                Debug.Log(" 3 "+stage);
            }

        }
    }
}
