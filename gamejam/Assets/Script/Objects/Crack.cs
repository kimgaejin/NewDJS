using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    [Range(0.1f, 10f)]
    [Tooltip("target이 지속되는 시간")]
    public float ableTime = 0.5f;
    [Range(0.1f, 10f)]
    [Tooltip("target이 비활성화 되어 있는 시간")]
    public float disableTime = 2.0f;

    private GameObject target;

    private void Awake()
    {
        transform.tag = "staticObject";

        target = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        StartCoroutine("Execute");
    }

    IEnumerator Execute()
    {
        WaitForSeconds waitAbleTime = new WaitForSeconds(ableTime);
        WaitForSeconds waitDisableTime = new WaitForSeconds(disableTime);

        while (true)
        {
            target.SetActive(true);
            yield return waitAbleTime;

            target.SetActive(false);
            yield return waitDisableTime;
        }
    }

}