﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGetter : MonoBehaviour
{
    private Transform pointsParent;
    private Vector3[] points;
    private Transform target;
    private MovingObject targetMovingObject;
    private Vector3 leverBodyBeforePos;
    private Vector3 parentInterval;
    private int onLazerRegister = 0;

    private Transform executeSprite;
    private Transform unexecuteSprite;

    [Tooltip("물체가 움직이는 속도를 정합니다.")]
    public float speed = 1.0f;

    [Space(5)]
    [Header("기본적으로는 켜지면 다신 안 꺼지고, 포인트를 1 2 3 1 2 3 으로 돕니다.")]
    public bool isExcuting = false;
    //[Header("레버가 오른쪽 일 때 실행")]
    //[Tooltip("false라면 레버가 왼쪽으로 갔을 때 실행됩니다.")]
    //public bool operateWhenLeverIsRight = false;

    [Space(5)]
    [Header("target이 마지막 point에 도착하면 멈춘다.")]
    public bool isStopOnLastPoint = false;

    [Space(5)]
    [Header("레이저를 쏘지 않으면 다시 불이 꺼진다")]
    public bool isTurnOffWithoutLazer= false;

    [Space(5)]
    [Header("레이저가 꺼있으면 역방향으로 간다. ( 초기 위치로 돌아감 )")]
    public bool isReverseWithoutLazer = false;

    [Space(5)]
    [Header("lever와 target이 독립적입니다. (움직이는 플랫폼 위에 있을 때 체크)")]
    public bool isIndependnetLeverWithTarget = false;

    [Space(5)]
    [Header("target이 platform과 충돌 했을 때 멈춘다 T/F. 게임 도중 건들지 마시오")]
    [Tooltip("target이 다른 벽과 충돌하면 멈춥니다.")]
    public bool isStopWhenCollpsedWall = false;

    [Space(5)]
    [Header("target이 platform과 충돌 했을 때 전 포인트로 돌아간다 T/F. 게임 도중 건들지 마시오")]
    public bool isReverseWhenCollpsedWall = false;
    private bool isReverse = false;

    [Space(5)]
    [Header("박스같은 오브젝트도 벽 취급합니다")]
    public bool isCollpsedWithDynamicObjects = false;

    [Space(5)]
    [Tooltip("게임을 시작 했을 때, points들이 투명이 됩니다.")]
    public bool isPointsTransparentInGame = true;

    // player의 transform.position으로 판독.
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private bool isPlayerLeftBefore;
    private bool isPlayerRightBefore;
    private bool isLeverLeft;
    private bool isLeverRight;
    private bool isCollpsedWithPlatform;
    private bool touchPlatform;

    private Vector3 beforeDistance = Vector3.zero;
    private Vector3 distance = Vector3.zero;

    private Vector3 arrow = Vector3.zero;
    private Vector3 fixedPos;

    private int curIndex = 0;   // 현재 물체가 몇 번 point 에 있는지.
    private int indexSize = 0;

    private void Awake()
    {
        {   // 오브젝트가 이동할 경로인 points에 대한 초기화
            pointsParent = this.transform.Find("Points");

            int size = pointsParent.childCount;
            indexSize = size;
            points = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                points[i] = pointsParent.GetChild(i).position;
            }

            if (isPointsTransparentInGame)
            {   // 포인트 투명화
                Color transparent = new Vector4(0, 0, 0, 0);
                for (int i = 0; i < size; i++)
                {
                    pointsParent.GetChild(i).GetComponent<SpriteRenderer>().color = transparent;
                }
            }

            leverBodyBeforePos = transform.position;
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
            if (graphic)
            {
                executeSprite = graphic.GetChild(1);
                unexecuteSprite = graphic.GetChild(0);
            }
        }
    }

    private void Update()
    {

        Vector3 leverBodyPos = transform.position;
        parentInterval = leverBodyBeforePos - leverBodyPos;
        leverBodyBeforePos = transform.position;

        target.transform.position += parentInterval;
        pointsParent.position += parentInterval;

        if (isTurnOffWithoutLazer)
        {
            if (onLazerRegister < 0)
                SwitchOff();
            onLazerRegister--;
        }

        if (isExcuting)
        {
            touchPlatform = TouchWithPlatform();

            if (isReverseWhenCollpsedWall == true && touchPlatform == true)
            {
                isReverse = !isReverse;
            }

            if (isReverse == false)
            {
                int nextInd = curIndex + 1;
                if (nextInd >= indexSize) nextInd = 0;

                if (isStopOnLastPoint && nextInd == 0) return;

                float delta = speed * Time.deltaTime;
                arrow = points[nextInd] - points[curIndex];
                fixedPos = target.transform.position + arrow.normalized * delta;
                distance = points[nextInd] - target.transform.position;

                if (Vector3.Distance(target.position, points[nextInd]) < delta * 2
                    || isEscapeLine(points[curIndex], points[nextInd], delta * 2))
                {
                    curIndex++;
                    if (curIndex >= indexSize) curIndex = 0;
                    fixedPos = points[curIndex];
                }
            }
            else  // 전 포인트로 돌아가야 하는 부분
            {
                int nextInd = curIndex + 1;
                if (nextInd >= indexSize) nextInd = 0;

                float delta = speed * Time.deltaTime;
                arrow = points[curIndex] - points[nextInd];
                fixedPos = target.transform.position + arrow.normalized * delta;
                distance = points[curIndex] - target.transform.position;

                if (Vector3.Distance(target.position, points[curIndex]) < delta * 2
                    || isEscapeLine(points[curIndex], points[nextInd], delta * 2))
                {
                    fixedPos = points[curIndex];
                    curIndex--;
                    if (curIndex < 0) curIndex = indexSize - 1;
                }
            }
        }

        if (isReverseWithoutLazer && !isExcuting)
        {
            int nextInd = curIndex + 1;
            if (nextInd >= indexSize) nextInd = 0;

            float delta = speed * Time.deltaTime;
            arrow = points[curIndex] - points[nextInd];
            fixedPos = target.transform.position + arrow.normalized * delta;
            distance = points[curIndex] - target.transform.position;

            if (Vector3.Distance(target.position, points[curIndex]) < delta * 2
                || isEscapeLine(points[curIndex], points[nextInd], delta * 2))
            {
                fixedPos = points[curIndex];
                curIndex--;
                if (curIndex < 0) curIndex = 0;
            }
        }

       
    }

    private void FixedUpdate()
    {
        if (isExcuting)
        {
            if (isStopWhenCollpsedWall == false
                || (isStopWhenCollpsedWall == true && touchPlatform == false))
            {
                target.transform.position = fixedPos;
            }
        }

        if (isReverseWithoutLazer && !isExcuting)
        {
            target.transform.position = fixedPos;
        }

    }

    private bool TouchWithPlatform()
    {
        int colliderCount = 0;
        Collider2D[] colliders = new Collider2D[20];
        ContactFilter2D contactFilter = new ContactFilter2D();
        colliderCount = target.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders);

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].tag == "platform")
            {
                return true;
            }
        }

        if (isCollpsedWithDynamicObjects)
        {
            for (int i = 0; i < colliderCount; i++)
            {
                if (colliders[i].tag == "DynamicObject")
                {
                    return true;
                }
            }

        }
        return false;
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

    public void Switch()
    {
        isExcuting = !isExcuting;
        if (isExcuting)
        {
            isLeverRight = true;
            isLeverLeft = false;
            if (executeSprite) executeSprite.gameObject.SetActive(true);
            if (unexecuteSprite) unexecuteSprite.gameObject.SetActive(false);
        }
        else
        {
            isLeverRight = false;
            isLeverLeft = true;
            if (executeSprite) executeSprite.gameObject.SetActive(false);
            if (unexecuteSprite) unexecuteSprite.gameObject.SetActive(true);
        }
    }

    private void SwitchOn()
    {
        if (!isExcuting)
        {
            isExcuting = true;
            isLeverRight = true;
            isLeverLeft = false;
            if (executeSprite) executeSprite.gameObject.SetActive(true);
            if (unexecuteSprite) unexecuteSprite.gameObject.SetActive(false);
        }
    }

    public void SwitchOff()
    {
        if (isExcuting)
        {
            isExcuting = false;
            isLeverRight = false;
            isLeverLeft = true;
            if (executeSprite) executeSprite.gameObject.SetActive(false);
            if (unexecuteSprite) unexecuteSprite.gameObject.SetActive(true);
        }
    }

    public void OnLazer()
    {
        SwitchOn();
        onLazerRegister = 10;
    }
}
