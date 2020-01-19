using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Outer Reference
    private GameObject gmPlayer;


    // Inner Reference
    private GameObject gmPlayerJump;
    private Transform tfPlayerJumpShadow;
    private Vector3 vec3PlayerJumpShadowScale;


    private GameObject gmEffectCanvas;
    private GameObject gmFade;
    private Animator fadeAnim;
    


    private void Awake()
    {
        gmPlayer = GameObject.Find("Player");

        gmPlayerJump = transform.Find("PlayerJump").gameObject;
        tfPlayerJumpShadow = gmPlayerJump.transform.Find("Shadow");
        vec3PlayerJumpShadowScale = tfPlayerJumpShadow.localScale;

        gmEffectCanvas = transform.Find("EffectCanvas").gameObject;
        gmFade = gmEffectCanvas.transform.Find("Fade").gameObject;
        fadeAnim = gmFade.GetComponent<Animator>();
        gmFade.SetActive(false);
    }

    private void Start()
    {
        FadeIn();
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
                if (scaleValue > 1) scaleValue = 1;
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

    public void FadeOut()
    {
        gmFade.SetActive(true);
        fadeAnim.Play("FadeOut");
    }

    public void FadeIn()
    {
        gmFade.SetActive(true);
        fadeAnim.Play("FadeIn");
        Invoke("FadeSetActiveFalse", 1.0f);
    }

    private void FadeSetActiveFalse()
    {
        gmFade.SetActive(false);
    }
}
