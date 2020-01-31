using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator_Child : MonoBehaviour
{
    private Transform tfParent;

    private void Awake()
    {
        tfParent = transform.parent;
    }

    public void Undo()
    {
        Debug.Log("Creator_Child Undo");
        transform.parent = tfParent;
        Creator_One creator_One = tfParent.GetComponent<Creator_One>();
        if (creator_One) creator_One.Create();

    }
}
