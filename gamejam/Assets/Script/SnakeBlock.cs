using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBlock : MonoBehaviour
{
    const int MAX_TRACKS = 100;

    GameObject [] blocks;
    public int [] blockInd;
    public int curTrack = 0;

    public Vector3 [] tracks;

    float speed = 0;    // 부모가 움직이는 스피드를 이어받음.

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

    private void FixedUpdate()
    {
        LogTrack();

        // 하위 블록이 하나도 없다면 SnakeBlock이 의미가 없다.
        if (blockSize <= 0) return;

        // 스네이크 블록의 머리가 움직이지 않았다면 모두 움직이지 않는다.
        if (beforePos == transform.position) return;

        // 언제나 가장 앞 track은 스네이크 머리
        int ind = curTrack + 1;
        if (ind >= MAX_TRACKS) ind = 0;
        tracks[ind] = transform.position;

        // 방향이 바뀌면 새로운 Track 추가
        // 방향이 바뀌었다는 것을 A:(전전->전) 이동방향과 B:(전->지금) 이동방향이 차이가 있는지를 식별함
        // 그런데 가끔 A와 B가 동일함에도 -상수, +상수 만큼의 차이가 1프레임 사이에서 발생할 때가 있음.
        // Flat_Move에서 움직이는건 FixedUpdate, 이곳은 Update 함수였기에 그 차이가 생긴거라고 생각 됨.
        // 이곳을 FixedUpdate 함수로 고친 후 부터 비교적 잘 돌아감.
        float curXDiff = transform.position.x - beforePos.x;
        float curYDiff = transform.position.y - beforePos.y;
        Vector3 curVec = new Vector3(curXDiff, curYDiff, 0);
        Vector3 directionDiffer = curVec - beforeVecDiff;

        // 방향이 바뀌었음 (이동하는 방향의 x, y가 오차 이상 이동)
        if ( Mathf.Abs(directionDiffer.x) > 0.01f || Mathf.Abs(directionDiffer.y) > 0.01f)
        {
            Debug.Log("방향전환 " + directionDiffer.x + " " + directionDiffer.y);
            AddTrack();

            beforeXDiff = curXDiff;
            beforeYDiff = curYDiff;
            beforeVecDiff = new Vector3(beforeXDiff, beforeYDiff, 0);
            //curArrow = curVec;
        }

        Vector3 parentDifference = transform.position - beforePos;
        speed = parentDifference.magnitude;

        // 스네이크 블록의 각 몸통들이 이동
        for (int i = 0; i < blockSize; i++)
        {
            Vector3 blockPos = blocks[i].transform.position;

            blockPos -= parentDifference;


            float distanceOfAim = Vector3.Distance(blockPos, tracks[blockInd[i] + 1]);
            if (distanceOfAim <= speed)
            {
                Debug.Log("동일");
                blockInd[i]++;
                if (blockInd[i] >= MAX_TRACKS) blockInd[i] = 0;
                blockPos = tracks[blockInd[i]];

                float reminderSpeed = speed - distanceOfAim;
               // Debug.Log("remainSpeed: " + reminderSpeed);
                blockPos += (tracks[blockInd[i] + 1] - blockPos).normalized * reminderSpeed;

            }
            else
            {
                Debug.Log("ㅠ");
                blockPos += (tracks[blockInd[i] + 1] - blockPos).normalized * speed;
            }


            blocks[i].transform.position = blockPos;
        }

        beforePos = transform.position;
    }

    private void AddTrack()
    {
        curTrack++;
        if (curTrack >= MAX_TRACKS) curTrack = 0;

        Vector3 target = transform.position;
        tracks[curTrack] = target;
    }
    
    private void LogTrack()
    {
        for (int i = 0; i < blockSize; i++)
        {
            Debug.DrawRay(tracks[blockInd[i]], tracks[blockInd[i]+1] - tracks[blockInd[i]], Color.red);
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
