using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBlock : MonoBehaviour
{
    GameObject [] blocks;
    Vector3 [] tracks;

    Vector3 beforePos;

    int blockSize = 0;

    private void Awake()
    {
        blockSize = transform.childCount;

        blocks = new GameObject[blockSize];
        int num = 0;
        int notPlatformSize = 0 ;
        for (int i = 0; i < blockSize; i++)
        {
            if (transform.GetChild(i).tag != "platform")
            {
                notPlatformSize++;
                continue;
            }
            blocks[num++] = transform.GetChild(i).gameObject;
        }
        beforePos = transform.position;
        blockSize -= notPlatformSize;

        tracks = new Vector3[blockSize + 1];
        InitTrack();
        
    }

    private void Start()
    {
        //StartCoroutine("SetTrackWithInterval");
    }

    private void Update()
    {
       

        // 하위 블록이 하나도 없다면 SnakeBlock이 의미가 없다.
        if (blockSize <= 0) return;

        if (beforePos != transform.position)
        {
            Vector3 parentDifference = transform.position - beforePos;

            for (int i = 0; i < blockSize; i++)
            {
                //Debug.Log("track:" +tracks[i]);
                //Debug.Log("blokcs: " + blocks[i].transform.position);

                //Debug.Log(tracks[i] + " - " + blocks[i].transform.position + " = "+ (tracks[i] - blocks[i].transform.position));

                Vector3 dir = tracks[i] - blocks[i].transform.position;
            
                blocks[i].transform.position += dir.normalized * Time.deltaTime;

                blocks[i].transform.position -= parentDifference;
                //Debug.DrawRay(blocks[i].transform.position, parentDifference, Color.red);
            }


            if (Vector3.Distance(blocks[0].transform.position, tracks[0]) < 0.05f)
            {
                AdjustBlock();

                SetTrack();
                LogTrack();
            }


            beforePos = transform.position;
        }
    }

    private void InitTrack()
    {
        tracks[0] = transform.position;
        for (int i = 0; i<blockSize; i++)
        {
            tracks[i+1] = blocks[i].transform.position;
        }
    }

    private void SetTrack()
    {
        // 현재 위치가 track[0]. 1, 2, 3, ... 은 [0]으로부터 n 시간 전
        // n 시간은 SetTrack을 언제 호출하느냐에 따라 다름

        if (transform.position != tracks[0])
        {
            for (int i = blockSize - 1; i > 0; i--)
            {
                tracks[i] = tracks[i - 1];
            }
            tracks[0] = transform.position;
        }
    }

    private IEnumerator SetTrackWithInterval()
    {
        WaitForSeconds wait50 = new WaitForSeconds(0.1f);
        while (true)
        {
            yield return wait50;
            SetTrack();
            
        }
    }

    private void LogTrack()
    {
        for (int i = 0; i < blockSize; i++)
        {
            Debug.DrawRay(blocks[i].transform.position ,tracks[i]-blocks[i].transform.position);
        }
    }

    private void AdjustBlock()
    {
        for (int i = 0; i < blockSize; i++)
        {
            blocks[i].transform.position = tracks[i];
        }
    }
}
