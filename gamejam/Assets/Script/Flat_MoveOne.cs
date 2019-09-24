using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flat_MoveOne : MonoBehaviour
{
    private Vector3[] points;
    private Transform target;
    private Animator anim;

    [Tooltip("물체가 움직이는 속도를 정합니다.")]
    public float speed = 1.0f;

    [Header("target이 무언가와 충돌 했을 때 멈춘다 T/F. 게임 도중 건들지 마시오")]
    [Tooltip("target이 다른 벽과 충돌하면 멈춥니다.")]
    public bool isStopWhenCollpsedWall = true;
    [Header("target이 points의 끝까지 이동하면 다시 뒤로 돌아갑니다.")]
    public bool isBackToLine = false;
    [Header("버튼이 안 눌려있으면 target이 멈춥니다.")]
    public bool isStopWhenNotPressed = false;

    [Space(5)]
    [Tooltip("게임을 시작 했을 때, points들이 투명이 됩니다.")]
    public bool isPointsTransparentInGame = true;

    private Transform pointsParent;
    private Vector3 leverBodyBeforePos;
    private Vector3 parentInterval;

    private bool isPressedBefore = false;
    private bool isPressed = false;
    private int curPressingObject = 0;

    //private Vector3 beforeDistance = Vector3.zero;
    private Vector3 distance = Vector3.zero;

    private Vector3 arrow = Vector3.zero;
    private Vector3 fixedPos;

    private int curIndex = -1;   // 현재 물체가 몇 번 point 에 있는지.
    private int indexSize = 0;

    private void Awake()
    {
        {   // 오브젝트가 이동할 경로인 points에 대한 초기화
            pointsParent = this.transform.Find("Points");

            int size = pointsParent.childCount;
            indexSize = size;
            if (isBackToLine) indexSize = size * 2;
            points = new Vector3[indexSize];
            for (int i = 0; i < size; i++)
            {
                points[i] = pointsParent.GetChild(i).position;
            }

            if (isBackToLine)
            {
                for (int i = size-1; i >= 0; i--)
                {
                    points[size + i] = pointsParent.GetChild(size-1-i).position;
                }
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
                target.tag = "platform";
                target.position = points[0];
                fixedPos = points[0];
            }
            catch { }
        }

        {
            Transform graphic = transform.GetChild(0);
            anim = graphic.GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        Vector3 leverBodyPos = transform.position;
        parentInterval = leverBodyBeforePos - leverBodyPos;
        leverBodyBeforePos = transform.position;

        target.transform.position += parentInterval;
        pointsParent.position += parentInterval;

        if (isPressed)
        {
            if (curIndex < 0) curIndex++;

            int nextInd = curIndex + 1;
            // 최대거리까지 이동했으면, 움직이지 않음.
            if (nextInd < indexSize)
            {
                MoveToNextPos(curIndex, nextInd);   
            }
          
        }
        else
        {
            if (isStopWhenNotPressed == false)
            {
                if (curIndex >= indexSize - 1) curIndex--;

                int nextInd = curIndex + 1;

                if (nextInd >= 1)
                {
                    MoveToNextPos(nextInd, curIndex);
                }
            }
        }

        target.transform.position = fixedPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 움직일 수 있는 강체만
        Rigidbody2D rigid = collision.transform.GetComponent<Rigidbody2D>();

        if (rigid 
            && rigid.bodyType == RigidbodyType2D.Dynamic)
        {
            curPressingObject++;
            if (curPressingObject == 1)
            {
                anim.SetTrigger("press");
                isPressed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rigid = collision.transform.GetComponent<Rigidbody2D>();

        if (rigid 
            && rigid.bodyType == RigidbodyType2D.Dynamic)
        {
            curPressingObject--;

            if (curPressingObject <= 0)
            {
                anim.SetTrigger("depress");
                isPressed = false;
            }
        }
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

    private void MoveToNextPos(int cur, int des)
    {
        float delta = speed * Time.deltaTime;
        arrow = points[des] - points[cur];
        fixedPos = target.transform.position + arrow.normalized * delta;
        distance = points[des] - target.transform.position;

        if (Vector3.Distance(target.position, points[des]) < delta
            || isEscapeLine(points[cur], points[des], delta))
        {
            if (cur < des)
            {
                curIndex++;
                fixedPos = points[curIndex];
                if (isBackToLine
                  && curIndex >= indexSize - 1)
                {
                    curIndex = 0;
                }
            }
            else
            {
                fixedPos = points[curIndex];
                curIndex--;
            }
        }
    }
}
