﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flat_MoveOne : MonoBehaviour
{
    private Vector3[] points;
    private Transform target;
    private Animator anim;

    public bool operateWhenLeverIsRight = true;
    public bool isPointsTransparentInGame = true;

    private bool isPressedBefore = false;
    private bool isPressed = false;

    //private Vector3 beforeDistance = Vector3.zero;
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
        Debug.Log("curInd: " + curIndex);

        if (isPressed)
        {
            if (curIndex < 0) curIndex++;

            int nextInd = curIndex + 1;
            // 최대거리까지 이동했으면, 움직이지 않음.
            if (nextInd < indexSize)
            {
                arrow = points[nextInd] - points[curIndex];
                fixedPos = target.transform.position + arrow.normalized * Time.deltaTime;
                distance = points[nextInd] - target.transform.position;

                if (Vector3.Distance(target.position, points[nextInd]) < 0.1f)
                {
                    curIndex++;
                }

            }
        }
        else
        {
            if (curIndex >= indexSize - 1) curIndex--;

            int nextInd = curIndex + 1;

            if (curIndex >= 0)
            {
                arrow = points[curIndex] - points[nextInd];
                fixedPos = target.transform.position + arrow.normalized * Time.deltaTime;
                distance = points[curIndex] - target.transform.position;

                if (Vector3.Distance(target.position, points[curIndex]) < 0.1f)
                {
                    curIndex--;
                }

            }
        }

        target.transform.position = fixedPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPressed = false;
        }
    }
}
