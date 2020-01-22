using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    private Vector3 beforePos;
    private Transform tfCameraColl;
    private CircleCollider2D playerCameraColl;
    private bool isTouching;
    private Transform tfPlayer;

    private void Awake()
    {
        beforePos = transform.position;
        tfPlayer = GameObject.Find("Player").transform;
        tfCameraColl = tfPlayer.Find("CameraColl");
        playerCameraColl = tfCameraColl.GetComponent<CircleCollider2D>();

    }

    private void FixedUpdate()
    {
        Vector2 velocity = (tfPlayer.position - transform.position) * 5;
        if (velocity.magnitude < 5) velocity = velocity.normalized * 5;
        transform.GetComponent<Rigidbody2D>().velocity = velocity;
        JumpToPlayer();
    }

    private bool JumpToPlayer()
    {
        if (Physics2D.IsTouchingLayers(playerCameraColl, -1))//LayerMask.NameToLayer("Camera")))
        {
            Collider2D[] colls = new Collider2D[20];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(1 << LayerMask.NameToLayer("Camera"));
            playerCameraColl.OverlapCollider(contactFilter, colls);

            foreach (Collider2D coll in colls)
            {
                try
                {
                    if (coll.name.Contains("JumpCollider"))
                    {
                        Debug.Log("CAMERA JUMP!");
                        Transform jumpColliderParent = coll.transform.parent;
                        Transform jumpDestination = jumpColliderParent.Find("JumpDestination");
                        transform.position = jumpDestination.position;
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }

        }
        return false;
    }

}