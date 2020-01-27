using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick_3 : MonoBehaviour
{
    public float destroyTime = 1.0f;
    public float regenerateTime = 1.0f;

    private Animator anim;
    private bool isDropping = false;

    private void Awake()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            if (isDropping == false)
            {
                StartCoroutine( Drop() );
            }
        }
    }

    IEnumerator Drop()
    {
        WaitForSeconds wait01 = new WaitForSeconds(0.1f);
        WaitForSeconds wait10 = new WaitForSeconds(1.0f);
        isDropping = true;

        while (true)
        {
            anim.Play("DropBrick_Ready");

            for (int i = 0; i < destroyTime * 10; i++)
            {
                yield return wait01;
            }

            anim.Play("DropBrick_Drop");
            yield return wait10;

            for (int i = 0; i < regenerateTime * 10; i++)
            {
                yield return wait01;
            }
            anim.Play("DropBrick_Default");
            isDropping = false;
            yield break;
        }
    }
}