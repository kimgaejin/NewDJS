using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropping : MonoBehaviour
{
    public GameObject shelf1;
    public GameObject shelf2;
    public GameObject shelf3;
    public GameObject fallen1;
    public GameObject fallen2;
    public GameObject fallen3;
    public int destroytime;
    public int regeneratetime;
    public int interval;
    public int disappear;
    private int fDestroyTime = 2;
    private int fTickTime;
    private int fTickTime1;
    private Vector2 pos1;
    private Vector2 pos2;
    private Vector2 pos3;
    private Vector2 fpos1;
    private Vector2 fpos2;
    private Vector2 fpos3;
    // Start is called before the first frame update
    void Start()
    {
        destroytime = 300;
        fTickTime = 0;
        fTickTime1 = 0;
        interval = 50;
        disappear = 50;
        pos1 = shelf1.gameObject.transform.position;
        pos2 = shelf2.gameObject.transform.position;
        pos3 = shelf3.gameObject.transform.position;
        fpos1 = fallen1.gameObject.transform.position;
        fpos2 = fallen2.gameObject.transform.position;
        fpos3 = fallen3.gameObject.transform.position;
        Destroy(shelf1, 0.1f);
        Destroy(shelf2, 0.1f);
        Destroy(shelf3, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fTickTime + "   " + fTickTime1);
        fTickTime = fTickTime + 1;
    
        if (fTickTime % destroytime == 30)
        {
            //Debug.Log("asd");
            shelf1_setActive_true();

        }
        if (fTickTime % destroytime == disappear)
        {
            //Debug.Log("asd");
            fallen1.transform.position = new Vector2(10000, 100000);

        }


        if (fTickTime % destroytime == interval + 30)
        {
            //Debug.Log("asd");
            shelf2_setActive_true();

        }
        if (fTickTime % destroytime == interval + disappear)
        {
            //Debug.Log("asd");
      
            fallen2.transform.position = new Vector2(10000, 100000);

        }

        if (fTickTime % destroytime == 2*interval + 30)
        {
            //Debug.Log("asd");
            shelf3_setActive_true();

        }
        if (fTickTime % destroytime == 2*interval + disappear)
        {
            //Debug.Log("asd");
 
            fallen3.transform.position = new Vector2(10000, 100000);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {

            //  shelf1.transform.position = new Vector2(10000, 100000);
            // shelf2.transform.position = new Vector2(10000, 100000);
            // shelf3.transform.position = new Vector2(10000, 100000);
            Destroy(shelf1, 0.1f);
            Destroy(shelf2, 0.1f);
            Destroy(shelf3, 0.1f);

        }
    }
    void shelf1_setActive_true()
    {

      //  shelf1.transform.position = pos1;
        fallen1.transform.position = fpos1;

    }
    void shelf1_setActive_false()
    {
        shelf1.transform.position = new Vector2(10000, 300000);
      
    }
    void shelf2_setActive_true()
    {

      //  shelf2.transform.position = pos2;
        fallen2.transform.position = fpos2;

    }
    void shelf2_setActive_false()
    {
        shelf2.transform.position = new Vector2(10000, 200000);

    }
    void shelf3_setActive_true()
    {

       // shelf3.transform.position = pos3;
        fallen3.transform.position = fpos3;

    }
    void shelf3_setActive_false()
    {
        shelf3.transform.position = new Vector2(10000, 400000);

    }

}