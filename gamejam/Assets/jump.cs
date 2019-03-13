using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// 
 //이코드는 예전코드로 혹시 참고 해야될수 있으니 남겨둠
/// 
public class jump : MonoBehaviour
{
    public Button[] arch = new Button[21];
    public float[] archx = new float[21];
    public float[] archy = new float[21];
    public int archxy = 10;
    public int life = 4;
    public int stage = 0;

    public Sprite[] arch_black_Image = new Sprite[21];
    public Sprite[] arch_red_Image = new Sprite[21];
    int c_arch_red_image = 0;

    Rigidbody2D rb;
    float guage = 0f;
    private bool grounded = false;
    private bool isJumping = false;

    public Image Jump_Guage;

    public GameObject player;
    public Vector3 small_save_Point;
    public Vector3 big_save_Point;
    public Vector3 teleport_Point;
    public Vector3 now_point;

    public Button back;
    public GameObject ending;
    public GameObject ending1;
    public GameObject life_ending;

    public Animator anim;

    public GameObject[] heart = new GameObject[4];

    public AudioClip la;
    public AudioClip DD;
    AudioSource myAudio; //AudioSorce 컴포넌트를 변수로 담습니다.
    public static jump instance;  //자기자신을 변수로 담습니다.
    // public Animator anim;
    // Use this for initialization
   void Awake()
    {
        if (jump.instance == null) //incetance가 비어있는지 검사합니다.
        {
            jump.instance = this; //자기자신을 담습니다.
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ending.SetActive(false);
        ending1.SetActive(false);
        life_ending.SetActive(false);
        myAudio = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        jump_guage();
        jump_space();
       // teleport();
        death();
        Jump_Guage.fillAmount = guage / 60;
    }
    public void PlaySound()
    {
        myAudio.PlayOneShot(la); //soundExplosion을 재생합니다.
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("black") || col.gameObject.tag.Equals("brown"))
        {
            grounded = true;
            isJumping = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("green") || col.gameObject.tag.Equals("blue"))
        {
            // Debug.Log(player.transform.position);
            small_save_Point = player.transform.position;
            small_save_Point.y = small_save_Point.y + 1;
            if (col.gameObject.tag.Equals("blue"))
            {
                big_save_Point = player.transform.position;
            }
        }
        if (col.gameObject.tag.Equals("red"))
        {
            PlaySound();
            player.transform.position = small_save_Point;

        }
        if (col.gameObject.tag.Equals("la")) {
            PlaySound();
        }
        if (col.gameObject.tag.Equals("goal"))
        {
            teleport_Point = player.transform.position;
            teleport_Point.x = teleport_Point.x + 10;
            player.transform.position = teleport_Point;
            stage++;
            if (stage == 1)
            {
                ending.SetActive(true);
            }
            else {
                ending1.SetActive(true);
            }
        }
    }

    void jump_guage()
    {
        if (grounded == true)
        {
            anim.SetTrigger("ground");
            if (Input.GetKey(KeyCode.Space))   //입력키가 위화살표면 실행함
            {
                anim.SetBool("jumpspace", true);   
                if (guage < 60f)
                {
                    guage = guage + 1f;
                }
            }

        }
    }
    void jump_space()
    {
        if (isJumping == false && grounded == true)
        {
            if (Input.GetKeyUp(KeyCode.Space))   //입력키가 위화살표면 실행함
            {
                myAudio.PlayOneShot(DD);
                anim.SetTrigger("jumpend");
                anim.SetBool("jumpspace", false);
                rb.AddForce(new Vector2(archx[archxy], archy[archxy]) * ((guage) / 4 + 5)); //위방향으로 올라가게함
                teleport_Point = player.transform.position;
                teleport_Point.y = teleport_Point.y + 0.01f;
                player.transform.position = teleport_Point;
                isJumping = true; //점프하면 isGrounded가 false
                grounded = false;
                guage = 3f;
            }
        }
    }
    void death()
    {

        now_point = player.transform.position;
        if (now_point.y < -5) {
            player.transform.position = small_save_Point;
        }
    }
    void teleport()
    {

        if (Input.GetKeyUp(KeyCode.C))   //입력키가 위화살표면 실행함
        {
            teleport_Point = player.transform.position;
            teleport_Point.x = teleport_Point.x + 5;
            teleport_Point.y = teleport_Point.y + 5;
            player.transform.position = teleport_Point;
        }
        if (Input.GetKeyUp(KeyCode.V))   //입력키가 위화살표면 실행함
        {
            teleport_Point = player.transform.position;
            teleport_Point.x = teleport_Point.x + 90;
            player.transform.position = teleport_Point;
        }

    }
    void change_arch_image(int k)
    {
        arch[c_arch_red_image].GetComponent<Image>().sprite = arch_black_Image[c_arch_red_image];
        arch[k].GetComponent<Image>().sprite = arch_red_Image[k];
        c_arch_red_image = k;
    }
    void lifem(int k) {
        if (life <= -1)
        {
            teleport_Point = player.transform.position;
            teleport_Point.x = 2;
            teleport_Point.y = 5;
            player.transform.position = teleport_Point;

            heart[0].SetActive(true);
            heart[1].SetActive(true);
            heart[2].SetActive(true);
            heart[3].SetActive(true);
            life = 4;
            life_ending.SetActive(true);
            stage = 0;
        }
        else
        {
            heart[k].SetActive(false);
        }
       

    }
    public void PressKey(int nKey)
    {
        switch (nKey)
        {

            case 1:
                archxy = 0;
                change_arch_image(archxy);
                break;
            case 2:
                archxy = 1;
                change_arch_image(archxy);
                break;
            case 3:
                archxy = 2;
                change_arch_image(archxy);
                break;
            case 4:
                archxy = 3;
                change_arch_image(archxy);
                break;
            case 5:
                archxy = 4;
                change_arch_image(archxy);
                break;
            case 6:
                archxy = 5;
                change_arch_image(archxy);
                break;
            case 7:
                archxy = 6;
                change_arch_image(archxy);
                break;
            case 8:
                archxy = 7;
                change_arch_image(archxy);
                break;
            case 9:
                archxy = 8;
                change_arch_image(archxy);
                break;
            case 10:
                archxy = 9;
                change_arch_image(archxy);
                break;
            case 11:
                archxy = 10;
                change_arch_image(archxy);
                break;
            case 12:
                archxy = 11;
                change_arch_image(archxy);
                break;
            case 13:
                archxy = 12;
                change_arch_image(archxy);
                break;
            case 14:
                archxy = 13;
                change_arch_image(archxy);
                break;
            case 15:
                archxy = 14;
                change_arch_image(archxy);
                break;
            case 16:
                archxy = 15;
                change_arch_image(archxy);
                break;
            case 17:
                archxy = 16;
                change_arch_image(archxy);
                break;
            case 18:
                archxy = 17;
                change_arch_image(archxy);
                break;
            case 19:
                archxy = 18;
                change_arch_image(archxy);
                break;
            case 20:
                archxy = 19;
                change_arch_image(archxy);
                break;
            case 21:
                archxy = 20;
                change_arch_image(archxy);
                break;
            case 9999:
                player.transform.position = big_save_Point;
                life--;
                lifem(life);
                break;
            case 10000:
                ending.SetActive(false);
                break;
            case 10001:
                ending1.SetActive(false);
                break;
            case 10002:
                life_ending.SetActive(false);
                break;

        }
    }
}
