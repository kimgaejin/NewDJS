using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPointer : MonoBehaviour
{
    [Header("레이저가 최대 몇번이나 분할 될 수 있는지 계산하세요")]
    public int maxLazerSize = 0;
    public GameObject lazerPrefab;
    
    // 레이저포인터
    private Transform lazerParent;
    private Transform [] lazers;
    private int lazerSize;
    private int useLazer;

    private void Awake()
    {
        InitLazer();
    }

    private void Update()
    {
        useLazer = 0;
        ExploreRay(0, transform.position, Vector2.right);
        TurnOffUselessLazer(useLazer);
    }

    private void InitLazer()
    {
        lazerParent = transform.GetChild(1);

        if (maxLazerSize > 0)
        {

            lazerSize = lazerParent.childCount;
            lazers = new Transform[maxLazerSize];

            for (int i = 0; i < lazerSize; i++)
            {
                lazers[i] = lazerParent.GetChild(i);
            }

            for (int i = lazerSize; i < maxLazerSize; i++)
            {
                GameObject lazer = Instantiate(lazerPrefab, lazerParent);
                lazers[i] = lazer.transform;
            }

            lazerSize = maxLazerSize;
        }
        else
        {
            lazerSize = lazerParent.childCount;
            lazers = new Transform[lazerSize];

            for (int i = 0; i < lazerSize; i++)
            {
                lazers[i] = lazerParent.GetChild(i);
            }

        }
    }

    private bool ExploreRay(int curLazer, Vector3 pos, Vector3 arrow)
    {
        if (curLazer >= lazerSize) return false;

        if (useLazer < curLazer) useLazer = curLazer;

        lazers[curLazer].gameObject.SetActive(true);

        float rotationAngle = GetRotationHardCoding ( (Vector2)arrow);
        lazers[curLazer].position = pos;
        lazers[curLazer].Rotate(-lazers[curLazer].rotation.eulerAngles + new Vector3(0, 0, rotationAngle), Space.World);

        Ray2D ray = new Ray2D(pos, arrow);
        Debug.DrawRay(pos, arrow);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        float distance = 20.0f;

        //bool leastOnceHit = false;
        foreach (var hit in hits)
        {
            if ((hit.point - (Vector2)pos).magnitude >= distance)
            {
                continue;
            }

            // 플랫폼과 닿으면 거기서 끊긴다.
            if (hit.transform.tag == "platform")
            {
                distance = (hit.point - (Vector2)pos).magnitude;
                break;
            }

            // 너무 가깝다면 무시한다.
            if (IsClose(hit.point, pos))
            {
                continue;
            }

            MirrorState mirror = hit.transform.GetComponent<MirrorState>();
            if (mirror)
            {
                //leastOnceHit = true;
                if (TurnLazer(arrow, hit.point, mirror, curLazer + 1))
                {
                    distance = (hit.point - (Vector2)pos).magnitude;
                    break;
                }
            }

            // Portal A/B...
            if (hit.transform.tag == "PortalA")
            {
                Portal portalA = hit.transform.GetComponent<Portal>();
                ExploreRay(curLazer + 1, portalA.PotalB.transform.position, arrow);
                distance = (hit.point - (Vector2)pos).magnitude;
                break;
            }
            else if (hit.transform.tag == "PortalB")
            {
                PortalB portalB = hit.transform.GetComponent<PortalB>();
                ExploreRay(curLazer + 1, portalB.PotalA.transform.position, arrow);
                distance = (hit.point - (Vector2)pos).magnitude;
                break;
            }

            // Levers
            LazerGetter lazerGetter = hit.transform.GetComponent<LazerGetter>();
            if (lazerGetter)
            {
                lazerGetter.OnLazer();
}

            Lever_MoveOne lever = hit.transform.GetComponent<Lever_MoveOne>();
            if (lever)
            {
                lever.Switch();
            }

            Lever_Cooler leverCooler = hit.transform.GetComponent<Lever_Cooler>();
            if (leverCooler)
            {
                leverCooler.SwitchAll();
            }
            Lever_Belt leverBelt = hit.transform.GetComponent<Lever_Belt>();

            if (leverBelt)
            {
                leverBelt.SwitchAll();
            }


        }

        lazers[curLazer].localScale = new Vector3(distance, 1, 1);
        return true;
    }

    private void PortalInteract()
    {

    }

    private bool TurnLazer(Vector3 lazerArrow, Vector3 mirrorPos, MirrorState mirror, int nextLazer)
    {
        Vector3 turnedArrow = lazerArrow;

        DirectionConst.Direction direction = mirror.GetDirection();
        if (direction == DirectionConst.Direction.Up)
        {
            turnedArrow.y *= -1;
        }
        if (direction == DirectionConst.Direction.LeftUp)
        {
            // y=x축 대칭이동
            float temp = turnedArrow.x;
            turnedArrow.x = turnedArrow.y;
            turnedArrow.y = temp;
        }
        if (direction == DirectionConst.Direction.Left)
        {
            turnedArrow.x *= -1;
        }
        if (direction == DirectionConst.Direction.LeftDown)
        {
            float temp = turnedArrow.x;
            turnedArrow.x = -turnedArrow.y;
            turnedArrow.y = -temp;
            /*
            turnedArrow = turnedArrow.normalized;
            turnedArrow.x *= -1;
            turnedArrow.y = 1 - turnedArrow.x;
        */    
        }

        return ExploreRay(nextLazer, mirrorPos, turnedArrow);
    }

    private bool IsClose(Vector2 v1, Vector2 v2)
    {
        // 너무 가까운 위치라면 참
        float reliability = 0.1f;
        if (Mathf.Abs(v1.x - v2.x) + Mathf.Abs(v1.y - v2.y) < reliability)
        {
            return true;
        }
        return false;
    }

    private float GetRotationHardCoding(Vector2 arrow)
    {
        if (IsSimilarVector(arrow, new Vector2(1, 0)))
        {
            return 0;
        }
        else if (IsSimilarVector(arrow, new Vector2(0, 1)))
        {
            return 90;
        }
        else if (IsSimilarVector(arrow, new Vector2(-1, 0)))
        {
            return 180;
        }
        else if (IsSimilarVector(arrow, new Vector2(0, -1)))
        {
            return 270;
        }
        else
        {
            if (IsSimilarVector(arrow, new Vector2(1, 1)))
            {
                return 45;
            }
            else if (IsSimilarVector(arrow, new Vector2(-1, 1)))
            {
                return 135;
            }
            else if (IsSimilarVector(arrow, new Vector2(-1, -1)))
            {
                return 225;
            }
            else if (IsSimilarVector(arrow, new Vector2(1, -1)))
            {
                return 315;
            }
        }

        return -100;
    }

    private bool IsSimilarVector(Vector2 v1, Vector2 v2)
    {
        v1 = v1.normalized;
        v2 = v2.normalized;

        // 신뢰도가 낮을수록 더 비슷하다고 믿을 수 있다.
        float reliability = 0.05f;
       // Debug.Log("ABS: " + (Mathf.Abs(v1.x - v2.x) + Mathf.Abs(v1.y - v2.y)));
        if (Mathf.Abs(v1.x - v2.x) + Mathf.Abs(v1.y - v2.y) < reliability)
        {
            
            return true;
        }
        return false;
    }

    private void TurnOffUselessLazer(int useLazer)
    {
        for (int i = useLazer + 1; i < lazerSize; i++)
        {
            lazers[i].gameObject.SetActive(false);
        }
    }
}
