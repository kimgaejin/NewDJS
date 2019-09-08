using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Belt : MonoBehaviour
{
    //===========================
    // 기획자를 위한 주석
    [Header("Belts의 하위에 컨베이어벨트들을 복사해서 쓰시면 됩니다.")]

    [Header("수정 ㄴ, 플레이어와 물체간 유효거리")]
    public float DISTANCE_WITH_PLAYER = 3.0f;
    [Space(10)]
    //===========================
    // 수정 가능한 값
    [Space(5)]
    [Header("모든 컨베이어 스피드 * 0이면 벨트 각자의 속도에 따릅니다.")]
    public float speed = 0f;

    [Header("레버가 오른쪽 일 때 실행")]
    [Tooltip("false라면 레버가 왼쪽으로 갔을 때 실행됩니다.")]
    public bool operateWhenLeverIsRight = true;

    //===========================
    // 임의로 바꾸지 마시오
    private Animator anim;

    private MovingWorkRight[] belts;

    // player의 transform.position으로 판독.
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private bool isPlayerLeftBefore;
    private bool isPlayerRightBefore;
    private bool isLeverLeft = true;
    private bool isLeverRight = false;

    private int beltSize;

    //===========================
    // 함수부

    private void Awake()
    {
        Transform beltParent = transform.GetChild(1);
        {   // 컨베이어벨트 초기화
            beltSize = beltParent.childCount;
            belts = new MovingWorkRight[beltSize];

            for (int i = 0; i < beltSize; i++)
            {
                belts[i] = beltParent.GetChild(i).GetComponent<MovingWorkRight>();
            }
            Debug.Log("size: " + beltSize);
        }

        {   // 애니메이션 초기화
            Transform graphic = transform.GetChild(0);
            Debug.Log("graphics: " + graphic.name);
            anim = graphic.GetComponent<Animator>();
            
        }
    }

    private void Start()
    {
        if (operateWhenLeverIsRight)
            AllBeltStop();
        else
            AllBeltExecute();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExecuteWithLight(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
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

            if (operateWhenLeverIsRight) AllBeltExecute();
            if (!operateWhenLeverIsRight) AllBeltStop();
        }

        if (isPlayerRightBefore && isPlayerLeft)
        {   // 레버가 왼쪽으로 돌아갔다.
            LeverTurnLeft();

            if (operateWhenLeverIsRight) AllBeltStop();
            if (!operateWhenLeverIsRight) AllBeltExecute();
        }

        isPlayerLeftBefore = isPlayerLeft;
        isPlayerRightBefore = isPlayerRight;
 */   
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

    private void AllBeltExecute()
    {
        for (int i = 0; i < beltSize; i++)
        {
            if (belts[i])
            {
                belts[i].transform.GetComponent<Rigidbody2D>().WakeUp();
                belts[i].isExecute = true;
                if (speed != 0)
                    belts[i].speed = speed;
            }
        }
    }

    private void AllBeltStop()
    {
        for (int i = 0; i < beltSize; i++)
        {
            if (belts[i])
            {
                belts[i].isExecute = false;
               // Debug.Log("쓰답");
            }
        }
    }

    public void SwitchAll()
    {
        if (isLeverLeft)    // 왼쪽 -> 오른쪽
        {
            LeverTurnRight();
            if (operateWhenLeverIsRight) AllBeltExecute();
            if (!operateWhenLeverIsRight) AllBeltStop();
        }
        else  // 오른쪽 -> 왼쪽
        {
            LeverTurnLeft();
            if (operateWhenLeverIsRight) AllBeltStop();
            if (!operateWhenLeverIsRight) AllBeltExecute();
        }
    }
}
