using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldText : MonoBehaviour
{
    private Animator anim;
    private RectTransform rectTransform;
    private bool withPlayer;
    private bool isOn;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        // 모든 자식들 set false
        //SetActiveAllChild(false);
        isOn = false;
        withPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            withPlayer = true;
            ////isOn = true;
            anim.Play("ToTransparency020");
            //SetActiveAllChild(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            anim.Play("ToTransparency100");
            //withPlayer = false;
           // SetActiveAllChild(false);
            //StartCoroutine(SetActiveOffAllChild());
        }
    }

    private void SetActiveAllChild(bool type)
    {
        foreach (Transform trans in transform)
        {
            trans.gameObject.SetActive(type);
        }
    }

    IEnumerator SetActiveOffAllChild()
    {
        WaitForSeconds wait100 = new WaitForSeconds(1.0f);
        while (true)
        {
            
            yield return wait100;
            isOn = false;
        }
    }
}
