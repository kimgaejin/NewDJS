
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletor : MonoBehaviour
{
    private void Awake()
    {
        transform.tag = "danger";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "platform") return;
        if (collision.tag == "Player") return;

        Transform tfCollisionParent = collision.transform.parent;
        if (tfCollisionParent.GetComponent<Creator_One>())
        {
            tfCollisionParent.GetComponent<Creator_One>().Create();
        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}