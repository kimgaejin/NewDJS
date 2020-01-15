using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    public float length = 10.0f;
    public float power = 0.2f;

    //public GameObject wind;
    public bool isExcuting = true;

    private void Update()
    {
        //Debug.Log("execute: " + isExcuting);
        if (isExcuting)
        {
            Execute();
        }
    }

    private void Execute()
    {
        Collider2D[] colls;

        float zAngle = transform.rotation.eulerAngles.z % 360;
        Vector2 collBoxCenterPos = Vector2.zero;
        Vector2 boxSize = Vector2.zero;
        collBoxCenterPos = GetWindBoxColliderCenter(zAngle);
        boxSize = GetWindBoxColliderSize(zAngle);
        Debug.Log("zAngle; " + zAngle);
        colls = Physics2D.OverlapBoxAll(collBoxCenterPos, boxSize, 0);

        Rigidbody2D closedRigid = null;
        float closedDistance = length * 2; ;
        foreach (Collider2D coll in colls)
        {
            Rigidbody2D collRigid = coll.GetComponent<Rigidbody2D>();
            if (collRigid)
            {
                //Debug.Log("collRigid: " + collRigid.transform.name);
                if (collRigid.tag == "platform") continue;

                float distance = Vector3.Distance(coll.transform.position, transform.position);
                if (distance < closedDistance)
                {
                    closedRigid = collRigid;
                    closedDistance = distance;
                }
            }
        }
        if (closedRigid)
        {
            Vector2 arrow = (collBoxCenterPos - (Vector2)transform.position).normalized;
            closedRigid.AddForce(arrow * power, ForceMode2D.Impulse);

        }
    }

    public void ExecutionOn()
    {
        isExcuting = true;
    }

    public void ExecutionOff()
    {
        isExcuting = false;
    }

    private Vector2 GetWindBoxColliderCenter(float zAngle)
    {
        bool faceLeft = zAngle < 45 || 315 < zAngle;
        bool faceDown = 45 < zAngle && zAngle < 135;
        bool faceRight = 135 < zAngle && zAngle < 225;
        bool faceUp = 225 < zAngle && zAngle < 315;

        Vector2 collBoxCenterPos = Vector2.zero;

        return transform.position + (-transform.right * length / 2.0f);
        if (faceLeft)
        {
            collBoxCenterPos = transform.position + (-transform.right * length / 2.0f);
        }
        else if (faceDown)
        {
            collBoxCenterPos = transform.position + (-transform.up * length / 2.0f);
        }
        else if (faceRight)
        {
            collBoxCenterPos = transform.position + (transform.right * length / 2.0f);
        }
        else if (faceUp)
        {
            collBoxCenterPos = transform.position + (transform.up * length / 2.0f);
        }

        return collBoxCenterPos;
    }

    private Vector2 GetWindBoxColliderSize(float zAngle)
    {
        bool faceLeft = zAngle < 45 || 315 < zAngle;
        bool faceDown = 45 < zAngle && zAngle < 135;
        bool faceRight = 135 < zAngle && zAngle < 225;
        bool faceUp = 225 < zAngle && zAngle < 315;

        Vector2 boxSize = Vector2.zero;

        if (faceLeft || faceRight)
        {
            boxSize = new Vector2(length, 1);
        }
        else
        {
            boxSize = new Vector2(1, length);
        }

        return boxSize;
    }
}