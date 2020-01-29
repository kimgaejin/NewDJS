using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScriptPrint : MonoBehaviour
{
    public string dialogue_group;

    private List<Dictionary<string, object>> scenario;
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
        string chapterName = SceneManager.GetActiveScene().name;
        if (chapterName.Equals("Chapter1")) chapterName = "10";
        else if (chapterName.Equals("Chapter2")) chapterName = "01";
        else if (chapterName.Equals("Chapter3")) chapterName = "02";
        else if (chapterName.Equals("Chapter4")) chapterName = "03";
        else if (chapterName.Equals("Chapter5")) chapterName = "04";

        scenario = new List<Dictionary<string, object>>();
        List<Dictionary<string, object>> data = CSVReader.Read("Scenario/Scenario", 1);

        Debug.Log("출발 " + data.Count.ToString());
        for (var i = 0; i < data.Count; i++)
        {
            if ( data[i]["chapter"].ToString() != chapterName ) continue;
            if ( data[i]["dialogue_group"].ToString() != dialogue_group) continue;
            if ( data[i]["is_active"].ToString() != "1" ) continue;

            if (i >= 1)
            {
                // 현재 대사가 마지막 인덱스 대사보다 우선순위가 낮다면 자기 자리를 찾아간다.
                int j = scenario.Count;
                while (j >= 1 && (int)(scenario[j-1]["group_order"]) > (int)(data[i]["group_order"]))
                {
                    j--;
                }
                // j가 0이거나 자신보다 우선순위가 높은 대사를 찾아냄
                if (j == 0) scenario.Insert(0, data[i]);
                else scenario.Insert(j, data[i]);
            }
            else
            {
                scenario.Insert(0, data[i]);
            }
        }

        for (var i = 0; i < scenario.Count; i++)
        {
            Debug.Log(scenario[i]["dialogue"]);
        }

        sceneInitManager = GameObject.Find("SceneInitManager").GetComponent<SceneInitManager>();
        scriptCanvasObj = sceneInitManager.GetGMScriptCanvas();
        if (scriptCanvasObj) Debug.Log("couldn't find ScriptPrint:scriptCanvasObj");
        scriptText = scriptCanvasObj.transform.Find("ScriptPanel").GetChild(0).GetComponent<Text>();
        maxInd = scenario.Count;
        //maxInd = scripts.Length;
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
            string curScript = scenario[curInd]["dialogue"].ToString();
            curImageString = scenario[curInd]["speaker"].ToString();

            //curImageString = GetImageCode(curScript);
            //curScript = GetScriptOnly(curScript);
            curScript = GetEnteredString(curScript);
            scriptText.text = curScript.ToString();
        }
        else
        {
            scriptCanvasManager.PageDone();
            scriptCanvasObj.SetActive(false);
        }
    }

    /*
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
    */
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

        //Debug.Log(scripts);
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
