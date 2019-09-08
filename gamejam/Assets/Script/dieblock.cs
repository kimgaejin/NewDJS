using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieblock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject block;
    //public int destroytime;
   // public int regeneratetime;
    /*
    private int fDestroyTime = 2;
    private int fTickTime;
    private int fTickTime1;
    */
    private float Step_time;
    public float destroytime;
    public float regeneratetime;


    private int c_enter;
    private Vector2 pos;
    void Start()
    {

        destroytime = 2;
        regeneratetime = 3;
        /*
        fTickTime = 0;
        fTickTime1 = 0;
        */

        c_enter = 0;
        pos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Debug.Log(fTickTime + "   " + fTickTime1);
        fTickTime = fTickTime + 1;
        if (fTickTime - fTickTime1 >= destroytime && c_enter == 1)
        {
           // Debug.Log("asd");
            Block_setActive_false();
        }
        if (fTickTime - fTickTime1 >= regeneratetime && fTickTime - fTickTime1 <= regeneratetime+30) {
            //Debug.Log("asd");
            Block_setActive_true();
            c_enter = 0;
        }
        */
        if (c_enter == 1 && (Time.time - Step_time) > destroytime) {
            Block_setActive_false();
        }
        if (c_enter == 1 && (Time.time - Step_time) > (regeneratetime + destroytime)) {

            Block_setActive_true();
            c_enter = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            c_enter = 1;
            /*
          //  Debug.Log(fTickTime);
            fTickTime1 = fTickTime;
            */
            Step_time = Time.time;
            
        }

    }
    void Block_setActive_true() {

        transform.position = pos;
        // block.gameObject.SetActive(true);
    }
    void Block_setActive_false()
    {
        transform.position = new Vector2(10000, 100000);
        //  block.gameObject.SetActive(false);
    }

}
