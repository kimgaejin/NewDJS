using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveLeftTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject player;
    private Player playerScript;

    private bool isDown = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (isDown) playerScript.MoveLeft();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        Debug.Log("mouse points down");
        isDown = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        isDown = false;
    }
}
