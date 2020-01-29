using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptCanvasManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ScriptPrint scriptPrint;

    private GameObject gmTraslucencyBg;
    private Image NormalBook;
    private Image NormalLibrarian;

    private List <string> curImageStringList;

    private Color normalColor;
    private Color blackColor;

    private int SKIP_SPEED_LEVEL = 1;
    private int skipSpeed;
    private bool isTouch = false;
    private int ntimer = 0;

    public void Init()
    {
        
        curImageStringList = new List<string>();
        normalColor = new Color(1, 1, 1, 1);
        blackColor = new Color(0, 0, 0, 1);
        skipSpeed = 25 * SKIP_SPEED_LEVEL;
    }

    public void Link(GameObject scriptPanel)
    {
        scriptPanel.SetActive(true);
        gmTraslucencyBg = transform.Find("TranslucencyBg").gameObject;
        NormalBook = scriptPanel.transform.Find("BookNormal").GetComponent<Image>();
        if (!NormalBook) Debug.Log("not exist NormalBook");
        NormalLibrarian = scriptPanel.transform.Find("LibrarianNormal").GetComponent<Image>();
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
        NormalBook.gameObject.SetActive(false);
        NormalLibrarian.gameObject.SetActive(false);
        gmTraslucencyBg.SetActive(false);
    }

    private void Update()
    {
        if (isTouch)
        {
            ntimer++;
            if (ntimer % skipSpeed == 0)
            {
                Talk();
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        isTouch = true;
        Talk();
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        isTouch = false;
        ntimer = 0;
    }

    public void Talk()
    {
        if (scriptPrint)
        {
            gmTraslucencyBg.SetActive(true);
            scriptPrint.TurnPage();

            string curImage = scriptPrint.GetCurImageString();
            //Debug.Log("curImage: " + curImage);
            foreach (string cur in curImageStringList)
            {
                SetColor(cur, blackColor);
            }

            switch (curImage)
            {
                case "0": // book

                    if (curImageStringList.Contains("book"))
                    {
                        SetColor("book", normalColor);
                    }
                    else
                    {
                        curImageStringList.Add("book");
                        NormalBook.gameObject.SetActive(true);
                    }
                    break;
                case "1": // librarian
                    if (curImageStringList.Contains("lib"))
                    {
                        SetColor("lib", normalColor);
                    }
                    else
                    {
                        curImageStringList.Add("lib");
                        NormalLibrarian.gameObject.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void SetColor(string imageCode, Color color)
    {
        switch (imageCode)
        {
            case "book":
                NormalBook.color = color;
                break;
            case "lib":
                NormalLibrarian.color = color;
                break;
            default:
                break;
        }
    }

    public void PageDone()
    {
        SetColor("book", normalColor);
        SetColor("lib", normalColor);
        NormalLibrarian.gameObject.SetActive(false);
        NormalLibrarian.gameObject.SetActive(false);
        gmTraslucencyBg.SetActive(false);
        scriptPrint.ImageStringClear();
        curImageStringList.Clear();        
    }

    public void Skip()
    {
        StartCoroutine(CSkip());
    }

    IEnumerator CSkip()
    {
        WaitForSeconds wait01 = new WaitForSeconds(0.05f);
        while (true)
        {
            Talk();
            yield return wait01;
        }
    }
}
