using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Lever_MoveOne : MonoBehaviour
{

    private Vector3[] points;
    private Transform target;
    private MovingObject targetMovingObject;
    private Animator anim;

    [Tooltip("물체가 움직이는 속도를 정합니다.")]
    public float speed = 1.0f;

    [Space(5)]
    [Header("레버가 오른쪽 일 때 실행")]
    [Tooltip("false라면 레버가 왼쪽으로 갔을 때 실행됩니다.")]
    public bool operateWhenLeverIsRight = false;
    

    [Space(5)]
    [Header("target이 platform과 충돌 했을 때 멈춘다 T/F. 게임 도중 건들지 마시오")]
    [Tooltip("target이 다른 벽과 충돌하면 멈춥니다.")]
    public bool isStopWhenCollpsedWall = false;

    [Space(5)]
    [Tooltip("게임을 시작 했을 때, points들이 투명이 됩니다.")]
    public bool isPointsTransparentInGame = true;

    /*
    // 미구현
    [Space(5)]
    [Header("target이 이동 할 때; 기본값: 모든 지점을 순환한다.")]
    [Tooltip("target이 마지막 지점에 도달하면 멈춥니다.")]
    public bool isStopWhenLastPoint= false;
    [Tooltip("target이 마지막 지점에 도달하면 역순환합니다.")]
    public bool isReturnWhenCollpsedWall = false;
    */

    //[Tooltip("target이 다른 벽과 충돌하면 이제 반대 방향으로 회전합니다.")]
    // public bool isReturnWhenCollpsedWall = false;

    // player의 transform.position으로 판독.
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private bool isPlayerLeftBefore;
    private bool isPlayerRightBefore;
    private bool isLeverLeft;
    private bool isLeverRight;
    private bool isCollpsedWithPlatform;

    private Vector3 beforeDistance = Vector3.zero;
    private Vector3 distance = Vector3.zero;

    private Vector3 arrow = Vector3.zero;
    private Vector3 fixedPos;

    private int curIndex = 0;   // 현재 물체가 몇 번 point 에 있는지.
    private int indexSize = 0;

    private void Awake()
    {
        {   // 오브젝트가 이동할 경로인 points에 대한 초기화
            Transform pointsParent;
            pointsParent = this.transform.Find("Points");

            int size = pointsParent.childCount;
            indexSize = size;
            points = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                points[i] = pointsParent.GetChild(i).position;
            }

            if (isPointsTransparentInGame)
            {
                Color transparent = new Vector4(0, 0, 0, 0);
                for (int i = 0; i < size; i++)
                {
                    pointsParent.GetChild(i).GetComponent<SpriteRenderer>().color = transparent;
                }
            }
        }

        {   // target 오브젝트 초기 위치 설정
            target = this.transform.GetChild(1);
            try
            {
                if (isStopWhenCollpsedWall) target.localScale = new Vector3(0.95f, 0.95f, 1);
                target.position = points[0];
                fixedPos = points[0];
                target.tag = "platform";
                targetMovingObject = target.GetComponent<MovingObject>();
            }
            catch { }
        }

        {
            Transform graphic = transform.GetChild(0);
            anim = graphic.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // 레버가 작동 상태일 때
        if ((operateWhenLeverIsRight && isLeverRight)
            || (operateWhenLeverIsRight == false && isLeverLeft))
        {
            int nextInd = curIndex + 1;
            if (nextInd >= indexSize) nextInd = 0;

            float delta = speed * Time.deltaTime;
            arrow = points[nextInd] - points[curIndex];
            fixedPos = target.transform.position + arrow.normalized * delta;
            distance = points[nextInd] - target.transform.position;

            if (Vector3.Distance(target.position, points[nextInd]) < delta
                || isEscapeLine(points[curIndex], points[nextInd], delta))
            {
                curIndex++;
                if (curIndex >= indexSize) curIndex = 0;
                fixedPos = points[curIndex];
            }

        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("targetMoving " + targetMovingObject.GetIsCollWithTag("platform"));

        int colliderCount = 0;
        Collider2D[] colliders = new Collider2D[20];
        ContactFilter2D contactFilter = new ContactFilter2D();
        colliderCount = target.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders);
        bool isTouchingWithPlatform = false;

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].tag == "platform")
            {
                isTouchingWithPlatform = true;
                break;
            }
        }

        //Debug.Log("isTouchingWithPlatform " + isTouchingWithPlatform);

        if (isStopWhenCollpsedWall == false
            || (isStopWhenCollpsedWall ==true && isTouchingWithPlatform == false))
        {
            target.transform.position = fixedPos;
        }
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
        {
            isLeverLeft = false;
            isLeverRight = true;
        }

        if (isPlayerRightBefore && isPlayerLeft)
        {
            isLeverLeft = true;
            isLeverRight = false;
        }

        if (isPlayerLeftBefore && isPlayerRight) anim.SetTrigger("handleToRight");
        if (isPlayerRightBefore && isPlayerLeft) anim.SetTrigger("handleToLeft");

        isPlayerLeftBefore = isPlayerLeft;
        isPlayerRightBefore = isPlayerRight;
    }

    private bool isEscapeLine(Vector3 before, Vector3 next, float delta)
    {
        float xMax = before.x;
        float yMax = before.y;
        float xMin = before.x;
        float yMin = before.y;

        if (xMax < next.x) xMax = next.x;
        if (yMax < next.y) yMax = next.y;
        if (xMin > next.x) xMin = next.x;
        if (yMin > next.y) yMin = next.y;

        Vector2 max = new Vector2(xMax, yMax);
        Vector2 min = new Vector2(xMin, yMin);


        if (min.x - delta <= target.position.x && target.position.x <= max.x + delta)
        {
            if (min.y - delta <= target.position.y && target.position.y <= max.y + delta)
            {
                return false;
            }
        }

        return true;
    }

}
