using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDirector : MonoBehaviour
{
    public float standTime = 3.0f;
    public float fadeOffTime = 2.0f;
    
    private RectTransform chapterTitleImg;

    private void Awake()
    {
        chapterTitleImg = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine("ShowChapterTitle");
    }

    IEnumerator ShowChapterTitle()
    {
        float frame = 0.02f;
        int perSecondFrame = (int)(1/frame);
        int fadeOffFrame = (int)(perSecondFrame * fadeOffTime);
        WaitForSeconds waitFrame = new WaitForSeconds(frame);
        Image img = chapterTitleImg.GetComponent<Image>();
        float perAlpha = frame;

        WaitForSeconds waitStand = new WaitForSeconds(standTime);

        while (true)
        {
            yield return waitStand;

            Color color = img.color;
            for (int i = 0; i < fadeOffFrame; i++)
            {
                color.a -= perAlpha;
                img.color = color;
                yield return waitFrame;
            }
            yield break;
        }
    }
}
