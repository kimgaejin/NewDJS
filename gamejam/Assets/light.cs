using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public float speed = 1.0f;
    public int wasd;
    public int start;

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
    }
}
