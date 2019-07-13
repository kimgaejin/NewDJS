using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBlock : MonoBehaviour
{
    const int MAX_TRACKS = 100;

    GameObject [] blocks;
    public int [] blockInd;
    public Vector3 [] tracks;
    public int curTrack = 0;

    Vector3 beforePos;
    //Vector3 curArrow = Vector3.zero;
    float beforeXDiff;
    float beforeYDiff;
    Vector3 beforeVecDiff;

    int blockSize = 0;

    private void Awake()
    {
        blockSize = transform.childCount;

        // blocks 할당
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
            blocks[num] = transform.GetChild(i).gameObject;
            num++;
        }
        blockSize -= notPlatformSize;

        blockInd = new int[blockSize];
        tracks = new Vector3[MAX_TRACKS];
        
        // 초기 tracks 할당
        for (int i = 0 ; i < blockSize; i++)
        {
            tracks[i] = blocks[blockSize - 1 - i].transform.position;
            blockInd[i] = blockSize - 1 - i;
        }
        tracks[blockSize] = transform.position;

        beforePos = transform.position;
        curTrack = blockSize;

    }

    private void Update()
    {
        LogTrack();

        // 하위 블록이 하나도 없다면 SnakeBlock이 의미가 없다.
        if (blockSize <= 0) return;

        // 스네이크 블록의 머리가 움직이지 않았다면 모두 움직이지 않는다.
        if (beforePos == transform.position) return;

        // 언제나 가장 앞 track은 스네이크 머리
        tracks[curTrack+1] = transform.position;

        // 방향이 바뀌면 새로운 Track 추가
        float curXDiff = transform.position.x - beforePos.x;
        float curYDiff = transform.position.y - beforePos.y;
        Vector3 curVec = new Vector3(curXDiff, curYDiff, 0);



        //if (Mathf.Abs(curXDiff - beforeXDiff) > 0.005f || Mathf.Abs(curYDiff - beforeYDiff) > 0.005f) 
        Vector3 directionDiffer = curVec - beforeVecDiff;
        if ( directionDiffer.x > 0.01f || directionDiffer.y > 0.01f)
        {
           // Debug.Log("direction: " + directionDiffer);
           // Debug.Log("방향전환" + (curVec - beforeVecDiff).magnitude);
           // Debug.Log("cur: "+curVec.x + " "+curVec.y +" Be: "+ beforeVecDiff.x + " "+ beforeVecDiff.y);
            //Debug.Log("normal: " + (double)(transform.position - beforePos).normalized.x + " " + (double)(transform.position - beforePos).normalized.y);
            //Debug.Log("arrow: " + (double)curArrow.x +" " + (double)curArrow.y);
            // Debug.Log(((transform.position - beforePos).normalized) * 10 + " "+(curArrow) * 10 + "방향변경" + (Vector3.Distance((transform.position - beforePos).normalized, curArrow)));
            AddTrack();

            beforeXDiff = curXDiff;
            beforeYDiff = curYDiff;
            beforeVecDiff = new Vector3(beforeXDiff, beforeYDiff, 0);
            //curArrow = curVec;
        }

        Vector3 parentDifference = transform.position - beforePos;

        for (int i = 0; i < blockSize; i++)
        {
            Vector3 blockPos = blocks[i].transform.position;

            blockPos -= parentDifference;

            if (Vector3.Distance(blocks[i].transform.position, tracks[blockInd[i]+1]) < 0.1f)
            {
                // Debug.Log( blocks[i].name+": " + Vector3.Distance(blocks[i].transform.position, tracks[blockInd[i]]));
                Debug.Log(blocks[i].name + ": " + blockInd[i]);
                 
                blockInd[i]++;
                if (blockInd[i] >= MAX_TRACKS) blockInd[i] = 0;
                blockPos = tracks[blockInd[i]];
                //Debug.Log(blocks[i].name + " is on track " + blockInd[i]);

            }
            else
            {
                Debug.Log("going: " + (tracks[blockInd[i] + 1] - blockPos).normalized.x + " " + (tracks[blockInd[i] + 1] - blockPos).normalized.y);
                blockPos += (tracks[blockInd[i]+1] - blockPos).normalized * Time.deltaTime;
            }


            blocks[i].transform.position = blockPos;
        }

        beforePos = transform.position;
    }

    private void AddTrack()
    {
        curTrack++;
        if (curTrack == MAX_TRACKS) curTrack = 0;

        Vector3 target = transform.position;
        tracks[curTrack] = target;
    }
    
    private void LogTrack()
    {
        for (int i = 0; i < blockSize; i++)
        {
            Debug.DrawRay(blocks[i].transform.position ,tracks[blockInd[i]]-blocks[i].transform.position);
        }
    }

    private void AdjustBlock()
    {
        Debug.Log("adjust");
        for (int i = 0; i < blockSize; i++)
        {
            blocks[i].transform.position = tracks[i];
        }
    }
}
