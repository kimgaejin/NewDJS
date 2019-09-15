using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyPoint : MonoBehaviour
{
    private Transform playerTrans;
    private Player playerScript;

    private void Start()
    {
        playerTrans = transform.parent;
        playerScript = playerTrans.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("platform")
            || collision.tag.Equals("staticObject"))
        {
            if (collision.GetComponent<PlatformEffector2D>()) return;

            playerScript.Die();
        }
    }
}
