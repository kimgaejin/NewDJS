using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float jumpPower = 7.0f;

    private bool isGround = false;
    private int jumpCount = 0;  // 땅을 밟으면 2가 된다. 점프할때마다 1씩 감소. 0보다 낮으면 점프 불가.
    private int maxJumpNum = 2;


    private Vector3 befPos;
    private bool usingMovingPlatform = false;
     
    private bool isDead = false;

    Rigidbody2D rigid;
    SpriteRenderer spr;
    Animator anim;
    Color normalColor;

    public static Player playerInstance;

    //포탈 관련 변수
    public static char key_down;
    public GameObject potalA;
    public GameObject potalB;
    //

    public GameObject light;
    public Transform lightPos;

    private int Stage; //스테이지 저장변수

    private void Awake()
    {
        Stage = PlayerPrefs.GetInt("Stage"); //게임 시작시 현재까지 깬 스테이지 로드
        jumpCount = maxJumpNum;

        if (Player.playerInstance == null) Player.playerInstance = this;

        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        normalColor = spr.color;
    }

    private void Update()
    {

        // 죽으면 조작 불가
        if (isDead) return;

        if (rigid.velocity.y < -200) StartCoroutine("Dead");

        // 점프(키보드입력, 무제한 점프)
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump(jumpPower);
        }
        
        if (Portal.Portal_A == 1)
        {
            Debug.Log("Portal_A");
            if (key_down == 'A')
            {
                transform.position = new Vector2((10 * PortalB.Portal_B_pos.x - 7) / 10, PortalB.Portal_B_pos.y);
                Portal.Portal_A = 0;
            }
            else if (key_down == 'D')
            {
                transform.position = new Vector2((10 * PortalB.Portal_B_pos.x + 7) / 10, PortalB.Portal_B_pos.y);
                Portal.Portal_A = 0;
            }

        }
        if (PortalB.Portal_B == 1) {
            Debug.Log("Portal_B");
            if (key_down == 'A')
            {
                transform.position = new Vector2((10 * Portal.Portal_A_pos.x - 7) / 10, Portal.Portal_A_pos.y);
                PortalB.Portal_B = 0;
            }
            else if (key_down == 'D')
            {
                transform.position = new Vector2((10 * Portal.Portal_A_pos.x + 7) / 10, Portal.Portal_A_pos.y);
                PortalB.Portal_B = 0;
            }
        }
    }

    private void FixedUpdate ()
    {
        // 죽으면 조작 불가
        if (isDead) return;

        // 좌우이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            key_down = 'A';
       
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            key_down = 'D';
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            key_down = '1';
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            key_down = '2';
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            key_down = '3';
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            key_down = '4';
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            key_down = '5';
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            key_down = '6';
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            key_down = '7';
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            key_down = '8';
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            key_down = '9';
        }

 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Light();
        }

        befPos = transform.position;
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
        if (col.gameObject.tag.Equals("danger"))
        {
            StartCoroutine("Dead");

        }

        if (col.gameObject.tag.Equals("green") || col.gameObject.tag.Equals("blue"))
        {
          
        }
        if (col.gameObject.tag.Equals("Player"))
        {
            Debug.Log("asdfasdfasdf");

        }
        if (col.gameObject.tag.Equals(""))
        {
          
        }
        if (col.gameObject.tag.Equals(""))
        {
          
        }


        //게임 스테이지 저장
        if (col.gameObject.tag.Equals("Level11"))
        {
            PlayerPrefs.SetInt("Stage1", 11); // 플레그 접촉시 스테이지 저장
        }
      
        
        /////포탈 충돌관련 함수
        for (int i = 0; i < 10; i++)
        {
            if (col.gameObject.tag.Equals("potalA" + i))
            {
                potalB = GameObject.FindWithTag("potalB" + i);
                if (key_down == 'A')
                {
                    transform.position = new Vector2((10 * potalB.transform.position.x - 7) / 10, potalB.transform.position.y);
                }
                else if (key_down == 'D')
                {
                    transform.position = new Vector2((10 * potalB.transform.position.x + 7) / 10, potalB.transform.position.y);
                }

            }

        }

        for (int i = 0; i < 10; i++)
        {
            if (col.gameObject.tag.Equals("potalB" + i))
            {
                potalA = GameObject.FindWithTag("potalA" + i);
                if (key_down == 'A')
                {
                   transform.position = new Vector2((10 * potalA.transform.position.x - 7) / 10, potalA.transform.position.y);
                }
                else if (key_down == 'D')
                {
                    transform.position = new Vector2((10 * potalA.transform.position.x + 7) / 10, potalA.transform.position.y);
                }

            }
        }
        ///////////////////////////////////////
    }

   
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("windzoneup"))
        {
            
            transform.position += Vector3.up * 3 * Time.deltaTime;
        }
    }

 
    public void SetCanJump()
    {
        isGround = true;
        jumpCount = maxJumpNum;
    }

    void Light()
    {
        CreateLight();
    }

    void CreateLight()
    {
        Instantiate(light, lightPos.position, lightPos.rotation);
    }

    private void Revival()
    {
        anim.enabled = true;
        spr.color = normalColor;
        transform.position = new Vector3(-2.2f, 4, 0);
        isDead = false;
        rigid.isKinematic = false;
        rigid.velocity = Vector2.zero;
    }

    IEnumerator Dead()
    {
        WaitForSeconds wait5 = new WaitForSeconds(0.05f);
        WaitForSeconds wait100 = new WaitForSeconds(1.0f);

        while (true)
        {

            if (isDead == true) yield break;

            isDead = true;
            anim.enabled = false;

            Color dec = spr.color / 20;
            dec.a = 0;
            for (int i = 0; i < 20; i++)
            {
                spr.color -= dec;
                yield return wait5;
            }

            rigid.isKinematic = true;

            float transparentAlpha = spr.color.a / 20;
            Color transparentColor = new Color(0, 0, 0, transparentAlpha);

            for (int i = 0; i < 20; i++)
            {
                spr.color -= transparentColor;
                yield return wait5;
            }

            yield return wait100;

            Revival();
            yield break;
        }
    }

    public float IncJumpPower(float value)
    {
        usingMovingPlatform = true;
        jumpPower += value;
        if (jumpPower < 0)
        {
            jumpPower = 0;
            return jumpPower;
        }
        return value;
    }

    public void DecJumpPower(float value)
    {
        if (usingMovingPlatform == true)
        {
            jumpPower -= value;
            usingMovingPlatform = false;
        }
    }

    public void Jump(float power)
    {
        if (jumpCount <= 0) return;

        Vector2 resPower = new Vector2(0, power);
        //Debug.Log("resP " + resPower);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(resPower, ForceMode2D.Impulse);
       // Debug.Log("velocity: " + rigid.velocity);

        jumpCount--;

        Debug.Log("jumpCount: " + jumpCount);
    }

    public void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
