using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraScroll : MonoBehaviour
{
    [Header("하위 스프라이트들의 z값을 0~100까지 설정.")]
    [Header("100 -> 움직이지않음.")]

    private Transform parent;
    private Vector3 parentBeforePos;

    private GameObject [] childObjs;
    private int childSize = 0;

    float scrollSpeed = 0.5f;

    private void Start()
    {
        parent = transform.parent;
        parentBeforePos = parent.position;

        childSize = transform.childCount;
        childObjs = new GameObject[childSize];

        for (int i = 0; i < childSize; i++)
        {
            childObjs[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        Vector3 parentDistance = parent.position - parentBeforePos;

        for (int i = 0; i < childSize;  i++)
        {
            float childZ = childObjs[i].transform.position.z;
            float zScalar = 1 / childZ;

            float x = parentDistance.x * zScalar;
            Vector3 distance = new Vector3(x, parentDistance.y, 0);

            childObjs[i].transform.position -= distance;
        }

        parentBeforePos = parent.position;
    }
}
