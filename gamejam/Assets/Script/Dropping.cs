using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropping : MonoBehaviour
{
    public GameObject shelf1;
    public GameObject shelf2;
    public GameObject shelf3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Destroy(shelf1, 0f);
            Destroy(shelf2, 0.1f);
            Destroy(shelf3, 0.2f);
        }
    }
}