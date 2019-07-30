using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    /* 레버같은 오브젝트들의 하위 오브젝트인 target이 무언가와 충돌할 때
     * 그 부모에게 알리기위해 존재합니다.
     */
    private List<GameObject> Colls = new List<GameObject>();

    private bool isStopWithPlatform = false;     
    private bool isCollpsedWithPlatform = false;

    private void Update()
    {
        // 연산이 너무 많을까? 그래봤자 한 맵에 100개도 안될텐데 괜찮으려나?
        isCollpsedWithPlatform = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Colls.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < Colls.Count; i++)
        {
            if (collision.gameObject == Colls[i])
            {
                Colls.RemoveAt(i);
                break;
            }
        }
    }

    public bool GetIsCollWithTag(string tag)
    {
        for (int i = 0; i < Colls.Count; i++)
        {
            if (Colls[i].tag == tag)
            {
                return true;
            }
        }
        return false;
    }
}
