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

    private Transform graphicsParent;
    private Transform graphics;
    private Animator anim;

    void Start()
    {

        graphicsParent = transform.FindChild("GraphicsParent");
        if (graphicsParent) graphics = graphicsParent.FindChild("Graphics");
        if (graphics)
            anim = graphics.GetComponent<Animator>();

        destroytime = 2;
        regeneratetime = 3;

        c_enter = 0;
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (c_enter == 1 && (Time.time - Step_time) > destroytime) {
            if (anim) anim.SetTrigger("Drop");
            Block_setActive_false();
        }
        if (c_enter == 1 && (Time.time - Step_time) > (regeneratetime + destroytime)) {
            if (anim) anim.SetTrigger("BackToOriginState");
            Block_setActive_true();
            c_enter = 0;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            c_enter = 1;
            if (anim) anim.SetTrigger("PlayerOnThis");
            Step_time = Time.time;
            
        }

    }

    void Block_setActive_true() {

        transform.position = pos;
        if (graphics)
        {
            graphicsParent.transform.position = pos;
        }
    }

    void Block_setActive_false()
    {
        transform.position = new Vector2(10000, 100000);
        if (graphics)
        {
            graphicsParent.transform.position = pos;
        }
    }

}
