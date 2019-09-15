using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformToOneDestination : MonoBehaviour
{
    // 하나의 목적지로만 계속 가려는 플랫폼입니다.
    // 중간에 플랫폼포탈같은걸 이용해서 목적지로 못가게 해주면 됩니다.
    // 그냥 플랫폼포탈 + 부유플랫폼 만으로는 최적화같은게 귀찮아서 만들었습니다.
    // 부유 플랫폼보다 호출 함수가 적습니다.

    [Header("*주의* 이거 MovingPlatform 아님.")]
    [Header("무조건 point[0]으로만 갑니다. 나머지는 신경도 안씁니다.")]

    public float speed = 1.0f;

    [Space(5)]
    [Tooltip("게임을 시작 했을 때, points들이 투명이 됩니다.")]
    public bool isPointsTransparentInGame = true;

    private GameObject target;
    private Transform pointParent;
    private Vector3[] points;
    private int pointSize;

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
    }

    private void FixedUpdate()
    {
        Vector3 distance = points[0] - target.transform.position;
        Vector3 deltaSpeed = distance.normalized * speed * Time.deltaTime;

        target.transform.position += deltaSpeed;
    }
}
