using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlackOnTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    private Color touchedColor;
    private Color originColor;

    private void Awake()
    {
        img = GetComponent<Image>();
        if (img)
        {
            originColor = img.color;
            touchedColor = img.color;
            touchedColor.r = touchedColor.r / 2;
            touchedColor.g = touchedColor.g / 2;
            touchedColor.b = touchedColor.b / 2;
        }
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        img.color = touchedColor;
    }

    public void OnPointerUp(PointerEventData pointer)
    {
        img.color = originColor;
    }

}
