using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    public float length = 10.0f;
    public float power = 0.2f;

    //public GameObject wind;
    private bool isExcuting = true;

    private void Update()
    {
        if (isExcuting == false) return;

        Execute();
    }

    private void Execute()
    {
        Collider2D[] colls;
        colls = Physics2D.OverlapBoxAll(transform.position + (-transform.right * length / 2.0f), new Vector2(length, 1), 0);
        Rigidbody2D closedRigid = null;
        float closedDistance = length * 2; ;
        foreach (Collider2D coll in colls)
        {
            Rigidbody2D collRigid = coll.GetComponent<Rigidbody2D>();
            if (collRigid)
            {
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
            closedRigid.AddForce(-transform.right * power, ForceMode2D.Impulse);

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
}
