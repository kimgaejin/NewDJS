using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalB : MonoBehaviour
{
    public GameObject PotalA;
    public GameObject PotalB;
    static public int Portal_B;
    static public int Light_Portal_B;
    static public Vector2 Portal_B_pos;
    static public Vector2 Light_Portal_B_pos;
    // Start is called before the first frame update
    void Start()
    {
        Portal_B = 0;
        Light_Portal_B=0;
        //Portal_B_pos = PotalB.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
     
        if (col.gameObject.tag.Equals("Player"))
        {
            Portal_B = 1;
            Portal_B_pos = PotalB.gameObject.transform.position;
            Portal.Portal_A_pos = PotalA.gameObject.transform.position;
          //  Debug.Log("asdsaasdfasdfasdfd");


        }
        if (col.gameObject.tag.Equals("light"))
        {
            Debug.Log("Lightb");
            Light_Portal_B = 1;
            Light_Portal_B_pos = PotalB.gameObject.transform.position;
            Portal.Light_Portal_A_pos = PotalA.gameObject.transform.position;
            Debug.Log("Lightb");


        }

    }
}
