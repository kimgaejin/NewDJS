using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScriptCanvasManager : MonoBehaviour, IPointerDownHandler
{
    public ScriptPrint scriptPrint;

    private void Start()
    {
        GameObject panel;
        panel = transform.GetChild(0).gameObject;
        if (panel) panel.SetActive(true);

        transform.gameObject.SetActive(false);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (scriptPrint)
        {
            scriptPrint.TurnPage();
        }
    }

}
