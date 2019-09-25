using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectOnMovingWork
{
    public Transform trans;

};

public class MovingWorkRight : MonoBehaviour
{
    public float speed = 1;

    public bool isExecute = true;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "platform") return;

        if (isExecute == false)
        {
            ReturnGravity(collision);
            return;
        }

        TakeObject(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "platform") return;

        ReturnGravity(collision);
    }

    private void TakeObject(Collider2D collision)
    {
        Rigidbody2D rigid;
        rigid = collision.GetComponent<Rigidbody2D>();
        if (!rigid) return;

        float zAngle = transform.rotation.eulerAngles.z;
        float tanValue = Mathf.Abs(Mathf.Tan(zAngle));
        float xRatio = 1 / (tanValue + 1);
        float yRatio = tanValue / (tanValue + 1);

        Vector3 power = new Vector3(xRatio * speed, yRatio * Mathf.Abs(speed), 0);
        collision.transform.position += power / rigid.mass * Time.deltaTime;

        rigid.gravityScale = 0;
    }

    private void ReturnGravity(Collider2D collision)
    {
        // 0으로 만든 중력을 원래대로 되돌립니다. 기본값 2
        Rigidbody2D rigid;
        rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.gravityScale = 2;
        }
    }
}
