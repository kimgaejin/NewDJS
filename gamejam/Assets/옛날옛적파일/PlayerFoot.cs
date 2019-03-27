using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{

    void OnTriggerStay2D(Collider2D other)
    {
        // 뭔가 닿았으면 아직까진 다 땅 아녀?

        Player.playerInstance.SetCanJump();
        
    }
}
