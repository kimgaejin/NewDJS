using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [Header("하위 오브젝트를 생성합니다.")]
    [Tooltip("하위 오브젝트가 몇 초마다 실행되는지. 0.1초 간격으로 작성")]
    public float responeTime = 5.0f;

    private GameObject target;

    private void Awake()
    {
        transform.tag = "staticObject";

        target = transform.GetChild(1).gameObject;
        target.SetActive(false);
        StartCoroutine("Execute");
    }

    IEnumerator Execute()
    {
        WaitForSeconds wait10 = new WaitForSeconds(0.1f);
        int intResponeTime;

        while (true)
        {
            if (target)
            {
                GameObject createdTarget = Instantiate(target, transform.position, Quaternion.identity);
                createdTarget.transform.parent = transform;
                createdTarget.SetActive(true);
            }

            intResponeTime = (int)(responeTime * 10);

            for (int i = 0; i < intResponeTime; i++)
            {
                yield return wait10;
            }


        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {

            AudioSource Projector = GetComponent<AudioSource>();
            Projector.Play();
            Debug.Log("AAAAAAA");

        }
    }
}