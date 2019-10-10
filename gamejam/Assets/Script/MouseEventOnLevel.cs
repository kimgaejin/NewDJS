﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEventOnLevel : MonoBehaviour
{
    private Transform transform;
    private Transform[] effects;
    private int effectSize;
    private int curEffect;

    private void Awake()
    {
        transform = this.GetComponent<Transform>();

        effectSize = this.transform.childCount;
        effects = new Transform[effectSize];
        for (int i = 0; i < effectSize; i++)
        {
            effects[i] = this.transform.GetChild(i);
        }
    }

    private void Update()
    {
        OnMouseEvent();
    }

    private void OnMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceEffect();
            LeverTouch();
        }
    }

    private void PlaceEffect()
    {
        NextCurEffect();
        effects[curEffect].transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool LeverTouch()
    {
        // 레버와 맞닿았다면 해당 레버 작동
        RaycastHit2D[] hit = new RaycastHit2D[10];
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.RaycastAll(ray.origin, ray.direction);

        bool leastOne = false;
        bool tempRes = false;
        foreach (RaycastHit2D hitOne in hit)
        {
            if (hitOne.transform.tag != "Untagged") continue;

            tempRes = InteractLever(hitOne);
            if (tempRes == true) leastOne = true;
        }

        if (leastOne) return true;
        return false;
    }

    private bool InteractLever(RaycastHit2D hit)
    {
        Lever_MoveOne lever = hit.transform.GetComponent<Lever_MoveOne>();

        if (lever)
        {
            if (Vector3.Distance(transform.position, lever.transform.position) < lever.DISTANCE_WITH_PLAYER)
            {
                lever.Switch();
                return true;
            }
        }

        Lever_Cooler leverCooler = hit.transform.GetComponent<Lever_Cooler>();

        if (leverCooler)
        {
            if (Vector3.Distance(transform.position, leverCooler.transform.position) < leverCooler.DISTANCE_WITH_PLAYER)
            {
                leverCooler.SwitchAll();
                return true;
            }
        }

        Lever_Belt leverBelt = hit.transform.GetComponent<Lever_Belt>();

        if (leverBelt)
        {
            if (Vector3.Distance(transform.position, leverBelt.transform.position) < leverBelt.DISTANCE_WITH_PLAYER)
            {
                leverBelt.SwitchAll();
                return true;
            }
        }
        return false;
    }

    private void NextCurEffect()
    {
        if (effectSize <= 0) return;

        curEffect++;
        if (curEffect >= effectSize)
        {
            curEffect = 0;
        }
    }

}
