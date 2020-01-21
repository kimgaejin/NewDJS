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
