using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JumpTouch : MonoBehaviour, IPointerDownHandler
{
    private GameObject player;
    private Player playerScript;

    private bool isDown = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        playerScript.Jump(playerScript.jumpPower);
    }
}
