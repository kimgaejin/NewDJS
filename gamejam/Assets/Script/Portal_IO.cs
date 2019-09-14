using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_IO : MonoBehaviour
{
    // 현재는 플랫폼과 닿았을 때 그 플랫폼을 이동시키는 역할
    private Vector3 outputPos;

    private void Awake()
    {
        outputPos = transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform")
        {
            collision.transform.position = outputPos;
        }
    }
}
