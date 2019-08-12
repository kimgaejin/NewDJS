using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptPrint : MonoBehaviour, IPointerDownHandler
{
    [Header("Size 정한 후 대사 입력")]
    public string [] scripts;

    private GameObject scriptCanvasObj;
    private Text scriptText;

    private int curInd = -1;
    private int maxInd;
    private bool isShown = false;


    public void Awake()
    {
        scriptCanvasObj = GameObject.Find("ScriptCanvas").gameObject;
        scriptText = scriptCanvasObj.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        maxInd = scripts.Length;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isShown == false)
            {
                isShown = true;
                scriptCanvasObj.SetActive(true);
                scriptCanvasObj.GetComponent<ScriptCanvasManager>().scriptPrint = this;
                TurnPage();
            }
        }
    }
     
    public virtual void OnPointerDown(PointerEventData ped)
    {
        //TurnPage();
    }

    public void TurnPage()
    {
        curInd++;
        if (curInd < maxInd)
        {
            Debug.Log("대사출력 진행");
            scriptText.text = scripts[curInd];
        }
        else
        {
            scriptCanvasObj.SetActive(false);
        }
    }

}
