using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMoving : MonoBehaviour
{
    private Vector3 befPos;
    private Vector3 curPos;
    private Vector3 posDiff;

    private float jumpPowerSave;

    private GameObject playerObj;
    private Player playerScript;

    private void Start()
    {
        befPos = transform.position;
        curPos = transform.position;
        posDiff = Vector3.zero;
        playerObj = GameObject.Find("Player");
        playerScript = playerObj.GetComponent<Player>();
    }

    private void Update()
    {
        curPos = transform.position;
        posDiff = curPos - befPos;
        befPos = curPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
