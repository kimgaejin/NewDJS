using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptCanvasManager : MonoBehaviour, IPointerDownHandler
{
    public ScriptPrint scriptPrint;

    private Image NormalBook;
    private Image NormalLibrarian;

    private List <string> curImageStringList;

    private Color normalColor;
    private Color blackColor;

    public void Init()
    {
        curImageStringList = new List<string>();
        normalColor = new Color(1, 1, 1, 1);
        blackColor = new Color(0, 0, 0, 1);
    }

    public void Link(GameObject scriptPanel)
    {
        scriptPanel.SetActive(true);
        NormalBook = scriptPanel.transform.Find("BookNormal").GetComponent<Image>();
        if (!NormalBook) Debug.Log("not exist NormalBook");
        NormalLibrarian = scriptPanel.transform.Find("LibrarianNormal").GetComponent<Image>();
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
        NormalBook.gameObject.SetActive(false);
        NormalLibrarian.gameObject.SetActive(false);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        Talk();
    }

    public void Talk()
    {
        if (scriptPrint)
        {
            scriptPrint.TurnPage();

            string curImage = scriptPrint.GetCurImageString();
            Debug.Log("curImage: " + curImage);
            foreach (string cur in curImageStringList)
            {
                SetColor(cur, blackColor);
            }

            switch (curImage)
            {
                case "book":

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
                case "lib":
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
        scriptPrint.ImageStringClear();
        curImageStringList.Clear();        
    }

}
