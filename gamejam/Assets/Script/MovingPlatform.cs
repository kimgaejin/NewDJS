using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // 공중에 부유하는 플랫폼입니다
    // A->B->C->A->... 계속 해서 반복해서 움직입니다.
    public float speed = 1.0f;

    [Space(5)]
    [Header("target이 platform과 충돌 했을 때 멈춘다 T/F. 게임 도중 건들지 마시오")]
    [Tooltip("target이 다른 벽과 충돌하면 멈춥니다.")]
    public bool isStopWhenCollpsedWall = false;

    [Space(5)]
    [Tooltip("게임을 시작 했을 때, points들이 투명이 됩니다.")]
    public bool isPointsTransparentInGame = true;

    private GameObject target;
    private Transform pointParent;
    private Vector3[] points;
    private int pointSize;
    private int curPoint = 0;

    private void Awake()
    {
        target = transform.GetChild(0).gameObject;
        pointParent = transform.GetChild(1);

        pointSize = pointParent.childCount;
        points = new Vector3[pointSize];

        for (int i = 0; i < pointSize; i++)
        {
            points[i] = pointParent.GetChild(i).position;
            if (isPointsTransparentInGame)
            {
                SpriteRenderer pSpr = pointParent.GetChild(i).GetComponent<SpriteRenderer>();
                if (pSpr) pSpr.color = new Color(0, 0, 0, 0);
            }
        }

        if (isStopWhenCollpsedWall) target.transform.localScale = new Vector3(0.95f, 0.95f, 1);

    }

    private void FixedUpdate()
    {
        // points가 존재하지 않는다면 움직이지 않음.
        if (pointSize <= 0) return;

        bool isTouchWithPlatform = TouchWithPlatform();

        int nextPoint = curPoint + 1;
        if (nextPoint >= pointSize) nextPoint = 0;

        Vector3 beforePos = points[curPoint];
        Vector3 curPos = target.transform.position;
        Vector3 nextPos;
        Vector3 aimPos = points[nextPoint];

        float deltaSpeed = speed * Time.deltaTime;

        nextPos = curPos + (aimPos - curPos).normalized * deltaSpeed;
        if ((curPos - aimPos).magnitude < deltaSpeed
            || isEscapeLine(beforePos, nextPos, deltaSpeed)) 
        {
            curPoint++;
            if (curPoint >= pointSize) curPoint = 0;
            nextPos = points[curPoint];
        }

        if (isStopWhenCollpsedWall == false
            || (isStopWhenCollpsedWall == true && isTouchWithPlatform == false))
        {
            target.transform.position = nextPos;
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

        return false;
    }

    private bool isEscapeLine(Vector3 before, Vector3 next, float delta)
    {
        // 이동중인 플랫폼이 경로를 이탈하는것을 찾기위한 조건
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

        if (min.x - delta <= target.transform.position.x && target.transform.position.x <= max.x + delta)
        {
            if (min.y - delta <= target.transform.position.y && target.transform.position.y <= max.y + delta)
            {
                return false;
            }
        }

        return true;
    }
}
