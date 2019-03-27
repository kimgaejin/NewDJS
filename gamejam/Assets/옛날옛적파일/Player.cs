using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float jumpPower = 10.0f;

    private bool isGround = false;
    private int jumpCount = 0;  // 땅을 밟으면 2가 된다. 점프할때마다 1씩 감소. 0보다 낮으면 점프 불가.
    private int maxJumpNum = 2;

    Rigidbody2D rigid;

    public static Player playerInstance;

    
    private int Stage; //스테이지 저장변수
    
    private void Awake()
    {
        Stage = PlayerPrefs.GetInt("Stage"); //게임 시작시 현재까지 깬 스테이지 로드
        jumpCount = maxJumpNum;

        if (Player.playerInstance == null) Player.playerInstance = this;

        rigid = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate ()
    {
        // 좌우이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        if (jumpCount > 0)
        {
            // 점프(키보드입력, 무제한 점프)
            if (Input.GetKeyDown(KeyCode.J))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumpCount--;
            }
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
        if (col.gameObject.tag.Equals(""))
        {
          
        }
        if (col.gameObject.tag.Equals(""))
        {
          
        }
        //게임 스테이지 저장
        if (col.gameObject.tag.Equals("Flag1-1"))
        {
            PlayerPrefs.SetInt("Stage", 1); // 플레그 접촉시 스테이지 저장
        }
        if (col.gameObject.tag.Equals("Flag1-2"))
        {

        }
        if (col.gameObject.tag.Equals("Flag1-3"))
        {

        }
        ///
    }

    public void SetCanJump()
    {
        // 2019-03-27 문성현
        // 바닥에 닿은 상태라고 알리는 것.
        isGround = true;
        jumpCount = maxJumpNum;
    }
}
