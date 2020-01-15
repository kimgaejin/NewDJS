
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroier : MonoBehaviour
{
    [Range(0.5f, 10)]
    [Tooltip("분쇄기가 내려오는 시간")]
    public float fallDownTime = 2.0f;
    [Range(0.5f, 10)]
    [Tooltip("분쇄기가 아래에서 대기하는 시간")]
    public float waitingTimeOnBottom = 2.0f;
    [Range(0.5f, 10)]
    [Tooltip("분쇄기가 올라가는 시간")]
    public float risingUpTime = 2.0f;
    [Range(0.5f, 10)]
    [Tooltip("분쇄기가 위에서 대기하는 시간")]
    public float waitingTimeOnTop = 2.0f;

    [Tooltip("몇 초부터 쾅 을 시작할지 간격 시간")]
    public float stnadTime = 0.0f;

    private Rigidbody2D rigid;
    private Vector3 originPos;
    private Transform pointTrans;
    private Vector3 point;

    private Vector3 distance;
    private float fallSpeed = 1.0f;
    private float risingSpeed = 1.0f;
    private int curIndex = -1;

    private void Awake()
    {
        transform.tag = "staticObject";

        rigid = transform.GetComponent<Rigidbody2D>();

        originPos = transform.position;

        point = transform.position;
        pointTrans = transform.Find("Points").GetChild(0);
        point = pointTrans.position;
        pointTrans.GetComponent<SpriteRenderer>().enabled = false;

        distance = point - originPos;
        fallSpeed = -distance.y / fallDownTime;
        risingSpeed = -distance.y / risingSpeed;

    }

    private void Start()
    {
        StartCoroutine("Stand");
    }

    private void FixedUpdate()
    {
        FallDown();
        RisingUp();
    }

    private void FallDown()
    {
        if (curIndex != 0) return;

        fallSpeed = -distance.y / fallDownTime;
        float delta = fallSpeed * Time.deltaTime;
        transform.position += Vector3.down * delta;

        if ((transform.position - point).magnitude < delta)
        {
            transform.position = point;
            curIndex++;
            StartCoroutine("Waiting");
        }
    }

    private void RisingUp()
    {
        if (curIndex != 2) return;

        risingSpeed = -distance.y / risingSpeed;
        float delta = risingSpeed * Time.deltaTime;

        transform.position += Vector3.up * delta;

        if ((transform.position - originPos).magnitude < delta)
        {
            transform.position = originPos;
            curIndex++;
            StartCoroutine("WaitingTop");
        }
    }

    IEnumerator Waiting()
    {
        WaitForSeconds wait = new WaitForSeconds(waitingTimeOnBottom);

        while (true)
        {
            yield return wait;
            curIndex++;
            yield break;
        }
    }

    IEnumerator WaitingTop()
    {
        WaitForSeconds wait = new WaitForSeconds(waitingTimeOnTop);

        while (true)
        {
            yield return wait;
            curIndex = 0;
            yield break;
        }
    }


    IEnumerator Stand()
    {
        WaitForSeconds wait = new WaitForSeconds(stnadTime);

        while (true)
        {
            yield return wait;
            curIndex = 0;
            yield break;
        }
    }
}