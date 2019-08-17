using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Cooler : MonoBehaviour
{
    //===========================
    // 기획자를 위한 주석
    [Header("Coolers 하위에 선풍기들을 복사해서 쓰시면 됩니다.")]

    //===========================
    // 수정 가능한 값
    [Space(5)]
    [Header("모든 선풍기 힘 (* 0이면 각자의 선풍기 속도에 따릅니다.)")]
    public float power = 0f;

    [Header("레버가 오른쪽 일 때 실행")]
    [Tooltip("false라면 레버가 왼쪽으로 갔을 때 실행됩니다.")]
    public bool operateWhenLeverIsRight = true;

    //===========================
    // 임의로 바꾸지 마시오
    private Animator anim;

    private Cooler[] coolers;

    // player의 transform.position으로 판독.
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private bool isPlayerLeftBefore;
    private bool isPlayerRightBefore;
    private bool isLeverLeft = true;
    private bool isLeverRight = false;

    private int coolerSize;

    //===========================
    // 함수부

    private void Awake()
    {
        {   // 쿨러 초기화
            Transform coolerParent = transform.GetChild(1);
            coolerSize = coolerParent.childCount;
            coolers = new Cooler[coolerSize];

            for (int i = 0; i < coolerSize; i++)
            {
                coolers[i] = coolerParent.GetChild(i).GetComponent<Cooler>();
            }
        }

        {   // 애니메이션 초기화
            Transform graphic = transform.GetChild(0);
            Debug.Log("graphics: " + graphic.name);
            anim = graphic.GetComponent<Animator>();

        }
    }

    private void Start()
    {
        AllCoolerTurnOff();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExecuteWithLight(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < this.transform.position.x)
            {
                isPlayerLeft = true;
                isPlayerRight = false;
            }
            else
            {
                isPlayerLeft = false;
                isPlayerRight = true;
            }
        }

        if (isPlayerLeftBefore && isPlayerRight)
        {   // 레버가 오른쪽으로 돌아갔다.
            LeverTurnRight();
            if (operateWhenLeverIsRight) AllCoolerTurnOn();
            if (!operateWhenLeverIsRight) AllCoolerTurnOff();
        }

        if (isPlayerRightBefore && isPlayerLeft)
        {   // 레버가 왼쪽으로 돌아갔다.
            LeverTurnLeft();
            if (operateWhenLeverIsRight) AllCoolerTurnOff();
            if (!operateWhenLeverIsRight) AllCoolerTurnOn();
        }

        isPlayerLeftBefore = isPlayerLeft;
        isPlayerRightBefore = isPlayerRight;
    }

    private void ExecuteWithLight(Collider2D collision)
    {
        // 빛하고 닿았을 때, 실행되는 방향으로 레버를 돌린다.
        if (collision.tag == "light")
        {
            if (operateWhenLeverIsRight)
            {
                LeverTurnRight();
            }
            else
            {
                LeverTurnLeft();
            }
        }

    }

    private void LeverTurnLeft()
    {
        isLeverLeft = true;
        isLeverRight = false;
        anim.SetTrigger("handleToLeft");
    }

    private void LeverTurnRight()
    {
        isLeverLeft = false;
        isLeverRight = true;
        anim.SetTrigger("handleToRight");
    }

    private void AllCoolerTurnOn()
    {
        for (int i = 0; i < coolerSize; i++)
        {
            if (coolers[i])
            {
                coolers[i].ExecutionOn();
                if (power != 0)
                {
                    coolers[i].power = power;
                }
            }
        
        }
    }

    private void AllCoolerTurnOff()
    {
        for (int i = 0; i < coolerSize; i++)
        {
            if (coolers[i])
            {
                coolers[i].ExecutionOff();
            }
        }
    }
}
