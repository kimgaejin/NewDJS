using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMoving : MonoBehaviour
{
    private Vector3 befPos;
    private Vector3 posDiff;

    private Transform parent;
    private Vector3 parentBefPos;

    private float jumpPowerSave;


    private void Start()
    {
        befPos = transform.position;
        posDiff = Vector3.zero;
        parent = transform.parent;
        parentBefPos = parent.transform.position;
    }

    private void LateUpdate()
    {
        posDiff = transform.position - befPos;
        Vector3 parentDiff = parent.position - parentBefPos;

        if (posDiff.magnitude > parentDiff.magnitude)
        {
            transform.position = befPos + parentDiff;
        }

        befPos = transform.position;
        parentBefPos = parent.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            || collision.tag == "DynamicObject")
        {
            if (collision.transform.GetComponent<Rigidbody2D>())
            {
                collision.transform.parent = transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"
            || collision.tag == "DynamicObject")
        {
            if (collision.transform.GetComponent<Rigidbody2D>())
            {
                collision.transform.parent = null;
            }
        }
        
    }

    // 위에 올라가 있는 물체만 움직여야하기 때문에 OnColision을 쓰지 않음.
    // 정확히는 쓸 이유가 없다고 봤음
}
