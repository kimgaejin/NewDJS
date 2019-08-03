using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWorkRight : MonoBehaviour
{
    /* 오른쪽으로만 움직이는 컨베이어 벨트
     * 
     * 논란:
     * - 무빙워크가 두 개 이상 겹쳐있을 때, 다음 무빙워크에 막혀서 전진이 안되던데. 그것을 고치기 위해 AddForce로 위로 띄움.
     * - 근데 그렇게 하니 되게 부들부들대면서 진행함. 나중에 문제가 있으면 수정하고 아니면 그냥 사용.
     * 
     * - transform.position을 직접 옮겨서 바꾸니 해결은 되지만, 이제 경사로가 안됨.
     */

    public float speed = 1;

    //private Vector3 risingPower = new Vector3(0, Time.deltaTime, 0);

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "platform") return;
        if (collision.tag == "danger") return;

        Rigidbody2D rigid;
        rigid = collision.GetComponent<Rigidbody2D>();
        if (!rigid) return;

        float zAngle = transform.rotation.eulerAngles.z;
        float tanValue = Mathf.Abs(Mathf.Tan(zAngle));
        float xRatio = 1 / (tanValue + 1);
        float yRatio = tanValue / (tanValue + 1);

        Vector3 power = new Vector3(xRatio * speed, yRatio * Mathf.Abs(speed), 0);
        collision.transform.position += power * Time.deltaTime;
        rigid.gravityScale = 0;
        //rigid.AddForce(power , ForceMode2D.Impulse);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "platform") return;
        if (collision.tag == "danger") return;

        Rigidbody2D rigid;

        rigid = collision.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.gravityScale = 2;
        }
    }
}
