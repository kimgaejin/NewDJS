using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildsTransparency : MonoBehaviour
{
    public float transparency = 0.0f;

    private void Awake()
    {
        foreach (Transform trans in transform)
        {
            try
            {
                foreach (Transform childTrans in trans)
                {
                    try
                    {
                        Color ccolor = childTrans.GetComponent<SpriteRenderer>().color;
                        ccolor = new Color(ccolor.r, ccolor.b, ccolor.b, transparency);
                        childTrans.GetComponent<SpriteRenderer>().color = ccolor;
                    }
                    catch
                    {

                    }
                }
                Color color = trans.GetComponent<SpriteRenderer>().color;
                color = new Color(color.r, color.b, color.b, transparency);
                trans.GetComponent<SpriteRenderer>().color = color;
            }
            catch
            {

            }

        }
    }
}
