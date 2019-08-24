using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHole : MonoBehaviour
{
    // 가시가 일정 시간마다 나왔다가 들어가는 함정입니다.

        [Header("다른 가시들과 차이를 주기 위해 처음에 기다리는 시간")]
    public float standTime = 0.0f;

    [Header("가시가 spikeTime 만큼 세워져 있습니다.")]
    public float spikeTime = 1.0f;
    [Header("가시가 holeTime 만큼 감춰져 있습니다.")]
    public float holeTime = 1.0f;

    private BoxCollider2D collider; // 감춰져있을 때, collider 미 사용으로 바꾸기
    private Animator anim;

    private void Awake()
    {
        collider = transform.GetChild(0).GetChild(1).GetComponent<BoxCollider2D>();
        if (!collider) Debug.Log("SpikeHole에 Collider를 찾을 수 없습니다.");

        anim = transform.GetChild(0).GetComponent<Animator>();
        if (!anim) Debug.Log("SpikeHole에 Animator를 찾을 수 없습니다.");
    }

    private void Start()
    {
        StartCoroutine("Execute");
    }

    IEnumerator Execute()
    {
        WaitForSeconds standWait = new WaitForSeconds(standTime);
        WaitForSeconds spikeWait = new WaitForSeconds(spikeTime);
        WaitForSeconds holeWait = new WaitForSeconds(holeTime);

        yield return standWait;
        while (true)
        {
            Spike();
            yield return spikeWait;

            DoNotSpike();
            yield return holeWait;

        }
    }

    private void Spike()
    {
        collider.enabled = true;
        anim.SetTrigger("Spike");
    }

    private void DoNotSpike()
    {
        collider.enabled = false;
        anim.SetTrigger("Rest");
    }
}
