using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private GameObject gmPlayer;

    private GameObject gmPlayerJump;

    private Transform tfPlayerJumpShadow;
    private Vector3 vec3PlayerJumpShadowScale;

    private void Awake()
    {
        gmPlayer = GameObject.Find("Player");

        gmPlayerJump = transform.Find("PlayerJump").gameObject;

        tfPlayerJumpShadow = gmPlayerJump.transform.Find("Shadow");
        vec3PlayerJumpShadowScale = tfPlayerJumpShadow.localScale;
    }

    private void Start()
    {
        
    }

    public void PlayerShadow()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(gmPlayer.transform.position, Vector2.down * 20);
        bool hited = false;
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log("hit: "+hit.transform.name);
            if (hit.transform.tag == "platform")
            {
                tfPlayerJumpShadow.position = hit.point;
                float scaleValue = 0.85f / (gmPlayer.transform.position.y - hit.point.y);
                tfPlayerJumpShadow.localScale = scaleValue * vec3PlayerJumpShadowScale;
                hited = true;
                break;
            }
        }

        if (hited == false)
        {
            tfPlayerJumpShadow.position = Vector3.zero;
        }
    }

    public void PlayerJump_SetActive(bool type)
    {
        gmPlayerJump.SetActive(type);
    }
}
