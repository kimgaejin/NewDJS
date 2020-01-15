
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

        Destroy(collision.gameObject);
    }
}