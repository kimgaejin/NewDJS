using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public float speed = 1.0f;
    public int wasd;
    public int start;


    //포탈 관련 변수
    public static char key_down;
    public GameObject potalA;
    public GameObject potalB;
    //


    // Start is called before the first frame update
    void Start()
    {
        wasd = 0;
        if (Player.key_down == 'D') {
            start = 0;
        }
        else if (Player.key_down == 'A')
        {
            start = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wasd == 0)
        {
            if (start == 0)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (start == 1 ) {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else if (wasd == 1) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("mirrorup"))
        {
            wasd = 1;
        }
        /////포탈 충돌관련 함수
        for (int i = 0; i < 10; i++)
        {
            if (col.gameObject.tag.Equals("potalA" + i))
            {
     
                potalB = GameObject.FindWithTag("potalB" + i);
               if (Player.key_down == 'A')
               {
                    transform.position = new Vector2((10 * potalB.transform.position.x - 7) / 10, potalB.transform.position.y);
               }
               else if (Player.key_down == 'D')
               {
                  Debug.Log("asdfasdfasdf");
                  transform.position = new Vector2((10 * potalB.transform.position.x + 7) / 10, potalB.transform.position.y);
               }

            }

        }

        for (int i = 0; i < 10; i++)
        {
            if (col.gameObject.tag.Equals("potalB" + i))
            {
                potalA = GameObject.FindWithTag("potalA" + i);
                if (Player.key_down == 'A')
                {
                   transform.position = new Vector2((10 * potalA.transform.position.x - 7) / 10, potalA.transform.position.y);
                }
                else if (Player.key_down == 'D')
                {
                   transform.position = new Vector2((10 * potalA.transform.position.x + 7) / 10, potalA.transform.position.y);
                }

            }
        }
        ///////////////////////////////////////
    }

}
