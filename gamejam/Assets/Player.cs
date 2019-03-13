using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1.0f;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
    //기본적인 틀
    //각 프리팹에 해당되는 컬러네임으로 오브젝트를 식별
    //해당되는 태그명으로 오브젝트에 대한 코드를 작성할수 있음
    //오브젝트 이동같은 부분은 해당 오브젝트내에 코드로 수행하여야함
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("black") || col.gameObject.tag.Equals("brown"))
        {
       
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("green") || col.gameObject.tag.Equals("blue"))
        {
          
        }
        if (col.gameObject.tag.Equals("red"))
        {
     

        }
        if (col.gameObject.tag.Equals("la"))
        {
          
        }
        if (col.gameObject.tag.Equals("goal"))
        {
          
        }
    }
}
