using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Lever_MoveOne : MonoBehaviour
{
    Vector3[] points;
    Transform target;

    public bool operateWhenLeverIsRight = true;

    // player의 transform.position으로 판독.
    private bool isPlayerLeft;
    private bool isPlayerRight;
    private bool isPlayerLeftBefore;
    private bool isPlayerRightBefore;
    private bool isLeverLeft;
    private bool isLeverRight;

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
            pointsParent = this.transform.FindChild("Points");

            int size = pointsParent.childCount;
            indexSize = size;
            points = new Vector3[size];
            for (int i = 0; i < size; i++)
            {
                points[i] = pointsParent.GetChild(i).position;
            }
            Debug.Log("localPos: " + pointsParent.GetChild(0).localPosition);
            Debug.Log("pos: " + pointsParent.GetChild(0).position);
        }

        {   // target 오브젝트 초기 위치 설정
            target = this.transform.GetChild(1);
            try
            {
                target.position = points[0];
                fixedPos = points[0];
            }
            catch { }

           
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
            arrow = points[nextInd] - points[curIndex];
            fixedPos = target.transform.position + arrow.normalized * Time.deltaTime;
            distance = points[nextInd] - target.transform.position;

            if (Vector3.Distance(target.position, points[nextInd]) < 0.1f)
            {
                curIndex++;
                if (curIndex >= indexSize) curIndex = 0;
                beforeDistance = points[curIndex];
            }
            else
            {
                beforeDistance = distance;
            }
           
        }
        
    }

    private void FixedUpdate()
    {
        target.transform.position = fixedPos;
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

        isPlayerLeftBefore = isPlayerLeft;
        isPlayerRightBefore = isPlayerRight;
    }



}
