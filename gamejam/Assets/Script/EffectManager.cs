using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private GameObject gmPlayer;
    private GameObject gmCMvcam;

    private GameObject gmPlayerJump;

    private GameObject gmEffectCanvas;
    private GameObject gmFade;


    private Transform tfPlayerJumpShadow;
    private Vector3 vec3PlayerJumpShadowScale;
    private Animator fadeAnim;


    private void Awake()
    {
        gmPlayer = GameObject.Find("Player");
        gmCMvcam = GameObject.Find("CM vcam1");
        gmEffectCanvas = GameObject.Find("EffectCanvas");
        gmFade = gmEffectCanvas.transform.Find("Fade").gameObject;

        gmPlayerJump = transform.Find("PlayerJump").gameObject;

        tfPlayerJumpShadow = gmPlayerJump.transform.Find("Shadow");
        vec3PlayerJumpShadowScale = tfPlayerJumpShadow.localScale;
        fadeAnim = gmFade.GetComponent<Animator>();
    }

    private void Start()
    {
        fadeAnim.Play("FadeIn");
    }

    public void PlayerShadow()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(gmPlayer.transform.position, Vector2.down * 20);
        bool hited = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "platform")
            {
                tfPlayerJumpShadow.position = hit.point;
                float scaleValue = 0.85f / (gmPlayer.transform.position.y - hit.point.y);
                if (scaleValue > 1)
                {
                    scaleValue = 1.0f;
                }
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
