using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyForStacked : MonoBehaviour
{
    private Vector3 beforePos;
    private Transform beforeGrandParent;

    private void Awake()
    {
        beforeGrandParent = null;
        beforePos = transform.position;
    }

    private void Update()
    {
        Transform parent = null;
        Transform grandParent = null;
        parent = transform.parent;
        if (parent) grandParent = parent.parent;

        if (grandParent != null)
        {
            Rigidbody2D grandRigid = grandParent.GetComponent<Rigidbody2D>();
            {
                if (beforeGrandParent == null || grandParent == beforeGrandParent)
                {
                    Vector3 posDiff;
                    posDiff = grandParent.position - grandParent.GetComponent<RigidbodyForStacked>().beforePos;
                    posDiff.y = 0;
                    transform.position += posDiff;
                }
            }
        }

        beforePos = transform.position;
    }


}
