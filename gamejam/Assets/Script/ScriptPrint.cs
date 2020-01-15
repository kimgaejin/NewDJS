﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptPrint : MonoBehaviour
{
    [Header("Size 정한 후 대사 입력")]
    [Header("대화의 주체는 'book', 'lib' 만 가능합니다")]

    [Header("예제")]
    [Header("book)안녕 클레오파트라")]
    public string [] scripts;

    private GameObject scriptCanvasObj;
    private ScriptCanvasManager scriptCanvasManager;
    private SceneInitManager sceneInitManager;

    private Text scriptText;

    private int curInd = -1;
    private int maxInd;
    private bool isShown = false;

    private string curImageString;

    public void Start()
    {
        sceneInitManager = GameObject.Find("SceneInitManager").GetComponent<SceneInitManager>();
        scriptCanvasObj = sceneInitManager.GetGMScriptCanvas();
        if (scriptCanvasObj) Debug.Log("couldn't find ScriptPrint:scriptCanvasObj");
        scriptText = scriptCanvasObj.transform.Find("ScriptPanel").GetChild(0).GetComponent<Text>();
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
                scriptCanvasManager = scriptCanvasObj.GetComponent<ScriptCanvasManager>();
                scriptCanvasManager.Talk();
            }
        }
    }

    public void TurnPage()
    {
        curInd++;
        if (curInd < maxInd)
        {
            string curScript = scripts[curInd];

            curImageString = GetImageCode(curScript);
            curScript = GetScriptOnly(curScript);
            curScript = GetEnteredString(curScript);
            scriptText.text = curScript.ToString();
        }
        else
        {
            scriptCanvasManager.PageDone();
            scriptCanvasObj.SetActive(false);
        }
    }

    public string GetScriptOnly(string scripts)
    {
        string[] values = scripts.Split(')');
        if (values.Length > 1) return values[1];
        return values[0];
    }

    public string GetEnteredString(string scripts)
    {
        int scriptsLength = scripts.Length;
        int indexRegister = 0;

        while (scripts.Contains("\\n"))
        {
            scriptsLength = scripts.Length;

            for (int i = indexRegister; i < scriptsLength - 1; i++)
            {
                if (scripts[i] == '\\')
                {
                    if (scripts[i + 1] == 'n')
                    {
                        Debug.Log("얘도찾음");

                        scripts = scripts.Remove(i, 2);
                        scripts = scripts.Insert(i, "\n");
                        break;
                    }
                }
            }
        }

        Debug.Log(scripts);
        return scripts;
    }

    public string GetImageCode(string scripts)
    {
        string[] values = scripts.Split(')');
        if (values.Length > 1) return values[0];
        return "";
    }

    public void ImageStringClear()
    {
        curImageString = "";
    }

    public string GetCurImageString()
    {
        return curImageString;
    }

}
