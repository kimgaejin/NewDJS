using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator_One : MonoBehaviour
{
    private Transform tfTarget;

    private void Awake()
    {
        transform.tag = "staticObject";

        tfTarget = transform.GetChild(1);
        Create();
    }

    public void Create()
    {
        tfTarget.gameObject.SetActive(true);
        tfTarget.position = transform.position + new Vector3(0.3f, 0 , 0);
        Rigidbody2D targetRigid = tfTarget.GetComponent<Rigidbody2D>();
        if (targetRigid)
        {
            targetRigid.velocity = Vector2.zero;
            targetRigid.AddForce(new Vector2(100, 100));
        }
    }

}
