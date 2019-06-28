using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMoving : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "platform" && collision.transform.GetComponent<Rigidbody2D>())
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "platform" && collision.transform.GetComponent<Rigidbody2D>())
            collision.transform.parent = null;
    }

    // 위에 올라가 있는 물체만 움직여야하기 때문에 OnColision을 쓰지 않음.
    // 정확히는 쓸 이유가 없다고 봤음
}
