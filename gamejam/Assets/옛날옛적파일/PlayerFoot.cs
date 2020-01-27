using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    Transform playerTrans;
    Rigidbody2D playerRigid;
    Player playerScript;

    private void Start()
    {
        playerTrans = transform.parent;
        playerRigid = playerTrans.GetComponent<Rigidbody2D>();
        playerScript = playerTrans.GetComponent<Player>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // 뭔가 닿았으면 아직까진 다 땅 아녀? // 2020-01-27 예외로 트리거 추가
        if (playerRigid.velocity.y <= 0.05f)
        {
            if (other.tag.Equals("Trigger")) return;

            playerScript.SetCanJump();
        }
    }
}
