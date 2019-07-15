using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;

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
            SpriteRenderer pSpr = pointParent.GetChild(i).GetComponent<SpriteRenderer>();
            if (pSpr) pSpr.color = new Color(0,0,0,0);
        }
    }

    private void FixedUpdate()
    {
        // points가 존재하지 않는다면 움직이지 않음.
        if (pointSize <= 0) return;

        int nextPoint = curPoint + 1;
        if (nextPoint >= pointSize) nextPoint = 0;

        Vector3 curPos = target.transform.position;
        Vector3 aimPos = points[nextPoint];

        float deltaSpeed = speed * Time.deltaTime;

        curPos += (aimPos - curPos).normalized * deltaSpeed;
        if ((curPos - aimPos).magnitude < deltaSpeed)
        {
            curPoint++;
            if (curPoint >= pointSize) curPoint = 0;
            curPos = points[curPoint];
        }

        target.transform.position = curPos;
    }
}
