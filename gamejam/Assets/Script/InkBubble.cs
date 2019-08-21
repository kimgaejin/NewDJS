using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBubble : MonoBehaviour
{
    // 잉크방울이 충돌 후 state2로 바뀝니다.
    Transform state1;   // 떨어지는 잉크 Sprite만 존재
    Transform state2;   // 떨어진 잉크 자리에 생겨난 잉크자국, BoxCollider 존재

    private void Awake()
    {
        state1 = transform.GetChild(0);
        state2 = transform.GetChild(1);

        state1.gameObject.SetActive(true);
        state2.gameObject.SetActive(false);

        if (!state1 || !state2)
             Debug.Log("잉크방울 자식개체 참고가 안됨");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform"
            || collision.tag == "DynamicObject") 
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.isKinematic = true;
            rigid.velocity = Vector2.zero;
            state1.gameObject.SetActive(false);
            state2.gameObject.SetActive(true);
            state2.position = state1.position + new Vector3(0, -0.2f, 0);
           
            StartCoroutine("InkFadeOut");
            Destroy(this.gameObject, 2.0f);
        }
    }

    IEnumerator InkFadeOut()
    {
        float frame = 0.05f;
        int perSecondFrame = (int)(1 / frame);
        WaitForSeconds wait05 = new WaitForSeconds(frame);
        SpriteRenderer state2_spr = state2.GetComponent<SpriteRenderer>();
        

        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            BoxCollider2D state2_boxColl = state2.GetComponent<BoxCollider2D>();
            state2_boxColl.enabled = false;

            Color color = state2_spr.color;
            color = new Color(0, 0, 0, color.a / perSecondFrame);
            for (int i = 0; i < perSecondFrame; i++)
            {
                state2_spr.color -= color;
                yield return wait05;
            }
            yield break;
        }
    }

}
