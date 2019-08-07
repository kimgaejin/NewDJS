using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerInAir : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void Awake()
    {
        transform.tag = "DynamicObject";
        rigid = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigid)
        {
            if (rigid.velocity.y < -2)
            {
                tag = "danger";
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "platform")
        {
            tag = "DynamicObject";
        }
    }
}
