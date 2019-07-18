using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject PortalA;
    public GameObject PotalB;
    static public int Portal_A;
    static public int Light_Portal_A;
    static public Vector2 Portal_A_pos;
    static public Vector2 Light_Portal_A_pos;

    // Start is called before the first frame update
    void Start()
    {
        Portal_A = 0;
        Light_Portal_A = 0;
        // Portal_A_pos = PortalA.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Portal_A = 1;
            Portal_A_pos = PortalA.gameObject.transform.position;
            PortalB.Portal_B_pos = PotalB.gameObject.transform.position;
            //Debug.Log("asdsad");
           

        }
        if (col.gameObject.tag.Equals("light"))
        {
            Light_Portal_A = 1;
            Light_Portal_A_pos = PortalA.gameObject.transform.position;
            PortalB.Light_Portal_B_pos = PotalB.gameObject.transform.position;
            Debug.Log("litghta");


        }


    }
}
