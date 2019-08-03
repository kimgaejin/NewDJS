using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    // 플레이어만 넉백시키는 코드
    // 플레이어 - 물체 + 약간 위로 넉백

    [Tooltip("넉백 파워")]
    public float scalarPower = 7.0f;
    public float UPWARD_POWER = 3.0f;

    private void Awake()
    {
        transform.tag = "staticObject";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        Vector3 distance = transform.position - collision.transform.position;
        Vector2 power = -(Vector2)distance * scalarPower + (Vector2.up * UPWARD_POWER);


        Rigidbody2D rigid;
        rigid = collision.transform.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.AddForce(power, ForceMode2D.Impulse);
        }
        
    }
}
