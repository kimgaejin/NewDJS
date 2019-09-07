using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gggsddfs : MonoBehaviour
{
    Animator animator;
    bool passed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("passed", true);
    }
}
