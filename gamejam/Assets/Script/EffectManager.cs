using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Outer Reference
    private Transform tfPlayer;
    private GameObject gmPlayer;


    // Inner Reference
    private Transform[] effects;
    private Animator[] effectAnims;
    private int effectSize;
    private int curEffect;

    private GameObject gmPlayerJump;
    private Transform tfPlayerJumpShadow;
    private Vector3 vec3PlayerJumpShadowScale;

    private GameObject gmEffectCanvas;
    private GameObject gmFade;
    private Animator fadeAnim;

    private GameObject gmCircleSign;

    private GameObject gmTooFarSign;

    private void Awake()
    {
        tfPlayer = GameObject.Find("Player").transform;
        gmPlayer = GameObject.Find("Player");

        // Jump
        gmPlayerJump = transform.Find("PlayerJump").gameObject;
        tfPlayerJumpShadow = gmPlayerJump.transform.Find("Shadow");
        vec3PlayerJumpShadowScale = tfPlayerJumpShadow.localScale;

        gmEffectCanvas = transform.Find("EffectCanvas").gameObject;
        gmFade = gmEffectCanvas.transform.Find("Fade").gameObject;
        fadeAnim = gmFade.GetComponent<Animator>();

        // Touch
        Transform tfTouchInteraction = transform.Find("TouchInteraction");
        // Touch - CircleSigh on levers
        gmCircleSign = tfTouchInteraction.Find("CircleSign").gameObject;

        // Touch - TouchSigns
        Transform tfTouchSigns = tfTouchInteraction.Find("TouchSigns");
        effectSize = this.transform.childCount;
        effects = new Transform[effectSize];
        effectAnims = new Animator[effectSize];
        for (int i = 0; i < effectSize; i++)
        {
            effects[i] = tfTouchSigns.GetChild(i);
            effectAnims[i] = effects[i].GetComponent<Animator>();
        }

        // Touch - AlarmSigns
        Transform tfAlarmSigns = tfTouchInteraction.Find("AlarmSigns");
        gmTooFarSign = tfAlarmSigns.Find("TooFarSign").gameObject;

        gmFade.SetActive(false);
    }

    private void Start()
    {
        FadeIn();
    }

    private void Update()
    {
        OnMouseEvent();
        CircleEffectLocation(tfPlayer.position);
    }

    private void OnMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceEffect();
            LeverTouch();
        }
    }

    private void PlaceEffect()
    {
        NextCurEffect();
        effects[curEffect].transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (effectAnims[curEffect]) effectAnims[curEffect].SetTrigger("Click");
    }

    private bool LeverTouch()
    {
        // 레버와 맞닿았다면 해당 레버 작동
        RaycastHit2D[] hit = new RaycastHit2D[10];
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction);

        bool leastOne = false;
        bool tempRes = false;
        foreach (RaycastHit2D hitOne in hit)
        {
            if (hitOne.transform.tag != "Untagged") continue;

            tempRes = InteractLever(hitOne);
            if (tempRes == true) leastOne = true;
        }

        if (leastOne) return true;
        return false;
    }

    private bool InteractLever(RaycastHit2D hit)
    {
        Lever_MoveOne lever = hit.transform.GetComponent<Lever_MoveOne>();

        if (lever)
        {
            Debug.DrawRay(transform.position, Vector3.right * 3);
            if (Vector3.Distance(tfPlayer.position, lever.transform.position) < lever.DISTANCE_WITH_PLAYER)
            {
                lever.Switch();
                return true;
            }
            else // 레버는 맞는데 거리가 안되는 경우
            {
                CircleEffect();
                Effect_TooFar();
            }
        }

        Lever_Cooler leverCooler = hit.transform.GetComponent<Lever_Cooler>();

        if (leverCooler)
        {
            if (Vector3.Distance(tfPlayer.position, leverCooler.transform.position) < leverCooler.DISTANCE_WITH_PLAYER)
            {
                leverCooler.SwitchAll();
                return true;
            }
            else // 레버는 맞는데 거리가 안되는 경우
            {
                CircleEffect();
                Effect_TooFar();

            }
        }

        Lever_Belt leverBelt = hit.transform.GetComponent<Lever_Belt>();

        if (leverBelt)
        {
            if (Vector3.Distance(tfPlayer.position, leverBelt.transform.position) < leverBelt.DISTANCE_WITH_PLAYER)
            {
                leverBelt.SwitchAll();
                return true;
            }
            else // 레버는 맞는데 거리가 안되는 경우
            {
                CircleEffect();
                Effect_TooFar();

            }
        }
        return false;
    }

    private void NextCurEffect()
    {
        if (effectSize <= 0) return;

        curEffect++;
        if (curEffect >= effectSize)
        {
            curEffect = 0;
        }
    }

    public void CircleEffect()
    {
        gmCircleSign.transform.GetComponent<Animator>().Play("Flicking1_Play", -1 , 0);
    }

    public void CircleEffectLocation(Vector3 position)
    {
        gmCircleSign.transform.position = position;
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

    private void Effect_TooFar()
    {
    
        Debug.Log("MOUSE " + Input.mousePosition);
        Debug.Log("ScreenToWorldPoint " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

        gmTooFarSign.transform.position =  (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gmTooFarSign.GetComponent<Animator>().Play("Effect_Volcano", -1, 0);
    }
}
