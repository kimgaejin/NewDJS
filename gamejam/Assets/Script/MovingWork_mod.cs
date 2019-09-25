using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWork_mod : MonoBehaviour
{
    public Vector2 velocity = new Vector2( 1, 0 );
    private Vector3 initLocalPos;

    private bool isDisable = false;

    private void Awake()
    {
        initLocalPos = transform.localPosition;

        transform.GetComponent<Rigidbody2D>().velocity = this.velocity;
    }

    private void Update()
    {
        if (isDisable) return;

        transform.localPosition = initLocalPos;
    }

    private void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    private void Stop()
    {
        SetVelocity(Vector2.zero);
        isDisable = true;
    }

}
